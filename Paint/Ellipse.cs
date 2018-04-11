using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Ellipse : Figure
    {
        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Height = FinishPoint.Y - StartPoint.Y;
            int Width = FinishPoint.X - StartPoint.X;
            g.DrawEllipse(pen, StartPoint.X, StartPoint.Y, Width, Height);
        }
    }
}
