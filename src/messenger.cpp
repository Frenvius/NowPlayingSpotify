#include "messenger.h"

#include <windows.h>

#include "text.h"

namespace {

const DWORD kNowPlayingMessage = 0x0547;
const UINT kSendTimeoutMs = 1000;
const int kPayloadCapacity = 1024;

const wchar_t kFieldSeparator[] = L"\\0";

void AppendField(wchar_t *payload, const wchar_t *value) {
  Append(payload, kPayloadCapacity, kFieldSeparator);
  Append(payload, kPayloadCapacity, value);
}

void Send(bool enabled, const Track &track) {
  wchar_t payload[kPayloadCapacity];
  payload[0] = 0;

  AppendField(payload, L"Music");
  AppendField(payload, enabled ? L"1" : L"0");
  AppendField(payload, L"{0} - {1}");
  AppendField(payload, track.artist);
  AppendField(payload, track.title);
  AppendField(payload, track.album);
  AppendField(payload, L"");
  Append(payload, kPayloadCapacity, kFieldSeparator);

  COPYDATASTRUCT data;
  data.dwData = kNowPlayingMessage;
  data.cbData = static_cast<DWORD>((Length(payload) + 1) * sizeof(wchar_t));
  data.lpData = payload;

  HWND messenger = 0;
  while ((messenger = FindWindowExW(0, messenger, L"MsnMsgrUIManager", 0)) !=
         0) {
    DWORD_PTR result = 0;
    SendMessageTimeoutW(messenger, WM_COPYDATA, 0,
                        reinterpret_cast<LPARAM>(&data), SMTO_ABORTIFHUNG,
                        kSendTimeoutMs, &result);
  }
}

}

void SendNowPlaying(const Track &track) { Send(true, track); }

void ClearNowPlaying() { Send(false, Track()); }
