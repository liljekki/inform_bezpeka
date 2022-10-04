using System;
using System.Security.Cryptography;
using System.Text;

namespace hesh
{
    class Program
    {


        public static void Main()
        {
            String Hash = "po1MVkAE7IjUUwu61XxgNg==";
            String HmacHash = "564c8da6-0440-88ec-d453-0bbad57c6036";


            for (int i = 9999999; i < 100000000; i++)
            {
                string str = i.ToString();
                byte[] key = Encoding.Unicode.GetBytes(str);
                /*Console.WriteLine(Convert.ToBase64String(key));*/
                byte[] gg = Convert.FromBase64String(Hash);
                var md5 = ComputeHashMd5(ComputeHmacsha1(gg, key));
                Guid guid1 = new Guid(md5);
                Console.WriteLine(Convert.ToBase64String(gg));
                Console.WriteLine(i);
                if (String.Compare(guid1.ToString(), HmacHash) == 0)
                {
                    Console.WriteLine("DONEEEEEEEEEEEEEEEEEEEEEEEEe");
                    break;

                }
            }


            byte[] ComputeHmacsha1(byte[] toBeHashed, byte[] key)
            {
                using (var hmac = new HMACSHA1(key))
                {
                    return hmac.ComputeHash(toBeHashed);
                }
            }
            static byte[] ComputeHashMd5(byte[] input)
            {
                var md5 = MD5.Create();
                return md5.ComputeHash(input);
            }

        }

    }
}