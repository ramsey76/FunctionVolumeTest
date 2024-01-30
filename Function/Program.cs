using FunctionVolumeTest.Function;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FunctionVolumeTest.Function.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s => {
        s.AddTransient<MessageHandler>();
    })
    .Build();

host.Run();
