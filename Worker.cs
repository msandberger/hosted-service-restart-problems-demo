using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;

namespace WorkerService2 {
  public class Worker : BackgroundService {
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IHostLifetime _hostLifetime;
    private readonly ILogger<Worker> _logger;


    public Worker(IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger,
      IHostLifetime hostLifetime) {
      _hostApplicationLifetime = hostApplicationLifetime;
      _logger = logger;
      _hostLifetime = hostLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
      _logger.LogInformation("Worker started");
      await Task.Delay(10000);

      _logger.LogInformation("Stopping application");


      if (_hostLifetime is WindowsServiceLifetime lifetime) {
        // sets the exit code properly (can be seen with `sc query`) but service is still not restarted
        lifetime.ExitCode = 1; 
      }
      else {
        Environment.ExitCode = 1;
      }
        

      _hostApplicationLifetime.StopApplication();
    }
  }
}