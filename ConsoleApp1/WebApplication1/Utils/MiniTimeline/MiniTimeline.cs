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
        private readonly IServiceProvider _serviceProvider;

        public MiniTimeline(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                UpdateTimeline();
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        public void UpdateTimeline()
        {
            Debug.WriteLine("Update after 5 seconds");
        }
    }
}
