using AbstractClassLibrary;

namespace SquareLibrary
{
    public class Square_Creator : ICreator
    {
        public  Figure Create()
        {
            return new Square();
        }
    }
}
