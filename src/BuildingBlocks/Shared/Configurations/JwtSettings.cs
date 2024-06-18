using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Configurations
{
    public class JwtSettings
    {
        //Many fields can be configured here such as issuer, expiration date,...
        public string Key {  get; set; }
    }
}
