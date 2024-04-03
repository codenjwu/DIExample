
using DIExample.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DIExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region Transient, Scoped, Singleton
            builder.Services.AddTransient<ITransientService, TransientService>();
            builder.Services.AddScoped<IScopedService, ScopedService>();
            builder.Services.AddSingleton<ISingletonService, SingletonService>();
            #endregion
            #region Lazy loading
            builder.Services.AddTransient<INonLazyService, NonLazyService>();
            builder.Services.AddTransient<ILazyService, LazyService>();
            builder.Services.AddTransient<Lazy<ILazyService>>(provider => new Lazy<ILazyService>(() => provider.GetRequiredService<ILazyService>()));
            #endregion
            #region Generic
            builder.Services.AddTransient<IGenericService<string>, StringService>();
            builder.Services.AddTransient<IGenericService<int>, IntService>();
            builder.Services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
            //builder.Services.TryAdd(ServiceDescriptor.Transient(typeof(IGenericService<>), typeof(GenericService<>)));
            #endregion
            #region Multiple implementations
            builder.Services.AddTransient<IMultipleImplServices, AService>();
            builder.Services.AddTransient<IMultipleImplServices, BService>();
            builder.Services.AddKeyedTransient<IMultipleImplServices, CService>(3);
            #endregion
            #region Constructor, Property, Action, View injection
            builder.Services.AddTransient<IInjectionService, InjectionService>();
            builder.Services.AddTransient<ICtorInjectionService, CtorInjectionService>();
            builder.Services.AddTransient<IPropInjectionService, PropInjectionService>();
            builder.Services.AddTransient<IActionInjectionService, ActionInjectionService>();
            builder.Services.AddTransient<IViewInjectionService, ViewInjectionService>();
            #endregion
            #region Dynamic
            builder.Services.AddKeyedTransient<IDynamicService, DynamicServiceA>("A");
            builder.Services.AddKeyedTransient<IDynamicService, DynamicServiceB>("B");
            builder.Services.AddKeyedTransient<IDynamicService, DynamicServiceC>("C");
            builder.Services.AddTransient<Func<string, IDynamicService>>(provider => key =>
            {
                switch (key)
                {
                    case "A":
                        return provider.GetRequiredKeyedService<IDynamicService>("A");
                    case "B":
                        return provider.GetRequiredKeyedService<IDynamicService>("B");
                    case "C":
                        return provider.GetRequiredKeyedService<IDynamicService>("C");
                    default:
                        throw new NotImplementedException();
                }
            });
            #endregion
            #region Multiple constructors, Pass parameter to constructor
            builder.Services.AddTransient<IMultipleCtorService>(provider =>
            {
                //return ActivatorUtilities.CreateInstance<MultipleCtorService>(provider);
                //return ActivatorUtilities.CreateInstance<MultipleCtorService>(provider, "A");
                return ActivatorUtilities.CreateInstance<MultipleCtorService>(provider, "A", "B");
            });
            builder.Services.AddTransient<Func<string, string, IMultipleCtorService>>(provider =>
                (par1, par2) => ActivatorUtilities.CreateInstance<MultipleCtorService>(provider, par1, par2)
             );
            builder.Services.AddTransient<Func<string, IMultipleCtorService>>(provider =>
                   (par1) => ActivatorUtilities.CreateInstance<MultipleCtorService>(provider, par1)
                );
            builder.Services.AddTransient<Func<IMultipleCtorService>>(provider =>
                () => ActivatorUtilities.CreateInstance<MultipleCtorService>(provider)
             );
            #endregion
            #region Singleton, Static, BackgroundService, Middleware, Filter, Attribute
            builder.Services.AddScoped<ServiceAttribute>();
            builder.Services.AddScoped<StaticScopedService>();
            builder.Services.AddScoped<StaticTransientService>();
            //builder.Services.AddHostedService<BkgService>();
            // if want to resolve background service from controller:
            builder.Services.AddSingleton<BkgService>();
            builder.Services.AddHostedService<BkgService>(provider => provider.GetService<BkgService>()!);
            builder.Services.AddScoped<BackgroundScopedService>();
            #endregion

            var app = builder.Build();
            StaticClass.Init(app.Services.GetRequiredService<IHttpContextAccessor>());
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<OkMiddleware>();
            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}
