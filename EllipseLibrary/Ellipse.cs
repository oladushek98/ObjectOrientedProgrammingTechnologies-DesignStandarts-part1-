using AbstractClassLibrary;
using System.Drawing;
using System.Runtime.Serialization;

namespace EllipseLibrary
{
    [DataContract]
    public class Ellipse : Figure
    {
        public override void Draw(Graphics graphics, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Height = FinishPoint.Y - StartPoint.Y;
            int Width = FinishPoint.X - StartPoint.X;
            graphics.DrawEllipse(pen, StartPoint.X, StartPoint.Y, Width, Height);
        }

        public override object Clone()
        {
            return (Ellipse)MemberwiseClone();
        }
    }
}
