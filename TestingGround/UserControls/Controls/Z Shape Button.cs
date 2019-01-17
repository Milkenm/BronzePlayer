using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

///
// https://social.msdn.microsoft.com/Forums/en-US/e717df53-dcad-4cde-80cd-8c689b574d00/how-a-user-control-can-be-have-a-abnormal-shape-?forum=winforms\
///

namespace TestingGround.UserControls.Controls
{
    public partial class Z_Shape_Button : Control
    {
        private GraphicsPath graphicsPath;
        Pen p = new Pen(SystemColors.WindowFrame, 2);
        


        public Z_Shape_Button()
        {
            graphicsPath = new GraphicsPath();
            setGraphicsPath();
            this.Region = new Region(graphicsPath);
        }



        private void setGraphicsPath()
        {
            int w = this.Width - 2;
            int h = this.Height - 2;

            
            graphicsPath.AddLine(new Point(2, 2), new Point(w, 2));
            graphicsPath.AddLine(new Point(w, 2), new Point(2, h));
            graphicsPath.AddLine(new Point(2, h), new Point(w, h));

            graphicsPath.Widen(p);
        }

        

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pe.Graphics.TranslateTransform(-1, -1);
            pe.Graphics.DrawPath(p, graphicsPath);
            pe.Graphics.ResetTransform();
            base.OnPaint(pe);
        }
    }
}
