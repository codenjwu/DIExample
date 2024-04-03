using Microsoft.Extensions.DependencyInjection;

namespace DIExample.Services
{
    public static class StaticClass
    {
        static IStaticService _staticService;
        public static string TransientValue(string val) => _staticService.Value(val);
        public static string ScopedValue() =>
          _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<StaticScopedService>().Value(_httpContextAccessor.HttpContext!.Request.QueryString.Value!); 

        static IHttpContextAccessor _httpContextAccessor; 
        public static void Init(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
    public class StaticTransientService : IStaticService
    {
        public string Value(string val)
        {
            return this.GetType().Name + $" {val}";
        }
    }
    public class StaticScopedService : IStaticService
    {
        public string Value(string val)
        {
            return this.GetType().Name + $" {val}";
        }
    }
    public interface IStaticService
    {
        string Value(string val);
    }
}
