#include "window.h"

#include "messenger.h"
#include "resource.h"
#include "spotify.h"
#include "text.h"

namespace {

const wchar_t kWindowClass[] = L"NowPlayingSpotifyWindow";
const wchar_t kWindowTitle[] = L"Now Playing";
const wchar_t kIdleText[] = L"No music playing";

const UINT kPollTimerId = 1;
const UINT kPollIntervalMs = 3000;
const UINT kTrayCallbackMessage = WM_APP + 1;
const UINT kTrayIconId = 1;
const UINT kExitCommand = 100;

const int kWindowWidth = 300;
const int kWindowHeight = 100;
const int kTitleBarHeight = 25;

const int kCloseIconSize = 14;
const int kCloseIconSupersample = 4;
const int kLucideViewBox = 24;
const int kLucideStrokeWidth = 2;
const int kLucideLineStart = 6;
const int kLucideLineEnd = 18;

const COLORREF kBackground = RGB(25, 26, 28);
const COLORREF kTitleBar = RGB(38, 40, 39);
const COLORREF kHeaderText = RGB(240, 240, 240);
const COLORREF kCloseButtonText = RGB(255, 255, 255);
const COLORREF kSongText = RGB(171, 171, 171);
const COLORREF kArtistText = RGB(160, 160, 160);

const wchar_t kFontFamily[] = L"Microsoft YaHei";

const RECT kHeaderRect = {12, 0, 112, kTitleBarHeight};
const RECT kCloseButtonRect = {270, 0, 300, kTitleBarHeight};
const RECT kSongRect = {12, 37, 288, 67};
const RECT kArtistRect = {15, 67, 291, 92};

HFONT CreatePointFont(int points) {
  HDC screen = GetDC(0);
  const int height = -MulDiv(points, GetDeviceCaps(screen, LOGPIXELSY), 72);
  ReleaseDC(0, screen);

  return CreateFontW(height, 0, 0, 0, FW_NORMAL, FALSE, FALSE, FALSE,
                     DEFAULT_CHARSET, OUT_DEFAULT_PRECIS, CLIP_DEFAULT_PRECIS,
                     CLEARTYPE_QUALITY, DEFAULT_PITCH | FF_DONTCARE,
                     kFontFamily);
}

void FillRectangle(HDC canvas, const RECT &area, COLORREF color) {
  HBRUSH brush = CreateSolidBrush(color);
  FillRect(canvas, &area, brush);
  DeleteObject(brush);
}

void DrawLabel(HDC canvas, HFONT font, COLORREF color, RECT area,
               const wchar_t *text, UINT format) {
  if (IsEmpty(text))
    return;

  HGDIOBJ previous = SelectObject(canvas, font);
  SetTextColor(canvas, color);
  SetBkMode(canvas, TRANSPARENT);
  DrawTextW(canvas, text, -1, &area, format | DT_SINGLELINE | DT_NOPREFIX);
  SelectObject(canvas, previous);
}

void DrawCloseIcon(HDC canvas, const RECT &button, COLORREF color) {
  const int size = kCloseIconSize * kCloseIconSupersample;
  const int low = (kLucideLineStart * size) / kLucideViewBox;
  const int high = (kLucideLineEnd * size) / kLucideViewBox;
  const int stroke = (kLucideStrokeWidth * size) / kLucideViewBox;

  HDC layer = CreateCompatibleDC(canvas);
  HBITMAP bitmap = CreateCompatibleBitmap(canvas, size, size);
  HGDIOBJ previous_bitmap = SelectObject(layer, bitmap);

  RECT area = {0, 0, size, size};
  FillRectangle(layer, area, kTitleBar);

  LOGBRUSH brush;
  brush.lbStyle = BS_SOLID;
  brush.lbColor = color;
  brush.lbHatch = 0;
  HPEN pen = ExtCreatePen(
      PS_GEOMETRIC | PS_SOLID | PS_ENDCAP_ROUND | PS_JOIN_ROUND, stroke, &brush,
      0, 0);
  HGDIOBJ previous_pen = SelectObject(layer, pen);

  MoveToEx(layer, high, low, 0);
  LineTo(layer, low, high);
  MoveToEx(layer, low, low, 0);
  LineTo(layer, high, high);

  SelectObject(layer, previous_pen);
  DeleteObject(pen);

  const int left =
      button.left + (button.right - button.left - kCloseIconSize) / 2;
  const int top =
      button.top + (button.bottom - button.top - kCloseIconSize) / 2;
  SetStretchBltMode(canvas, HALFTONE);
  SetBrushOrgEx(canvas, 0, 0, 0);
  StretchBlt(canvas, left, top, kCloseIconSize, kCloseIconSize, layer, 0, 0,
             size, size, SRCCOPY);

  SelectObject(layer, previous_bitmap);
  DeleteObject(bitmap);
  DeleteDC(layer);
}

bool Contains(const RECT &area, POINT point) {
  return point.x >= area.left && point.x < area.right && point.y >= area.top &&
         point.y < area.bottom;
}

POINT PointFromLParam(LPARAM lp) {
  POINT p = {static_cast<short>(LOWORD(lp)), static_cast<short>(HIWORD(lp))};
  return p;
}

bool SameTrack(const Track &a, const Track &b) {
  return a.state == b.state && Equals(a.artist, b.artist) &&
         Equals(a.title, b.title) && Equals(a.album, b.album);
}

template <typename Fn> Fn Resolve(HMODULE library, const char *name) {
  return reinterpret_cast<Fn>(
      reinterpret_cast<void *>(GetProcAddress(library, name)));
}

struct FrameMargins {
  int left;
  int right;
  int top;
  int bottom;
};

void ExtendFrameShadow(HWND window) {
  typedef HRESULT(WINAPI * IsCompositionEnabledFn)(BOOL *);
  typedef HRESULT(WINAPI * ExtendFrameFn)(HWND, const FrameMargins *);
  typedef HRESULT(WINAPI * SetWindowAttributeFn)(HWND, DWORD, LPCVOID, DWORD);

  const DWORD kNonClientRenderingPolicy = 2;
  DWORD policy = 2;

  HMODULE library = LoadLibraryW(L"dwmapi.dll");
  if (!library)
    return;

  IsCompositionEnabledFn is_enabled =
      Resolve<IsCompositionEnabledFn>(library, "DwmIsCompositionEnabled");
  ExtendFrameFn extend_frame =
      Resolve<ExtendFrameFn>(library, "DwmExtendFrameIntoClientArea");
  SetWindowAttributeFn set_attribute =
      Resolve<SetWindowAttributeFn>(library, "DwmSetWindowAttribute");

  BOOL composition = FALSE;
  if (is_enabled && extend_frame && set_attribute &&
      is_enabled(&composition) == S_OK && composition) {
    set_attribute(window, kNonClientRenderingPolicy, &policy, sizeof(policy));
    FrameMargins margins = {0, 0, 0, 1};
    extend_frame(window, &margins);
  }

  FreeLibrary(library);
}

}

MainWindow::MainWindow()
    : window_(0), tray_added_(false), header_font_(0), song_font_(0),
      artist_font_(0) {
  ZeroMemory(&tray_, sizeof(tray_));
}

MainWindow::~MainWindow() {
  if (tray_added_)
    Shell_NotifyIconW(NIM_DELETE, &tray_);
  if (header_font_)
    DeleteObject(header_font_);
  if (song_font_)
    DeleteObject(song_font_);
  if (artist_font_)
    DeleteObject(artist_font_);
}

bool MainWindow::Create(HINSTANCE instance) {
  HICON icon = LoadIconW(instance, MAKEINTRESOURCEW(IDI_APPLICATION_ICON));

  WNDCLASSEXW window_class;
  ZeroMemory(&window_class, sizeof(window_class));
  window_class.cbSize = sizeof(window_class);
  window_class.lpfnWndProc = WindowProc;
  window_class.hInstance = instance;
  window_class.hCursor = LoadCursorW(0, IDC_ARROW);
  window_class.lpszClassName = kWindowClass;
  window_class.hIcon = icon;
  if (!RegisterClassExW(&window_class))
    return false;

  RECT frame = {0, 0, kWindowWidth, kWindowHeight};
  AdjustWindowRectEx(&frame, WS_POPUP, FALSE, 0);

  const int width = frame.right - frame.left;
  const int height = frame.bottom - frame.top;
  const int left = (GetSystemMetrics(SM_CXSCREEN) - width) / 2;
  const int top = (GetSystemMetrics(SM_CYSCREEN) - height) / 2;

  window_ = CreateWindowExW(0, kWindowClass, kWindowTitle, WS_POPUP, left, top,
                            width, height, 0, 0, instance, this);
  if (!window_)
    return false;

  header_font_ = CreatePointFont(10);
  song_font_ = CreatePointFont(16);
  artist_font_ = CreatePointFont(10);

  tray_.cbSize = sizeof(tray_);
  tray_.hWnd = window_;
  tray_.uID = kTrayIconId;
  tray_.uFlags = NIF_ICON | NIF_MESSAGE | NIF_TIP;
  tray_.uCallbackMessage = kTrayCallbackMessage;
  tray_.hIcon = icon;
  Copy(tray_.szTip, sizeof(tray_.szTip) / sizeof(wchar_t), kWindowTitle);
  tray_added_ = Shell_NotifyIconW(NIM_ADD, &tray_) != FALSE;

  ExtendFrameShadow(window_);

  ShowWindow(window_, SW_SHOW);
  UpdateWindow(window_);

  SetTimer(window_, kPollTimerId, kPollIntervalMs, 0);
  Poll();
  return true;
}

LRESULT CALLBACK MainWindow::WindowProc(HWND window, UINT message, WPARAM wp,
                                        LPARAM lp) {
  MainWindow *self = 0;

  if (message == WM_NCCREATE) {
    CREATESTRUCTW *create = reinterpret_cast<CREATESTRUCTW *>(lp);
    self = static_cast<MainWindow *>(create->lpCreateParams);
    self->window_ = window;
    SetWindowLongPtrW(window, GWLP_USERDATA, reinterpret_cast<LONG_PTR>(self));
  } else {
    self = reinterpret_cast<MainWindow *>(
        GetWindowLongPtrW(window, GWLP_USERDATA));
  }

  if (!self)
    return DefWindowProcW(window, message, wp, lp);
  return self->HandleMessage(message, wp, lp);
}

LRESULT MainWindow::HandleMessage(UINT message, WPARAM wp, LPARAM lp) {
  switch (message) {
  case WM_PAINT: {
    PAINTSTRUCT paint;
    HDC target = BeginPaint(window_, &paint);
    Paint(target);
    EndPaint(window_, &paint);
    return 0;
  }
  case WM_ERASEBKGND:
    return 1;
  case WM_TIMER:
    if (wp == kPollTimerId)
      Poll();
    return 0;
  case WM_NCHITTEST: {
    POINT client = PointFromLParam(lp);
    ScreenToClient(window_, &client);
    return Contains(kCloseButtonRect, client) ? HTCLIENT : HTCAPTION;
  }
  case WM_LBUTTONDOWN:
    OnLeftClick(lp);
    return 0;
  case kTrayCallbackMessage:
    OnTrayMessage(wp, lp);
    return 0;
  case WM_COMMAND:
    if (LOWORD(wp) == kExitCommand)
      Quit();
    return 0;
  case WM_CLOSE:
    ShowWindow(window_, SW_HIDE);
    return 0;
  case WM_DESTROY:
    PostQuitMessage(0);
    return 0;
  }
  return DefWindowProcW(window_, message, wp, lp);
}

void MainWindow::Poll() {
  Track next;
  ReadSpotifyTrack(&next);

  if (next.state == PlaybackPlaying)
    SendNowPlaying(next);
  else if (next.state == PlaybackIdle)
    ClearNowPlaying();

  if (SameTrack(next, track_))
    return;

  track_ = next;
  InvalidateRect(window_, 0, FALSE);
}

void MainWindow::Paint(HDC target) const {
  RECT client;
  GetClientRect(window_, &client);

  HDC canvas = CreateCompatibleDC(target);
  HBITMAP surface = CreateCompatibleBitmap(target, client.right, client.bottom);
  HGDIOBJ previous = SelectObject(canvas, surface);

  PaintContent(canvas);
  BitBlt(target, 0, 0, client.right, client.bottom, canvas, 0, 0, SRCCOPY);

  SelectObject(canvas, previous);
  DeleteObject(surface);
  DeleteDC(canvas);
}

void MainWindow::PaintContent(HDC canvas) const {
  RECT client = {0, 0, kWindowWidth, kWindowHeight};
  FillRectangle(canvas, client, kBackground);

  RECT title_bar = {0, 0, kWindowWidth, kTitleBarHeight};
  FillRectangle(canvas, title_bar, kTitleBar);

  DrawLabel(canvas, header_font_, kHeaderText, kHeaderRect, kWindowTitle,
            DT_LEFT | DT_VCENTER);
  DrawCloseIcon(canvas, kCloseButtonRect, kCloseButtonText);

  const bool playing = track_.state == PlaybackPlaying;
  DrawLabel(canvas, song_font_, kSongText, kSongRect,
            playing ? track_.title : kIdleText,
            DT_LEFT | DT_TOP | DT_END_ELLIPSIS);
  if (playing)
    DrawLabel(canvas, artist_font_, kArtistText, kArtistRect, track_.artist,
              DT_LEFT | DT_TOP | DT_END_ELLIPSIS);
}

void MainWindow::OnLeftClick(LPARAM lp) {
  if (Contains(kCloseButtonRect, PointFromLParam(lp)))
    ShowWindow(window_, SW_HIDE);
}

void MainWindow::OnTrayMessage(WPARAM wp, LPARAM lp) {
  if (wp != kTrayIconId)
    return;

  if (lp == WM_LBUTTONDBLCLK) {
    ShowWindow(window_, SW_SHOW);
    SetForegroundWindow(window_);
  } else if (lp == WM_RBUTTONUP) {
    ShowTrayMenu();
  }
}

void MainWindow::ShowTrayMenu() const {
  HMENU menu = CreatePopupMenu();
  if (!menu)
    return;

  AppendMenuW(menu, MF_STRING, kExitCommand, L"Exit");

  POINT cursor;
  GetCursorPos(&cursor);
  SetForegroundWindow(window_);
  TrackPopupMenu(menu, TPM_RIGHTBUTTON, cursor.x, cursor.y, 0, window_, 0);
  PostMessageW(window_, WM_NULL, 0, 0);

  DestroyMenu(menu);
}

void MainWindow::Quit() {
  ClearNowPlaying();
  DestroyWindow(window_);
}
