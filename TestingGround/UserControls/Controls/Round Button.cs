using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TestingGround.UserControls.Controls
{
    public partial class RoundButton : Control
    {
        public RoundButton()
        {
            InitializeComponent();
            this.Size = new Size(30, 30);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            GraphicsPath graphicspath = new GraphicsPath();
            graphicspath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new Region(graphicspath);
            base.OnPaint(pe);
        }
    }
}
