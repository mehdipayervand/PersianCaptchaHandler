using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSample.Models;
using PersianCaptchaHandler;

namespace MvcSample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
           
            var ipAddress = Request.UserHostAddress;
            var newNumber = RandomGenerator.Next(199, 999);

            var farsiAlphabatic = NumberToString.ConvertIntNumberToFarsiAlphabatic(newNumber.ToString());
            var encrypted =  HttpUtility
                .UrlEncode(
                    Encryptor.Encrypt(
                        farsiAlphabatic, ipAddress
                    )
                );
            ;

            var userLogin = new UserLogin
            {
                Captcha = "/captcha/?text=" + encrypted,
                Encrypted = encrypted
            };


            return View(userLogin);
        }

        [HttpPost]
        public ActionResult Index(UserLogin userLogin)
        {
            var ipAddress = Request.UserHostAddress;

            var decryptedString =
                HttpUtility
                .UrlEncode(
                    Encryptor.Encrypt(
                        NumberToString.ConvertIntNumberToFarsiAlphabatic(userLogin.InputCaptcha), ipAddress
                    )
                );

            var strDecodedVAlue = userLogin.Encrypted;

            if (strDecodedVAlue != decryptedString)
            {
                var newNumber = RandomGenerator.Next(199, 999);

                var farsiAlphabatic = NumberToString.ConvertIntNumberToFarsiAlphabatic(newNumber.ToString());
                var encrypted = HttpUtility
                    .UrlEncode(
                        Encryptor.Encrypt(
                            farsiAlphabatic, ipAddress
                        )
                    );
                ;

                userLogin.Captcha = "/captcha/?text=" + encrypted;
                userLogin.Encrypted = encrypted;
                
            }

            return RedirectToAction("Index", userLogin);
        }

    }
}
