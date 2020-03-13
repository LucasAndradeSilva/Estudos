using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebLog.Models.Interfaces.Repository;

namespace WebLog.Common.ReCaptha
{
    public class ReCaptcha : IReCaptcha
    {
        private string _siteKey;
        private string _privateKey;

        public ReCaptcha(string siteKey, string privateKey)
        {
            _siteKey = siteKey;
            _privateKey = privateKey;
        }      
   
        public bool ValidarCaptcha(string response)
        {
            var client = new WebClient();
            try
            {
                var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", _privateKey, response));
                return JObject.Parse(googleReply)["success"].ToObject<bool>();
            }
            catch
            {
                return false;
            }
        }

        public string SiteKey() => _siteKey;
    }
}
