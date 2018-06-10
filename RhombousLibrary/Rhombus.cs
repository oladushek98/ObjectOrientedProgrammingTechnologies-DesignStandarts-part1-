using AbstractClassLibrary;
using System.Drawing;
using System.Runtime.Serialization;

namespace RhombousLibrary
{
    [DataContract]
    public class Rhombus : Figure
    {
        public override void Draw(Graphics graphics, Pen pen, Point StartPoint, Point FinishPoint)
        {
            Point[] points = {StartPoint, FinishPoint, new Point(StartPoint.X, FinishPoint.Y + (FinishPoint.Y - StartPoint.Y)),
                                                       new Point(StartPoint.X - (FinishPoint.X - StartPoint.X), FinishPoint.Y)};
            graphics.DrawPolygon(pen, points);
        }

        public override object Clone()
        {
            return (Rhombus)MemberwiseClone();
        }
    }
}
