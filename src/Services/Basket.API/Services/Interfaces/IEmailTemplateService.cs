﻿namespace Basket.API.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        string GenerateReminderPaymentOrderEmail(string username);
    }
}
