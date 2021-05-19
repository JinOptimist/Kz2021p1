using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WebApplication1.Utils.MiniTimeline
{
    public class MiniTimeline : BackgroundService
    {
        private readonly AirportUpdate _airportUpdate;

        public MiniTimeline(IServiceProvider serviceProvider)
        {
            _airportUpdate = new AirportUpdate(serviceProvider);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateTimeline();
                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }

        public async Task UpdateTimeline()
        {
            await Task.WhenAll(
                Task.Run(() => _airportUpdate.AirportStateUpdater())
                );
        }
    }
}
