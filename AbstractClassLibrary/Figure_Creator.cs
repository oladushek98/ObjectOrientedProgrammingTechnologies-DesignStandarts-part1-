namespace AbstractClassLibrary
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
