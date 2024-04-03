using DIExample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DIController : ControllerBase
    {
        IInjectionService _injectionService;
        INonLazyService _nonlazyService;
        Lazy<ILazyService> _lazyService;
        IGenericService<string> _stringService;
        IGenericService<int> _intService;
        IGenericService<double> _doubleService;
        Func<string, IDynamicService> _dynamicServiceFactory;
        IMultipleCtorService _multipleCtorService;
        Func<string, string, IMultipleCtorService> _multipleCtorService3;
        Func<string, IMultipleCtorService> _multipleCtorService2;
        Func<IMultipleCtorService> _multipleCtorService1;
        StaticScopedService _staticScopedService;
        StaticTransientService _staticTransientService;
        BkgService _bkgService;
        public DIController(
            IInjectionService commonService,
            INonLazyService nonlazyService,
            Lazy<ILazyService> lazyService,
            IGenericService<string> stringService,
            IGenericService<int> intService,
            IGenericService<double> doubleService,
            Func<string, IDynamicService> dynamicServiceFactory,
            IMultipleCtorService multipleCtorService,
            Func<string, string, IMultipleCtorService> multipleCtorService3,
            Func<string, IMultipleCtorService> multipleCtorService2,
            Func<IMultipleCtorService> multipleCtorService1,
            StaticScopedService staticScopedService,
            StaticTransientService staticTransientService,
            BkgService bkgService
            )
        {
            _injectionService = commonService;
            _nonlazyService = nonlazyService;
            _lazyService = lazyService;
            _stringService = stringService;
            _intService = intService;
            _doubleService = doubleService;
            _dynamicServiceFactory = dynamicServiceFactory;
            _multipleCtorService = multipleCtorService;
            _multipleCtorService3 = multipleCtorService3;
            _multipleCtorService2 = multipleCtorService2;
            _multipleCtorService1 = multipleCtorService1;
            _staticScopedService = staticScopedService;
            _staticTransientService = staticTransientService;
            _bkgService = bkgService;
        }

        [HttpGet]
        public IActionResult Get() { return Ok(); }

        [HttpGet(nameof(Injection))]
        public IActionResult Injection()
        {
            return Ok(_injectionService.Value());
        }

        [HttpGet(nameof(Action))]
        public IActionResult Action([FromServices] IActionInjectionService service)
        {
            return Ok(service.Value());
        }

        [HttpGet(nameof(Lazy))]
        public IActionResult Lazy()
        {
            return Ok(_lazyService.Value.Value());
        }

        [HttpGet(nameof(Singleton))]
        public IActionResult Singleton()
        {
            return Ok();
        }

        [HttpGet(nameof(Transient))]
        public IActionResult Transient()
        {
            return Ok();
        }

        [HttpGet(nameof(Scoped))]
        public IActionResult Scoped()
        {
            return Ok();
        }

        [HttpGet(nameof(Generic))]
        public IActionResult Generic()
        {
            return Ok($"{_stringService.Value()}, {_intService.Value()}, {_doubleService.Value()}");
        }

        [HttpGet(nameof(MultipleImpl))]
        public IActionResult MultipleImpl([FromServices] IServiceProvider provider)
        {
            var services = provider.GetServices<IMultipleImplServices>();

            //return Ok($"{services.First(x => x.GetType().Name == "BService").Value()}");
            return Ok($"{provider.GetRequiredKeyedService<IMultipleImplServices>(3).Value()}");
        }

        [HttpGet(nameof(Backgroud))]
        public IActionResult Backgroud()
        {
            return Ok();
        }

        [HttpGet(nameof(MultipleCtor))]
        public IActionResult MultipleCtor()
        {
            return Ok($"{_multipleCtorService.Value()}, {_multipleCtorService1.Invoke().Value()}, {_multipleCtorService2.Invoke("par2").Value()}, {_multipleCtorService3.Invoke("par3","par3").Value()}");
        }

        [HttpGet(nameof(Filter))]
        [Ok]
        [ServiceFilter(typeof(ServiceAttribute))]
        [TypeFilter(typeof(TypeAttribute))]
        public IActionResult Filter()
        {
            return Ok(HttpContext.Items["ok"]!.ToString() + ", " + HttpContext.Items["service"]!.ToString() + ", " + HttpContext.Items["type"]!.ToString());
        }

        [HttpGet(nameof(Static))]
        public IActionResult Static(string value)
        {
            return Ok(StaticClass.ScopedValue());
        }

        [HttpGet(nameof(Background))]
        public IActionResult Background()
        {
            _bkgService.Enqueue(DateTime.Now);
            return Ok();
        }

        [HttpGet(nameof(Middleware))]
        public IActionResult Middleware()
        {
            return Ok(HttpContext.Items["middleware"]);
        }

        [HttpGet(nameof(Dynamic))]
        public IActionResult Dynamic()
        {
            return Ok(_dynamicServiceFactory.Invoke("B").Value());
        }

        [HttpGet(nameof(ServiceScopeFactory))]
        public IActionResult ServiceScopeFactory()
        {
            return Ok();
        }

        [HttpGet(nameof(ServiceCollection))]
        public IActionResult ServiceCollection()
        {
            return Ok();
        }

        [HttpGet(nameof(ServiceProvider))]
        public IActionResult ServiceProvider(IServiceProvider p)
        {
            return Ok();
        }

        [HttpGet(nameof(ServiceDescriptor))]
        public IActionResult ServiceDescriptor()
        {
            return Ok();
        }
    }
}
