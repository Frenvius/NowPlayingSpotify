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
        
        private static IntPtr GetSpotifyByProcess() {
            foreach (var process in Process.GetProcessesByName("Spotify")) {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                    return process.MainWindowHandle;
            }
            return IntPtr.Zero;
        }

        private static IntPtr GetSpotify() {
            if (DateTime.Now.Subtract(_lastGetSpotifyCall).TotalSeconds < _GET_SPOTIFY_RETURN_LAST_SEC)
                return _cachedHWnd;

            _lastGetSpotifyCall = DateTime.Now;

            const string windowClassName = "Chrome_WidgetWin_0";
            var rv = Win32.FindWindow(windowClassName, "Spotify");

            if (rv == IntPtr.Zero) {
                rv = GetSpotifyByProcess();
            }

            if (rv == IntPtr.Zero) return rv;
            var proc = Process.GetProcessById(_cachedProcId);
            _cachedProcId = proc.Id;
            _cachedHWnd = rv;

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