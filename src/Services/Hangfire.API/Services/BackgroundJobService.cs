using Contracts.ScheduledJobs;
using Contracts.Services;
using Hangfire.API.Services.Interfaces;
using Shared.Services.Email;
using ILogger = Serilog.ILogger;

namespace Hangfire.API.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IScheduledJobService _scheduledJobService;
        private readonly ISmtpEmailService _smtpEmailService;
        private readonly ILogger _logger;

        public BackgroundJobService(IScheduledJobService scheduledJobService, ISmtpEmailService smtpEmailService, ILogger logger)
        {
            _logger = logger;
            _scheduledJobService = scheduledJobService;
            _smtpEmailService = smtpEmailService;
        }

        public IScheduledJobService ScheduledJob => _scheduledJobService;

        public string? SendEmailContent(string email, string subject, string emailContent, DateTimeOffset futureExactTime)
        {
            var emailRequest = new MailRequest
            {
                ToAddress = email,
                Body = emailContent,
                Subject = subject,
            };

            try
            {
                var jobId = _scheduledJobService.Schedule(() => _smtpEmailService.SendEmail(emailRequest), futureExactTime);
                _logger.Information($"Send email to {email} with subject: {subject} - job id: {jobId}");
                return jobId;
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed due to an error with the email service: {ex.Message}");
            }
            return null;
        }
    }
}
