using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class FigureCreatorList
    {
        public List<Figure_Creator> Creators = new List<Figure_Creator> {
            new Line_Creator(),
            new Square_Creator(),
            new Circle_Creator(),
            new Ellipse_Creator(),
            new Rhombus_Creator(),
            new Rectangle_Creator()
        };
    }
}
