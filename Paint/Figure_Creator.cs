using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    // change absctract class to interface
    /*public abstract class Figure_Creator : ICreator
    {
        public abstract Figure Create();
    }*/

    public interface ICreator
    {
         Figure Create();
    }
}
