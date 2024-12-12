using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;

namespace BookSearch.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IRecurringJobManager _recurringJobs;
        public Worker(IRecurringJobManager recurringJobs)
        {
            _recurringJobs = recurringJobs;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _recurringJobs.AddOrUpdate<Jobs.BookUpsertJob>("book-upsert", job => job.Run(), "0 * * * *");
            return Task.CompletedTask;
        }
    }
}
