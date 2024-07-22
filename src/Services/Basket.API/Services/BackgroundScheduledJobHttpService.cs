using Shared.Configurations;

namespace Basket.API.Services
{
    public class BackgroundScheduledJobHttpService
    {
        public HttpClient Client { get; }

        public BackgroundScheduledJobHttpService(HttpClient client, BackgroundScheduledJobSettings jobSettings)
        {
            client.BaseAddress = new Uri(jobSettings.HangfireUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client = client;

        }
    }
}
