using Contracts.ScheduledJobs;

namespace Hangfire.API.Services.Interfaces
{
    public interface IBackgroundJobService
    {
        IScheduledJobService ScheduledJob { get; }
        string? SendEmailContent(string email, string subject, string emailContent, DateTimeOffset futureExactTime);
    }
}
