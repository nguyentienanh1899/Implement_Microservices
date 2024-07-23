using Hangfire.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.ScheduledJobs;
using System.ComponentModel.DataAnnotations;

namespace Hangfire.API.Controllers
{
    [Route("api/scheduled-jobs")]
    [ApiController]
    public class ScheduledJobsController : ControllerBase
    {
        private readonly IBackgroundJobService _jobService;
        public ScheduledJobsController(IBackgroundJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        [Route("send-email-reminder-payment-order")]
        public IActionResult SendEmailReminderPaymentOrder([FromBody] ReminderPaymentOrderDto model)
        {
            var jobId = _jobService.SendEmailContent(model.email, model.subject, model.emailContent, model.futureExactTime);
            return Ok(jobId);
        }

        [HttpDelete]
        [Route("delete/jobId/{id}")]
        public IActionResult DeleteJobId([Required]string id)
        {
            var result = _jobService.ScheduledJob.Delete(id);
            return Ok(result);
        }
    }
}
