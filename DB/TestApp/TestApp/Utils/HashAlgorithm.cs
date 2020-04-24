using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TestApp.Utils
{
    public class HashAlgorithm
    {
        private MD5 md5;

        public HashAlgorithm()
        {
            md5 = MD5.Create();
        }

        public string GetHash(string msg)
            => Convert.ToBase64String(md5.ComputeHash(Encoding.Unicode.GetBytes(msg)));

        public bool CheckHash(string stringForHash, string hash)
        {
            if (Convert.ToBase64String(md5.ComputeHash(Encoding.Unicode.GetBytes(stringForHash))) == hash)
                return true;
            return false;
        }
    }
}