namespace DIExample.Services
{
    public class LazyService : ILazyService
    {
        public LazyService()
        {
            System.Diagnostics.Debug.WriteLine($"{this.GetType().Name} Constructor, Random: {Random.Shared.Next(1,20)}");
        }
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class NonLazyService : INonLazyService
    {
        public NonLazyService()
        {
            System.Diagnostics.Debug.WriteLine($"{this.GetType().Name} Constructor, Random: {Random.Shared.Next(1,20)}");
        }
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public interface ILazyService
    {
        string Value();
    }
    public interface INonLazyService
    {
        string Value();
    }
}
