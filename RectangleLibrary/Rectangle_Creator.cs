using AbstractClassLibrary;

namespace RectangleLibrary
{
    public class Rectangle_Creator : ICreator
    {
        public  Figure Create()
        {
            return new Rectangle();
        }
    }
}
