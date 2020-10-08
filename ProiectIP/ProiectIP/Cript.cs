using System;
using System.Security.Cryptography;
using System.Text;

namespace ProiectIP
{
    /// <summary>
    /// Cripts the user's password.
    /// </summary>
    public class Cript
    {
        /// <summary>
        /// Cripts the user's password.
        /// </summary>
        /// <param name="pass">String to be cripted</param>
        /// <returns></returns>
        public static string SHA256hash(string pass)
        {
            SHA256 hash = SHA256Managed.Create();
            Byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder sb = new StringBuilder();
            foreach (Byte b in result)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
