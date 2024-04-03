namespace DIExample.Services
{
    public class ParamService : IParamService
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public interface IParamService
    {
        string Value();
    }
}
