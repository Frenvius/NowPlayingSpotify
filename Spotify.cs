using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NowPlayingSpotify {
    public abstract class Spotify {
        private const int _GET_SPOTIFY_RETURN_LAST_SEC = 5;

        private static DateTime _lastGetSpotifyCall = DateTime.MinValue;
        private static int _cachedProcId;
        private static IntPtr _cachedHWnd;

        private static IntPtr GetSpotify() {
            if (DateTime.Now.Subtract(_lastGetSpotifyCall).TotalSeconds < _GET_SPOTIFY_RETURN_LAST_SEC)
                return _cachedHWnd;

            _lastGetSpotifyCall = DateTime.Now;
            var rv = IntPtr.Zero;
            var processesByName = Process.GetProcessesByName("spotify");

            if (Array.Exists(processesByName, proc => proc.Id == _cachedProcId))
                return _cachedHWnd;

            foreach (var proc in processesByName)
            foreach (ProcessThread thread in proc.Threads) {
                Win32.EnumThreadWindows(thread.Id, (IntPtr hWnd, IntPtr lParam) => {
                    var sb = new StringBuilder(256);
                    var ret = Win32.GetClassName(hWnd, sb, sb.Capacity);
                    if (ret == 0) return true;
                    if (sb.ToString() != "Chrome_WidgetWin_0") return true;
                    ret = Win32.GetWindowText(hWnd, sb, sb.Capacity);
                    if (ret == 0) return true;
                    if (string.IsNullOrEmpty(sb.ToString())) return true;
                    rv = hWnd;

                    _cachedProcId = proc.Id;
                    _cachedHWnd = hWnd;

                    return false;
                }, IntPtr.Zero);

                if (rv != IntPtr.Zero) return rv;
            }

            return rv;
        }

        private static bool IsRunning() {
            return GetSpotify() != IntPtr.Zero;
        }

        public static List<string> GetCurrentSong() {
            if (!IsRunning())
                return new List<string> { "Spotify window not found", "Error:", "" };

            var hWnd = GetSpotify();
            var length = Win32.GetWindowTextLength(hWnd);
            var sb = new StringBuilder(length + 1);
            Win32.GetWindowText(hWnd, sb, sb.Capacity);

            var title = sb.ToString();

            if (string.IsNullOrEmpty(title) || title == "Spotify")
                return new List<string> { "Spotify window not found", "Error:", "" };
            var portions = title.Split(new[] { " - " }, StringSplitOptions.None);

            var song = portions.Length > 1 ? portions[1] : "";
            var artist = portions[0];
            var album = portions.Length > 2
                ? string.Join(" ", portions.Skip(2).ToArray())
                : "";

            if (song == "Spotify Premium" || artist == "Spotify Premium")
                return new List<string> { "", "No music playing", "" };

            return new List<string> { artist, song, album };
        }
    }
}