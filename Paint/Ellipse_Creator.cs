using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Ellipse_Creator : Figure_Creator
    {
        public override Figure Create()
        {
            return new Ellipse();
        }
    }
}
