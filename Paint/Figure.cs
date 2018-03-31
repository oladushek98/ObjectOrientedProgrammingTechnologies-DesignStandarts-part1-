using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    abstract class Figure
    {
        public Point StartPoint { get; set; }
        public Point FinishPoint { get; set; }
        abstract public void Draw(Graphics graphics, Pen pen, Point StartPoint, Point FinishPoint);
    }
}
