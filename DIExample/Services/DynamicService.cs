namespace DIExample.Services
{
    public class DynamicServiceA : IDynamicService
    {
        public string Value()
        {
            return $"{this.GetType().Name}";
        }
    }
    public class DynamicServiceB : IDynamicService
    {
        public string Value()
        {
            return $"{this.GetType().Name}";
        }
    }
    public class DynamicServiceC : IDynamicService
    {
        public string Value()
        {
            return $"{this.GetType().Name}";
        }
    }
    public interface IDynamicService
    {
        string Value();
    }
}
