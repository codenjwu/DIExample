using Microsoft.AspNetCore.Cors.Infrastructure;

namespace DIExample.Services
{
    public class InjectionService : IInjectionService
    {
        ICtorInjectionService _ctorService;
        IServiceProvider _provider;
        IPropInjectionService _propService { get => _provider.GetRequiredService<IPropInjectionService>(); }
        public InjectionService(ICtorInjectionService ctorService, IServiceProvider provider)
        {
            _ctorService = ctorService;
            _provider = provider;
        }
        public string Value()
        {
            return $"{this.GetType().Name},{Environment.NewLine}ctor: {_ctorService.Value()},{Environment.NewLine}, prop: {_propService?.Value()}";
        }
    }
    public class CtorInjectionService : ICtorInjectionService
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class PropInjectionService : IPropInjectionService
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class ActionInjectionService : IActionInjectionService
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public class ViewInjectionService : IViewInjectionService
    {
        public string Value()
        {
            return this.GetType().Name;
        }
    }
    public interface IInjectionService
    {
        string Value();
    }
    public interface ICtorInjectionService
    {
        string Value();
    }
    public interface IPropInjectionService
    {
        string Value();
    }
    public interface IActionInjectionService
    {
        string Value();
    }
    public interface IViewInjectionService
    {
        string Value();
    }
}
