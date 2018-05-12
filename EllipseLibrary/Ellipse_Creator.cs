using AbstractClassLibrary;

namespace EllipseLibrary
{
    public class Ellipse_Creator : ICreator
    {
        public Figure Create()
        {
            return new Ellipse();
        }
    }
}
