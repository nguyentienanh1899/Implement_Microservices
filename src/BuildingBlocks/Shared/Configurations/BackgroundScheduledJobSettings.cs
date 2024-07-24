using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Configurations
{
    public class BackgroundScheduledJobSettings
    {
        public string HangfireUrl { get; set; }
        public string PaymentOrderUrl { get; set; }
        public string BasketUrl { get; set; }
        public string ScheduledJobUrl { get; set; }
    }
}
