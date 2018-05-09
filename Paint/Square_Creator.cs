using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class Square_Creator : ICreator
    {
        public  Figure Create()
        {
            return new Square();
        }
    }
}
