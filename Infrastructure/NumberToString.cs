namespace PersianCaptchaHandler
{
    public class NumberToString
    {

        #region Fields
        private static readonly string[] Yakan = new[] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static readonly string[] Dahgan = new[] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static readonly string[] Dahyek = new [] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static readonly string[] Sadgan = new [] { "", "یکصد", "دوصد", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static readonly string[] Basex = new [] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };
        #endregion

        private static string Getnum3(int num3)
        {
            var s = "";
            var d12 = num3 % 100;
            var d3 = num3 / 100;
            if (d3 != 0)
                s = Sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + Dahyek[d12 - 10];
            }
            else
            {
                var d2 = d12 / 10;
                if (d2 != 0)
                    s = s + Dahgan[d2] + " و ";
                var d1 = d12 % 10;
                if (d1 != 0)
                    s = s + Yakan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            }
            return s;
        }

        public static string ConvertIntNumberToFarsiAlphabatic(string snum)
        {
            var stotal = "";
            if (snum == "0") return Yakan[0];

            snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
            var l = snum.Length / 3 - 1;
            for (var i = 0; i <= l; i++)
            {
                var b = int.Parse(snum.Substring(i * 3, 3));
                if (b != 0)
                    stotal = stotal + Getnum3(b) + " " + Basex[l - i] + " و ";
            }
            stotal = stotal.Substring(0, stotal.Length - 3);
            return stotal;
        }
    }
}
