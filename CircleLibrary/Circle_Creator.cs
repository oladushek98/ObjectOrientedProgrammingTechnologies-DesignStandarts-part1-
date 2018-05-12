using AbstractClassLibrary;

namespace CircleLibrary
{
   public class Circle_Creator : ICreator
    {
        public  Figure Create()
        {
            return new Circle();
        }
    }
}
