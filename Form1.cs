using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NowPlayingSpotify {
    public partial class Form1 : Form {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private readonly Timer timer;
        private bool Drag;
        private bool m_aeroEnabled;
        private int MouseX;
        private int MouseY;

        public Form1() {
            timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += SendCurrentMusicToMsn;
            timer.Start();
            m_aeroEnabled = false;
            InitializeComponent();
            // SendCurrentMusicToMsn(null, null);
        }

        protected override CreateParams CreateParams {
            get {
                m_aeroEnabled = CheckAeroEnabled();

                var cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private void SendCurrentMusicToMsn(object sender, EventArgs e) {
            var songInfo = GetCurrentlyPlayingSong();
            if (songInfo[1] != "Error:") MSNStatus.SetMusic(songInfo[0], songInfo[1], songInfo[2]);
            if (songInfo[1] == "No music playing") MSNStatus.Clear();
            ArtistLabel.Text = songInfo[0];
            MusicLabel.Text = songInfo[1];
        }

        private static List<string> GetCurrentlyPlayingSong() {
            var chromeWindows = new List<IntPtr>();
            Program.EnumWindows((hWnd, lParam) => {
                const int maxClassNameLength = 256;
                var className = new StringBuilder(maxClassNameLength);

                if (Program.GetClassName(hWnd, className, maxClassNameLength) != 0 &&
                    className.ToString() == "Chrome_WidgetWin_0") chromeWindows.Add(hWnd);

                return true;
            }, IntPtr.Zero);

            foreach (var hwnd in chromeWindows.Where(hwnd => hwnd != IntPtr.Zero)) {
                Program.GetWindowThreadProcessId(hwnd, out var pid);

                var processList = Process.GetProcesses();
                if (processList.Where(p => p.Id == pid).All(p => p.ProcessName != "Spotify")) continue;
                const int WM_GETTEXT = 0x000D;
                const int MAX_TEXT_LENGTH = 256;
                var titleBuilder = new StringBuilder(MAX_TEXT_LENGTH);
                var result = Program.SendMessage(hwnd, WM_GETTEXT, (IntPtr)MAX_TEXT_LENGTH,
                    titleBuilder);
                if (result == IntPtr.Zero)
                    throw new Exception("SendMessage error: " + Program.GetLastError());
                var title = titleBuilder.ToString();
                var titleParts = title.Split(new[] { " - " }, StringSplitOptions.None);
                var artist = titleParts.Length > 1 ? titleParts[0] : "";
                var songTitle = titleParts.Length > 1 ? titleParts[1] : titleParts[0];
                var album = titleParts.Length > 2 ? titleParts[2] : "";
                if (songTitle == "Spotify Premium") return new List<string> { "", "No music playing", "" };
                return new List<string> {
                    artist.Trim(), songTitle.Trim(), album.Trim()
                };
            }

            return new List<string> { "Spotify window not found", "Error:", "" };
        }

        private static bool CheckAeroEnabled() {
            if (Environment.OSVersion.Version.Major < 6) return false;
            var enabled = 0;
            Program.DwmIsCompositionEnabled(ref enabled);
            return enabled == 1;
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case WM_NCPAINT:
                    if (m_aeroEnabled) {
                        var v = 2;
                        Program.DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        var margins = new Program.Margins {
                            BottomHeight = 1,
                            LeftWidth = 0,
                            RightWidth = 0,
                            TopHeight = 0
                        };
                        Program.DwmExtendFrameIntoClientArea(Handle, ref margins);
                    }

                    break;
            }

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }

        private void PanelMove_MouseDown(object sender, MouseEventArgs e) {
            Drag = true;
            MouseX = Cursor.Position.X - Left;
            MouseY = Cursor.Position.Y - Top;
        }

        private void PanelMove_MouseMove(object sender, MouseEventArgs e) {
            if (!Drag) return;
            Top = Cursor.Position.Y - MouseY;
            Left = Cursor.Position.X - MouseX;
        }

        private void PanelMove_MouseUp(object sender, MouseEventArgs e) {
            Drag = false;
        }

        private void LabelExit_Click(object sender, EventArgs e) {
            MSNStatus.Clear();
            Environment.Exit(0);
        }

        private void PanelExit_Click(object sender, EventArgs e) {
            MSNStatus.Clear();
            Environment.Exit(0);
        }
    }
}