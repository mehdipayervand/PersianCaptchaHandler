using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Web;

namespace PersianCaptchaHandler
{
    public class CaptchaHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Params["text"] == null) return;

            var ipAddress = context.Request.UserHostAddress;

            var fontSize = 8;
            const int heightTotalImage = 50;
            const int widthTotalImage = 150;

            var queryStringValue = context.Request.Params["text"];

            var sImageText = Encryptor.Decrypt(queryStringValue, ipAddress);

            var objBmpImage = new Bitmap(1, 1, PixelFormat.Format32bppArgb);

            // Create the Font object for the image text drawing.
            var objFont = new Font("Tahoma", fontSize, FontStyle.Bold, GraphicsUnit.Point);

            // Create a graphics object to measure the text's width and height.
            var objGraphics = Graphics.FromImage(objBmpImage);


            // Thies is where the bitmap size is determined.
            var intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width + 10;
            var intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height + 10;

            var floatX = (widthTotalImage - intWidth) / 2;
            var floatY = (heightTotalImage - intHeight) / 2;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(widthTotalImage, heightTotalImage));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(Color.FromArgb(252, 252, 250));
            objGraphics.SmoothingMode = SmoothingMode.HighQuality;
            objGraphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            objGraphics.DrawString(sImageText, objFont, new SolidBrush(Color.FromArgb(95, 67, 189)), floatX, floatY);

            // Adds a simple wave
            double distort = RandomGenerator.Next(2, 5) * (RandomGenerator.Next(5) == 1 ? 1 : -1);
            using (var copy = (Bitmap)objBmpImage.Clone())
            {
                for (var y = 0; y < heightTotalImage; y++)
                {
                    for (var x = 0; x < widthTotalImage; x++)
                    {
                        var newX = (int)(x + (distort * Math.Sin(Math.PI * y / 84.0)));
                        var newY = (int)(y + (distort * Math.Cos(Math.PI * x / 44.0)));

                        if (newX < 0 || newX >= widthTotalImage) newX = 0;
                        if (newY < 0 || newY >= heightTotalImage) newY = 0;

                        var dd = copy.GetPixel(newX, newY);
                        objBmpImage.SetPixel(x, y, dd);
                    }
                }
            }

            context.Response.ContentType = "image/jpeg";
            objBmpImage.Save(context.Response.OutputStream, ImageFormat.Gif);
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion
    }
}
