using AbstractClassLibrary;
using System.Drawing;
using System.Runtime.Serialization;

namespace CircleLibrary
{
    [DataContract]
    public class Circle : Figure
    {
        public override void Draw(Graphics graphics, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Width = FinishPoint.X - StartPoint.X;
            if (((Width > 0) && (FinishPoint.Y < StartPoint.Y)) || ((Width < 0) && (FinishPoint.Y > StartPoint.Y)))
            {
                graphics.DrawEllipse(pen, StartPoint.X, StartPoint.Y, Width, -Width);
            }
            else
            {
                graphics.DrawEllipse(pen, StartPoint.X, StartPoint.Y, Width, Width);
            }
        }

        public override object Clone()
        {
            return (Circle)MemberwiseClone();
        }
    }
}
