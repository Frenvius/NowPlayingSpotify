#include <windows.h>

#include "window.h"

namespace {
const wchar_t kSingleInstanceMutex[] = L"NowPlayingSpotify.SingleInstance";

bool AlreadyRunning() {
  CreateMutexW(0, TRUE, kSingleInstanceMutex);
  return GetLastError() == ERROR_ALREADY_EXISTS;
}
}

int WINAPI wWinMain(HINSTANCE instance, HINSTANCE, LPWSTR, int) {
  if (AlreadyRunning())
    return 0;

  MainWindow window;
  if (!window.Create(instance))
    return 1;

  MSG message;
  while (GetMessageW(&message, 0, 0, 0) > 0) {
    TranslateMessage(&message);
    DispatchMessageW(&message);
  }

  return static_cast<int>(message.wParam);
}
