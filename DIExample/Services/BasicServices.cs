namespace DIExample.Services
{
    public class TransientService : ITransientService
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class ScopedService : IScopedService
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class SingletonService : ISingletonService
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public interface ITransientService
    {
        string Value();
    }
    public interface IScopedService
    {
        string Value();
    }
    public interface ISingletonService
    {
        string Value();
    }
}
