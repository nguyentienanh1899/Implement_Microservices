using Contracts.ScheduledJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ScheduledJobs.Services
{
    public class HangfireServices : IScheduledJobService
    {

        public string ContinueQueueWith(string parentJobId, Expression<Action> functionCall)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string jobId)
        {
            throw new NotImplementedException();
        }

        public string Enqueue(Expression<Action> functionCall)
        {
            throw new NotImplementedException();
        }

        public string Enqueue<T>(Expression<Action<T>> functionCall)
        {
            throw new NotImplementedException();
        }

        public bool Requeue(string jobId)
        {
            throw new NotImplementedException();
        }

        public string Schedule(Expression<Action> functionCall, TimeSpan delaytime)
        {
            throw new NotImplementedException();
        }

        public string Schedule<T>(Expression<Action<T>> functionCall, TimeSpan delaytime)
        {
            throw new NotImplementedException();
        }

        public string Schedule(Expression<Action> functionCall, DateTimeOffset futureExactTime)
        {
            throw new NotImplementedException();
        }

        public string Schedule<T>(Expression<Action<T>> functionCall, DateTimeOffset futureExactTime)
        {
            throw new NotImplementedException();
        }
    }
}
