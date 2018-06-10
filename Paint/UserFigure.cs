using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractClassLibrary;

namespace Paint
{
    class UserFigure : Figure
    {
        public List<Figure> userFigureList = new List<Figure>();
        public static int fieldWidth;
        public static int fieldHeight;

        public override void Draw(Graphics g, Pen pen, Point StartPoint, Point FinishPoint)
        {
            foreach (var figure in userFigureList)
            {
                if (figure != null)
                {
                    float widthDif = (FinishPoint.X - StartPoint.X) / (float)fieldWidth;
                    float heightDif = (FinishPoint.Y - StartPoint.Y) / (float)fieldHeight;
                    figure.StartPoint = CountStartPoint(figure, widthDif, heightDif);
                    figure.FinishPoint = CountFinishPoint(figure, widthDif, heightDif);
                    figure.Draw(g, figure.Pen, figure.StartPoint, figure.FinishPoint);
                }
            }
        }

        public Point CountStartPoint(Figure figure, float widthDif, float heightDif)
        { 
            int tempStartX = (int)(StartPoint.X + figure.FixedStartPoint.X * widthDif);
            int tempStartY = (int)(StartPoint.Y + figure.FixedStartPoint.Y * heightDif);
            return new Point(tempStartX, tempStartY);
        }

        public Point CountFinishPoint(Figure figure, float widthDif, float heightDif)
        {
            int tempFinishX = (int)(StartPoint.X + figure.FixedFinishPoint.X * widthDif);
            int tempFinishY = (int)(StartPoint.Y + figure.FixedFinishPoint.Y * heightDif);
            return new Point(tempFinishX, tempFinishY);
        }

        public override object Clone()
        {
            UserFigure clonedFigure = new UserFigure();
            foreach (var figure in userFigureList)
            {
                clonedFigure.userFigureList.Add((Figure)figure.Clone());
            }
            return clonedFigure;
        }

        public override void Add(List<Figure> list)
        {
            foreach (var figure in userFigureList)
            {
                figure.FixedFinishPoint = figure.FinishPoint;
                figure.FixedStartPoint = figure.StartPoint;
                list.Add(figure);
            }
        }

    }
}
