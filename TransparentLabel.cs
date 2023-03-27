using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace NowPlayingSpotify {
    public class TransparentLabel : Label {
        private int _opacity;

        private Color _transparentBackColor;

        public TransparentLabel() {
            _transparentBackColor = Color.Orange;
            _opacity = 0;
            BackColor = Color.Transparent;
        }

        private int Opacity {
            get => _opacity;
            set {
                if (value >= 0 && value <= 255)
                    _opacity = value;
                Invalidate();
            }
        }

        public Color TransparentBackColor {
            get => _transparentBackColor;
            set {
                _transparentBackColor = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public sealed override Color BackColor {
            get => Color.Transparent;
            set {
                _transparentBackColor = value;
                base.BackColor = Color.Transparent;
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            if (Parent == null) return;
            using (var bmp = new Bitmap(Parent.Width, Parent.Height)) {
                Parent.Controls.Cast<Control>()
                    .Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                    .Where(c => c.Bounds.IntersectsWith(Bounds))
                    .OrderByDescending(c => Parent.Controls.GetChildIndex(c))
                    .ToList()
                    .ForEach(c => c.DrawToBitmap(bmp, c.Bounds));


                e.Graphics.DrawImage(bmp, -Left, -Top);
                using (var b = new SolidBrush(Color.FromArgb(Opacity, TransparentBackColor))) {
                    e.Graphics.FillRectangle(b, ClientRectangle);
                }

                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, Color.Transparent);
            }
        }
    }
}