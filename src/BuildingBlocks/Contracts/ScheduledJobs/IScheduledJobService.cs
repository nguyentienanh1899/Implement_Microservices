using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ScheduledJobs
{
    public interface IScheduledJobService
    {
        // Fire-and-Forget Jobs: Fire-and-forget jobs are executed only once and almost immediately after creation.
        string Enqueue(Expression<Action> functionCall);
        string Enqueue<T>(Expression<Action<T>> functionCall);
        // Delayed Jobs: Delayed jobs are executed only once too, but not immediately, after a certain time interval.
        string Schedule(Expression<Action> functionCall, TimeSpan delaytime);
        string Schedule<T>(Expression<Action<T>> functionCall, TimeSpan delaytime);
        string Schedule(Expression<Action> functionCall, DateTimeOffset futureExactTime);
        string Schedule<T>(Expression<Action<T>> functionCall, DateTimeOffset futureExactTime);

        // Continuations: Continuations are executed when its parent job has been finished.
        string ContinueQueueWith(string parentJobId, Expression<Action> functionCall);

        // Recurring Jobs: Recurring jobs fire many times on the specified CRON schedule.
        bool Delete(string jobId);
        bool Requeue(string jobId);
        /* Other:
            + Batches (PRO): Batch is a group of background jobs that is created atomically and considered as a single entity.
            + Batch Continuations(PRO): Batch continuation is fired when all background jobs in a parent batch finished.*/

    }
}
