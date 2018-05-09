using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    [DataContract]
    class Ellipse : Figure
    {
        public override void Draw(Graphics graphics, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Height = FinishPoint.Y - StartPoint.Y;
            int Width = FinishPoint.X - StartPoint.X;
            graphics.DrawEllipse(pen, StartPoint.X, StartPoint.Y, Width, Height);
        }
    }
}
