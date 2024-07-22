using Basket.API.Services.Interfaces;
using Shared.Configurations;

namespace Basket.API.Services
{
    public class BasketEmailTemplateService : EmailTemplateService, IEmailTemplateService
    {
        public BasketEmailTemplateService(BackgroundScheduledJobSettings scheduledJobSettings) : base(scheduledJobSettings)
        {
        }

        public string GenerateReminderPaymentOrderEmail(string username, string paymentOrderUrl = "basket/checkout")
        {
            var orderPaymentLink = $"{BackgroundScheduledJobSettings.ApiGwUrl}/{paymentOrderUrl}/{username}";
            var emailText = ReadEmailTemplateContent("order-payment-reminder");
            var emailSendCustomerText = emailText.Replace("[username]", username).Replace("[orderPaymentLink]", orderPaymentLink);
            return emailSendCustomerText;
        }
    }
}
