using System;
using System.Windows.Forms;
using Microsoft.Win32;
using NowPlayingSpotify.Properties;

namespace NowPlayingSpotify {
    public partial class AppSettings : Form {
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

        public AppSettings() {
            InitializeComponent();
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

        private void Close_Settings(object sender, EventArgs e) {
            Close();
        }

        private void ToggleWithWindowsSetting(object sender, EventArgs e) {
            var checkbox = (CheckBox)sender;
            var value = checkbox.Checked;
            var appPath = Application.ExecutablePath;
            Settings.Default.RunOnStartup = value;
            Settings.Default.Save();
            var rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (value)
                rkApp?.SetValue("NowPlayingSpotify", appPath);
            else
                rkApp?.DeleteValue("NowPlayingSpotify", false);
        }
    }
}