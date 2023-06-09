﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

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
            var version = Application.ProductVersion;
            InitializeComponent();
            VersionLabel.Text = $"v{version} - Check for updates";
            VersionLabel.Click += VersionLabel_Click;
            VersionLabel.Cursor = Cursors.Hand;
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

        private static void VersionLabel_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/Frenvius/NowPlayingSpotify/releases");
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
    }
}