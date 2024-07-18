using Contracts.ScheduledJobs;
using Hangfire;
using System.Linq.Expressions;

namespace Infrastructure.ScheduledJobs.Services
{
    public class HangfireServices : IScheduledJobService
    {

        public string ContinueQueueWith(string parentJobId, Expression<Action> functionCall) => BackgroundJob.ContinueJobWith(parentJobId, functionCall);

        public bool Delete(string jobId) => BackgroundJob.Delete(jobId);

        public string Enqueue(Expression<Action> functionCall) => BackgroundJob.Enqueue(functionCall);

        public string Enqueue<T>(Expression<Action<T>> functionCall) => BackgroundJob.Enqueue<T>(functionCall);

        public bool Requeue(string jobId) => BackgroundJob.Requeue(jobId);

        public string Schedule(Expression<Action> functionCall, TimeSpan delaytime) => BackgroundJob.Schedule(functionCall, delaytime);

        public string Schedule<T>(Expression<Action<T>> functionCall, TimeSpan delaytime) => BackgroundJob.Schedule<T>(functionCall, delaytime);

        public string Schedule(Expression<Action> functionCall, DateTimeOffset futureExactTime) => BackgroundJob.Schedule(functionCall, futureExactTime);

        public string Schedule<T>(Expression<Action<T>> functionCall, DateTimeOffset futureExactTime) => BackgroundJob.Schedule<T>(functionCall, futureExactTime);
    }
}
