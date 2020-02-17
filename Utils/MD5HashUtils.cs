using System;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    class MD5HashUtils
    {
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public string MD5Hash(string Password)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(Password));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }

        }
    }
}