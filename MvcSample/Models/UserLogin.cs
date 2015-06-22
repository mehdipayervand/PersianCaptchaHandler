using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSample.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Captcha { get; set; }
        public string InputCaptcha { get; set; }
        public string Encrypted { get; set; }
    }
}