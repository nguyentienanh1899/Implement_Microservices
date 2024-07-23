using Basket.API.Services.Interfaces;
using Shared.Configurations;

namespace Basket.API.Services
{
    public class BasketEmailTemplateService : EmailTemplateService, IEmailTemplateService
    {
        public BasketEmailTemplateService(BackgroundScheduledJobSettings scheduledJobSettings) : base(scheduledJobSettings)
        {
        }

        public string GenerateReminderPaymentOrderEmail(string username)
        {
            var orderPaymentLink = $"{BackgroundScheduledJobSettings.ApiGwUrl}/{BackgroundScheduledJobSettings.BasketUrl}/{username}";
            var emailText = ReadEmailTemplateContent("order-payment-reminder");
            var emailSendCustomerText = emailText.Replace("[username]", username).Replace("[orderPaymentLink]", orderPaymentLink);
            return emailSendCustomerText;
        }
    }
}
