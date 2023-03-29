using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NowPlayingSpotify {
    public partial class Main : Form {
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private readonly NotifyIcon _notifyIcon;
        private readonly Timer timer;
        private bool Drag;
        private bool m_aeroEnabled;
        private int MouseX;
        private int MouseY;

        public Main() {
            timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += SendCurrentMusicToMsn;
            timer.Start();
            m_aeroEnabled = false;
            var resources = new ComponentResourceManager(typeof(Main));

            _notifyIcon = new NotifyIcon {
                Icon = (Icon)resources.GetObject("$this.Icon"),
                Text = "Now Playing",
                Visible = true
            };

            var contextMenu = new ContextMenu();
            var exitMenuItem = new MenuItem("Exit");
            exitMenuItem.Click += ExitMenuItem_Click;
            contextMenu.MenuItems.Add(exitMenuItem);
            _notifyIcon.ContextMenu = contextMenu;

            InitializeComponent();
            SendCurrentMusicToMsn(null, null);
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
            var songInfo = Spotify.GetCurrentSong();
            if (songInfo[1] != "Error:") MsnStatus.SetMusic(songInfo[0], songInfo[1], songInfo[2]);
            if (songInfo[1] == "No music playing") MsnStatus.Clear();
            ArtistLabel.Text = songInfo[0];
            MusicLabel.Text = songInfo[1];
        }

        private static bool CheckAeroEnabled() {
            if (Environment.OSVersion.Version.Major < 6) return false;
            var enabled = 0;
            Win32.DwmIsCompositionEnabled(ref enabled);
            return enabled == 1;
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case WM_NCPAINT:
                    if (m_aeroEnabled) {
                        var v = 2;
                        Win32.DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        var margins = new Win32.Margins {
                            BottomHeight = 1,
                            LeftWidth = 0,
                            RightWidth = 0,
                            TopHeight = 0
                        };
                        Win32.DwmExtendFrameIntoClientArea(Handle, ref margins);
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

        private void MinimizeToTray(object sender, EventArgs e) {
            Hide();
            _notifyIcon.Visible = true;
            _notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
        }

        private static void ExitMenuItem_Click(object sender, EventArgs e) {
            MsnStatus.Clear();
            Environment.Exit(0);
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
            Show();
        }

        private void Open_Settings(object sender, EventArgs e) {
            var settings = new AppSettings();
            settings.Show();
            settings.Location = new Point(Location.X, Location.Y + Height);
            Enabled = false;
            settings.FormClosed += (o, args) => Enabled = true;
        }
    }
}