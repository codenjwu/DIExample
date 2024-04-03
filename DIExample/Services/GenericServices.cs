namespace DIExample.Services
{
    public class StringService : IGenericService<string>
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class IntService : IGenericService<int>
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class GenericService<T> : IGenericService<T>
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public interface IGenericService<T>
    {
        string Value();
    }
}
