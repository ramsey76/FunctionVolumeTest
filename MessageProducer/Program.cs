using FunctionVolumeTest.MessageProducer;
using FunctionVolumeTest.MessageProducer.ResourceAccess.ServiceBus;
using FunctionVolumeTest.MessageProducer.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MessageProducer;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);
        
        builder.ConfigureServices((hostContext,services) => {
            services.Configure<MessageBusSettings>(hostContext.Configuration.GetSection("MessageBusSettings"));
            services.AddSingleton<ServiceBus>();
            services.AddSingleton<Producer>();
        });

        IHost host = builder.Build();
        var service = host.Services.GetRequiredService<Producer>();
        await service.Produce();
    }
}
