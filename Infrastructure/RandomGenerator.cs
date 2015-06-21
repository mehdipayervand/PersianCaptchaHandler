using System;
using System.Security.Cryptography;

namespace PersianCaptchaHandler
{
    public class RandomGenerator
    {
        private static readonly byte[] Randb = new byte[4];
        private static readonly RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

        public static int Next()
        {
            Rand.GetBytes(Randb);
            var value = BitConverter.ToInt32(Randb, 0);
            if (value < 0) value = -value;
            return value;
        }
        public static int Next(int max)
        {
            Rand.GetBytes(Randb);
            var value = BitConverter.ToInt32(Randb, 0);
            value = value%(max + 1);
            if (value < 0) value = -value;
            return value;
        }
        public static int Next(int min, int max)
        {
            var value = Next(max - min) + min;
            return value;
        }
    }
}
