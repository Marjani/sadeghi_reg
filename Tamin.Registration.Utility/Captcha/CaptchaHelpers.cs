using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tamin.Registration.Utility.Captcha
{
    public static class CaptchaHelpers
    {
        public static int CreateSalt()
        {
            Random random = new Random();
            return random.Next(11, 99);
        }
    }
}