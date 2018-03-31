using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
     class FigureList
    {
         public List<Figure> Figures = new List<Figure>
        {
            new Circle(),
            new Rhombus(),
            new Square(),
            new Rectangle(),
            new Ellipse(),
            new Line()
        };

        public List<Figure> ReadyFigures = new List<Figure>
        {

        };
    }
}
