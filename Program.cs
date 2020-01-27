using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerService2 {
  public class Program {
    public static async Task Main(string[] args) {
      using var host = CreateHostBuilder(args).Build();

      await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) {
      return Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .ConfigureServices((hostContext, services) => { services.AddHostedService<Worker>(); });
    }
  }
}