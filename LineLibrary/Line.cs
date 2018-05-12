using AbstractClassLibrary;
using System.Drawing;
using System.Runtime.Serialization;

namespace LineLibrary
{
    [DataContract]
    public class Line : Figure
    {
        public override void Draw(Graphics graphics, Pen pen, Point StartPoint, Point FinishPoint)
        {
            graphics.DrawLine(pen, StartPoint, FinishPoint);
        }
    }
}
