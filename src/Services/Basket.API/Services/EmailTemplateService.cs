using Shared.Configurations;
using System.Text;

namespace Basket.API.Services
{
    public class EmailTemplateService
    {
        protected readonly BackgroundScheduledJobSettings BackgroundScheduledJobSettings;
        public EmailTemplateService(BackgroundScheduledJobSettings scheduledJobSettings)
        {
            BackgroundScheduledJobSettings = scheduledJobSettings;
        }
        private static readonly string _baseDirectoryLocation = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string _templateFolder = Path.Combine(_baseDirectoryLocation, "EmailTemplates");

        protected string ReadEmailTemplateContent(string templateEmailName, string formatfile = "html")
        {
            var filePath = Path.Combine(_templateFolder, templateEmailName + "." + formatfile);
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs, Encoding.Default);
            var emailText = sr.ReadToEnd();
            sr.Close();

            return emailText;
        }
    }
}
