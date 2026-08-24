#include "spotify.h"

#include <windows.h>

#include <tlhelp32.h>

#include "title_parser.h"

namespace {

const int kMaxSpotifyProcesses = 16;
const int kMaxTitleLength = 512;

HWND cached_window = 0;

struct SearchContext {
  const DWORD *pids;
  int pid_count;
  HWND found;
};

bool HasTitle(HWND window) { return GetWindowTextLengthW(window) > 0; }

bool IsCandidateWindow(HWND window) {
  if (!IsWindowVisible(window))
    return false;
  if (GetWindow(window, GW_OWNER))
    return false;
  return HasTitle(window);
}

int CollectSpotifyProcessIds(DWORD *pids, int capacity) {
  HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
  if (snapshot == INVALID_HANDLE_VALUE)
    return 0;

  PROCESSENTRY32W entry;
  entry.dwSize = sizeof(entry);

  int count = 0;
  if (Process32FirstW(snapshot, &entry)) {
    do {
      if (lstrcmpiW(entry.szExeFile, L"Spotify.exe") == 0 && count < capacity)
        pids[count++] = entry.th32ProcessID;
    } while (count < capacity && Process32NextW(snapshot, &entry));
  }

  CloseHandle(snapshot);
  return count;
}

BOOL CALLBACK OnWindowEnumerated(HWND window, LPARAM parameter) {
  SearchContext *context = reinterpret_cast<SearchContext *>(parameter);
  if (!IsCandidateWindow(window))
    return TRUE;

  DWORD owner_pid = 0;
  GetWindowThreadProcessId(window, &owner_pid);
  for (int i = 0; i < context->pid_count; ++i) {
    if (context->pids[i] != owner_pid)
      continue;
    context->found = window;
    return FALSE;
  }
  return TRUE;
}

HWND FindSpotifyWindow() {
  if (cached_window && IsWindow(cached_window) && HasTitle(cached_window))
    return cached_window;

  DWORD pids[kMaxSpotifyProcesses];
  SearchContext context;
  context.pids = pids;
  context.pid_count = CollectSpotifyProcessIds(pids, kMaxSpotifyProcesses);
  context.found = 0;

  if (context.pid_count > 0)
    EnumWindows(OnWindowEnumerated, reinterpret_cast<LPARAM>(&context));

  cached_window = context.found;
  return cached_window;
}

}

void ReadSpotifyTrack(Track *out) {
  *out = Track();

  HWND window = FindSpotifyWindow();
  if (!window)
    return;

  wchar_t title[kMaxTitleLength];
  title[0] = 0;
  GetWindowTextW(window, title, kMaxTitleLength);

  ParseWindowTitle(title, out);
}
