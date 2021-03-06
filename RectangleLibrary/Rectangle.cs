﻿using AbstractClassLibrary;
using System;
using System.Drawing;
using System.Runtime.Serialization;


namespace RectangleLibrary
{
    [DataContract]
    public class Rectangle : Figure
    {
        public override void Draw(Graphics graphics, Pen pen, Point StartPoint, Point FinishPoint)
        {
            int Height = Math.Abs(FinishPoint.Y - StartPoint.Y);
            int Width = Math.Abs(FinishPoint.X - StartPoint.X);
            if (FinishPoint.Y < StartPoint.Y)
            {
                StartPoint = new Point(StartPoint.X, FinishPoint.Y);
            }
            if (FinishPoint.X < StartPoint.X)
            {
                StartPoint = new Point(FinishPoint.X, StartPoint.Y);
            }
            graphics.DrawRectangle(pen, StartPoint.X, StartPoint.Y, Width, Height);
        }

        public override object Clone()
        {
            return (Rectangle)MemberwiseClone();
        }
    }
}

