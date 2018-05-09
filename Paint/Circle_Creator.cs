using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Circle_Creator : ICreator
    {
        public  Figure Create()
        {
            return new Circle();
        }
    }
}
