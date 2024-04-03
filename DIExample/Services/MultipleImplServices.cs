namespace DIExample.Services
{
    public class AService : IMultipleImplServices
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class BService : IMultipleImplServices
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class CService : IMultipleImplServices
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public interface IMultipleImplServices
    {
        string Value();
    }
}
