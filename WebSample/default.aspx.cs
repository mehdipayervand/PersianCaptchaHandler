using System;
using System.Web;
using System.Web.UI;
using PersianCaptchaHandler;

namespace WebSample
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                SetCaptcha();
        }

        private void SetCaptcha()
        {
            lblMessage.Text =
            txtCaptcha.Text = string.Empty;

            var ipAddress = Request.UserHostAddress;

            var newNumber =
                RandomGenerator.Next(100, 999)
                ;

            var farsiAlphabatic = NumberToString.ConvertIntNumberToFarsiAlphabatic(newNumber.ToString());

            hfCaptchaText.Value =
                HttpUtility
                .UrlEncode(
                    Encryptor.Encrypt(
                        farsiAlphabatic, ipAddress
                    )
                );

            txtCaptcha.Text = string.Empty;

            imgCaptchaText.ImageUrl =
                "/captcha/?text=" + hfCaptchaText.Value;

        }
        private string GetCaptcha()
        {
            var ipAddress = Request.UserHostAddress;

            var farsiAlphabatic = NumberToString.ConvertIntNumberToFarsiAlphabatic(txtCaptcha.Text);

            var encryptedString =
                HttpUtility
                .UrlEncode(
                    Encryptor.Encrypt(
                        farsiAlphabatic, ipAddress
                    )
                );

            return encryptedString;
        }

        private bool ValidateUserInputForLogin()
        {
            if (string.IsNullOrEmpty(txtCaptcha.Text) || !Utils.IsNumber(txtCaptcha.Text))
            {
                lblMessage.Text = "تصویر امنیتی را بطور صحیح وارد نکرده اید";
                return false;
            }

            var strGetCaptcha =
                GetCaptcha()
                ;

            var strDecodedVAlue =
                hfCaptchaText.Value
                ;

            if (strDecodedVAlue != strGetCaptcha)
            {
                lblMessage.Text = "کلمه امنیتی اشتباه است";
                SetCaptcha();
                return false;
            }
            return true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateUserInputForLogin()) return;
            lblMessage.Text = "کلمه امنیتی درست است";
        }

        protected void btnRefreshCaptcha_Click(object sender, ImageClickEventArgs e)
        {
            SetCaptcha();
        }
    }
}