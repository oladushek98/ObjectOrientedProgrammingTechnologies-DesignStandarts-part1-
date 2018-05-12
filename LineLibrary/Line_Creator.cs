using AbstractClassLibrary;

namespace LineLibrary
{
    public class Line_Creator : ICreator
    {
        public  Figure Create()
        {
            return new Line();
        }
    }
}
