﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Square : Figure
    {
        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Width = Math.Abs(FinishPoint.X - StartPoint.X);

            if ((FinishPoint.Y < StartPoint.Y) && (FinishPoint.X < StartPoint.X))
            {
                StartPoint = new Point(FinishPoint.X, StartPoint.Y - Width);
            }
            else
            {
                if ((FinishPoint.Y < StartPoint.Y) && (FinishPoint.X > StartPoint.X))
                {
                    StartPoint = new Point(StartPoint.X, StartPoint.Y - Width);
                }
                else
                {
                    if (FinishPoint.X < StartPoint.X)
                    {
                        StartPoint = new Point(FinishPoint.X, StartPoint.Y);
                    }
                }
            }
            g.DrawRectangle(pen, StartPoint.X, StartPoint.Y, Width, Width);
        }
    }
}
