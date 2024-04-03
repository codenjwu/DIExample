using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DIExample.Services
{
    public class OkAttribute : ActionFilterAttribute,IActionFilter
    {
        IScopedService _scopedService;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            #region option 1: 
            var provider = context.HttpContext.RequestServices;
            _scopedService = provider.GetRequiredService<IScopedService>();
            context.HttpContext.Items.Add("ok", _scopedService.Value());
            #endregion
            
        }
    }
    public class ServiceAttribute : ActionFilterAttribute, IActionFilter
    {
        ITransientService _transientService;
        public ServiceAttribute(ITransientService transientService)
        {
            _transientService = transientService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            #region option 2: 
            context.HttpContext.Items.Add("service", _transientService.Value());
            #endregion
        }
         
    }
    public class TypeAttribute : ActionFilterAttribute, IActionFilter
    {
        ISingletonService _singletonService;
        public TypeAttribute(ISingletonService singletonService)
        {
            _singletonService = singletonService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            #region option 3: 
            context.HttpContext.Items.Add("type", _singletonService.Value());
            #endregion

        }
    }
}