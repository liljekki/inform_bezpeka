using System;
using System.Security.Cryptography;
using System.Text;

namespace lab
{
    internal class pz
    {
        public static void Main()
        {
            Console.WriteLine("enter text: ");
            string strx = Console.ReadLine();
            var md5 = HashMd5(Encoding.Unicode.GetBytes(strx));
            var sha1 = HashSHA1(Encoding.Unicode.GetBytes(strx));
            var sha256 = HashSHA256(Encoding.Unicode.GetBytes(strx));
            var sha512 = HashSHA512(Encoding.Unicode.GetBytes(strx));
            Guid guid1 = new Guid(md5);
            var md5_pass = MD5.Create();


            Console.WriteLine($"Str:{strx}");
            Console.WriteLine($"Hash MD5:{Convert.ToBase64String(md5)}");
            Console.WriteLine($"GUID:{guid1}");
            Console.WriteLine($"Hash SHA1:{Convert.ToBase64String(sha1)}");
            Console.WriteLine($"Hash SHA256:{Convert.ToBase64String(sha256)}");
            Console.WriteLine($"Hash SHA512:{Convert.ToBase64String(sha512)}");
            Console.WriteLine($"Hash HMAC:{Convert.ToBase64String(sha512)}");
        }
        public static byte[] HashMd5(byte[] datdforHash)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(datdforHash);
            }
        }
        public static byte[] HashSHA1(byte[] tobeHashed)
        {
            using (var sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(tobeHashed);
            }
        }
        public static byte[] HashSHA256(byte[] tobeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(tobeHashed);
            }
        }
        public static byte[] HashSHA384(byte[] tobeHashed)
        {
            using (var sha384 = SHA384.Create())
            {
                return sha384.ComputeHash(tobeHashed);
            }
        }
        public static byte[] HashSHA512(byte[] tobeHashed)
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(tobeHashed);
            }
        }
        public static byte[] ComputeHmacsha1(byte[] toBeHashed, byte[] key)
        {
            using (var hmac = new HMACSHA1(key))
            {
                return hmac.ComputeHash(toBeHashed);
            }
        }

    }
}
