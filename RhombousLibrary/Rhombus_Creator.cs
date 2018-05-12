using AbstractClassLibrary;

namespace RhombousLibrary
{
    public class Rhombus_Creator :  ICreator
    {
        public  Figure Create()
        {
            return new Rhombus();
        }
    }
}
