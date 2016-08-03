using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TomTom.Utilities
{
    public static class EncriptionUtilities
    {

        public static byte[] ToMD5Hash(this string str)
        {
            if (str.IsEmpty())
                return null;
            char[] charArray = str.ToCharArray();
            int len = charArray.GetLength(0);
            byte[] buffer = new byte[len];

            for (int i = 0; i < len; i++)
                buffer[i] = (byte)charArray[i];

            var md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(buffer);
        }
    }
}
