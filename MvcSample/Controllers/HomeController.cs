using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSample.Models;
using NumberToWordsLib;
using PersianCaptchaHandler;

namespace MvcSample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
           
            var newNumber = RandomGenerator.Next(199, 999);

            var farsiAlphabatic = newNumber.NumberToText(Language.Persian);
            var encrypted =  HttpUtility
                .UrlEncode(
                    Encryptor.Encrypt(
                        farsiAlphabatic
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
            var decryptedString =
                HttpUtility
                .UrlEncode(
                    Encryptor.Encrypt(
                        (int.Parse(userLogin.InputCaptcha).NumberToText(Language.Persian)))
                );

            var strDecodedVAlue = userLogin.Encrypted;

            if (strDecodedVAlue != decryptedString)
            {
                var newNumber = RandomGenerator.Next(199, 999);

                var farsiAlphabatic = newNumber.NumberToText(Language.Persian);
                var encrypted = HttpUtility
                    .UrlEncode(
                        Encryptor.Encrypt(
                            farsiAlphabatic
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
