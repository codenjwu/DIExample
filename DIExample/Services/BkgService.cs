
using Microsoft.Extensions.Options;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;

namespace DIExample.Services
{
    public class BkgService : BackgroundService
    {
        public readonly Channel<DateTime> _queue;
        IServiceScopeFactory _serviceScope;
        public BkgService(IServiceScopeFactory serviceScope)
        {
            _queue = Channel.CreateUnbounded<DateTime>();
            _serviceScope = serviceScope;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while (await _queue.Reader.WaitToReadAsync(stoppingToken))
                    while (_queue.Reader.TryRead(out DateTime dt))
                    {
                        using (var scope = _serviceScope.CreateScope())
                        {
                            await Task.Delay(500);
                            var service = scope.ServiceProvider.GetRequiredService<BackgroundScopedService>();
                            System.Diagnostics.Debug.WriteLine(service.Value(dt));
                        }
                    }
            }
        }
        public void Enqueue(DateTime dt) { _queue.Writer.TryWrite(dt); }
    }
    public class BackgroundScopedService
    {
        public string Value(DateTime val)
        {
            return $"{this.GetType().Name}, {val}";
        }
    }
}
