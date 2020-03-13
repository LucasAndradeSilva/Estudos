using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLog.Common.ReCaptha
{
    public interface IReCaptcha
    {
        bool ValidarCaptcha(string response);
        string SiteKey();
    }
}
