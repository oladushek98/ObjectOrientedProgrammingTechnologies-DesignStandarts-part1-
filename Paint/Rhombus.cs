using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Rhombus : Figure
    {
        public override void Draw(Graphics graphics, Pen pen, Point StartPoint, Point FinishPoint)
        {
            Point[] points = {StartPoint, FinishPoint, new Point(StartPoint.X, FinishPoint.Y + (FinishPoint.Y - StartPoint.Y)),
                                                       new Point(StartPoint.X - (FinishPoint.X - StartPoint.X), FinishPoint.Y)};
            graphics.DrawPolygon(pen, points);
        }
    }
}
