using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Application.Services
{
    public class WorkerService: IHostedService
    {
        private Timer _timer;
        private HttpClient client = new HttpClient();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DeleteQuotes, null, TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Method for removing a quote
        /// </summary>
        /// <returns></returns>
        public void DeleteQuotes(object state)
        {
            client.DeleteAsync("http://localhost:5000/Quotes/clear");
        }
    }
}
