#include "startup.h"

#include <windows.h>

#include "text.h"

namespace {

const wchar_t kRunKey[] =
    L"Software\\Microsoft\\Windows\\CurrentVersion\\Run";
const wchar_t kValueName[] = L"NowPlayingSpotify";
const int kCommandCapacity = MAX_PATH + 2;

bool StartupCommand(wchar_t *out, int capacity) {
  wchar_t path[MAX_PATH];
  const DWORD written = GetModuleFileNameW(0, path, MAX_PATH);
  if (written == 0 || written >= MAX_PATH)
    return false;

  out[0] = 0;
  Append(out, capacity, L"\"");
  Append(out, capacity, path);
  Append(out, capacity, L"\"");
  return true;
}

}

bool IsStartupEnabled() {
  wchar_t expected[kCommandCapacity];
  if (!StartupCommand(expected, kCommandCapacity))
    return false;

  HKEY key;
  if (RegOpenKeyExW(HKEY_CURRENT_USER, kRunKey, 0, KEY_QUERY_VALUE, &key) !=
      ERROR_SUCCESS)
    return false;

  wchar_t stored[kCommandCapacity];
  DWORD type = 0;
  DWORD size = sizeof(stored);
  const LONG result = RegQueryValueExW(key, kValueName, 0, &type,
                                       reinterpret_cast<BYTE *>(stored), &size);
  RegCloseKey(key);

  if (result != ERROR_SUCCESS || type != REG_SZ)
    return false;

  int chars = static_cast<int>(size / sizeof(wchar_t));
  if (chars < 1 || chars >= kCommandCapacity)
    chars = kCommandCapacity - 1;
  stored[chars] = 0;

  return Equals(stored, expected);
}

void SetStartupEnabled(bool enabled) {
  HKEY key;
  if (RegOpenKeyExW(HKEY_CURRENT_USER, kRunKey, 0, KEY_SET_VALUE, &key) !=
      ERROR_SUCCESS)
    return;

  if (!enabled) {
    RegDeleteValueW(key, kValueName);
    RegCloseKey(key);
    return;
  }

  wchar_t command[kCommandCapacity];
  if (StartupCommand(command, kCommandCapacity))
    RegSetValueExW(
        key, kValueName, 0, REG_SZ, reinterpret_cast<const BYTE *>(command),
        static_cast<DWORD>((Length(command) + 1) * sizeof(wchar_t)));

  RegCloseKey(key);
}
