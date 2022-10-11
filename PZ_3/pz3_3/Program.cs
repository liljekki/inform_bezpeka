using System;
using System.Security.Cryptography;
using System.Text;


namespace px3_2
{
    class Program
    {
        public static void Main()
        {
            do
            {
                Console.WriteLine("Choose algorithm:");
                Console.WriteLine("1 - HMAC MD5");
                Console.WriteLine("2 - HMAC SHA1");
                Console.WriteLine("3 - HMAC SHA256");
                Console.WriteLine("4 - HMAC SHA384");
                Console.WriteLine("5 - HMAC SHA512");
                Console.WriteLine("write here: ");
                int choose = Convert.ToInt32(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        Console.WriteLine("Message:");
                        string msg = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key = Console.ReadLine();
                        var strmd5 = ComputeHashMd5(Encoding.Unicode.GetBytes(msg), Encoding.Unicode.GetBytes(key));

                        Console.WriteLine($"Message: {msg}");
                        Console.WriteLine($"HMAC MD5: {Convert.ToBase64String(strmd5)}\n");

                        Console.WriteLine("Check hash and message: ");
                        Console.WriteLine("Hash:");
                        string hash = Console.ReadLine();
                        Console.WriteLine("Message:");
                        string msg_check = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key_check = Console.ReadLine();
                        var strmd5_check = ComputeHashMd5(Encoding.Unicode.GetBytes(msg_check), Encoding.Unicode.GetBytes(key_check));
                        
                        if (Convert.ToBase64String(strmd5).Equals(hash)) Console.WriteLine("Done\n");
                        else Console.WriteLine("Wrong\n");
                        break;
                   case 2:
                        Console.WriteLine("Message:");
                        string msg1 = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key1 = Console.ReadLine();
                        var strsh1 = ComputeHashSHA1(Encoding.Unicode.GetBytes(msg1), Encoding.Unicode.GetBytes(key1));

                        Console.WriteLine($"Message: {msg1}");
                        Console.WriteLine($"HMAC MD5: {Convert.ToBase64String(strsh1)}\n");

                        Console.WriteLine("Check hash and message: ");
                        Console.WriteLine("Hash:");
                        string hash1 = Console.ReadLine();
                        Console.WriteLine("Message:");
                        string msg1_check = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key1_check = Console.ReadLine();
                        var strsh1_check = ComputeHashSHA1(Encoding.Unicode.GetBytes(msg1_check), Encoding.Unicode.GetBytes(key1_check));

                        if (Convert.ToBase64String(strsh1).Equals(hash1)) Console.WriteLine("Done\n");
                        else Console.WriteLine("Wrong\n");
                        break;
                    case 3:
                        Console.WriteLine("Message:");
                        string msg2 = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key2 = Console.ReadLine();
                        var strsh2 = ComputeHashSha256(Encoding.Unicode.GetBytes(msg2), Encoding.Unicode.GetBytes(key2));

                        Console.WriteLine($"Message: {msg2}");
                        Console.WriteLine($"HMAC MD5: {Convert.ToBase64String(strsh2)}\n");

                        Console.WriteLine("Check hash and message: ");
                        Console.WriteLine("Hash:");
                        string hash2 = Console.ReadLine();
                        Console.WriteLine("Message:");
                        string msg2_check = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key2_check = Console.ReadLine();
                        var strsh2_check = ComputeHashSha256(Encoding.Unicode.GetBytes(msg2_check), Encoding.Unicode.GetBytes(key2_check));

                        if (Convert.ToBase64String(strsh2).Equals(hash2)) Console.WriteLine("Done\n");
                        else Console.WriteLine("Wrong\n");
                        break;
                    case 4:
                        Console.WriteLine("Message:");
                        string msg3 = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key3 = Console.ReadLine();
                        var strsh3 = ComputeHashSHA384(Encoding.Unicode.GetBytes(msg3), Encoding.Unicode.GetBytes(key3));

                        Console.WriteLine($"Message: {msg3}");
                        Console.WriteLine($"HMAC MD5: {Convert.ToBase64String(strsh3)}\n");

                        Console.WriteLine("Check hash and message: ");
                        Console.WriteLine("Hash:");
                        string hash3 = Console.ReadLine();
                        Console.WriteLine("Message:");
                        string msg3_check = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key3_check = Console.ReadLine();
                        var strsh3_check = ComputeHashSHA384(Encoding.Unicode.GetBytes(msg3_check), Encoding.Unicode.GetBytes(key3_check));

                        if (Convert.ToBase64String(strsh3).Equals(hash3)) Console.WriteLine("Done\n");
                        else Console.WriteLine("Wrong\n");
                        break;
                    case 5:
                        Console.WriteLine("Message:");
                        string msg4 = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key4 = Console.ReadLine();
                        var strsh4 = ComputeHashSHA512(Encoding.Unicode.GetBytes(msg4), Encoding.Unicode.GetBytes(key4));

                        Console.WriteLine($"Message: {msg4}");
                        Console.WriteLine($"HMAC MD5: {Convert.ToBase64String(strsh4)}\n");

                        Console.WriteLine("Check hash and message: ");
                        Console.WriteLine("Hash:");
                        string hash4 = Console.ReadLine();
                        Console.WriteLine("Message:");
                        string msg4_check = Console.ReadLine();
                        Console.WriteLine("Key:");
                        string key4_check = Console.ReadLine();
                        var strsh4_check = ComputeHashSHA512(Encoding.Unicode.GetBytes(msg4_check), Encoding.Unicode.GetBytes(key4_check));

                        if (Convert.ToBase64String(strsh4).Equals(hash4)) Console.WriteLine("Done\n");
                        else Console.WriteLine("Wrong\n");
                        break;
                }
            } while (true);
        }

        static byte[] ComputeHashMd5(byte[] input, byte[] key)
        {
            using (var md5 = new HMACMD5(key))
            {
                return md5.ComputeHash(input);
            }
        }
        static byte[] ComputeHashSHA1(byte[] input, byte[] key)
        {
            using (var sha1 = new HMACSHA1(key))
            {
                return sha1.ComputeHash(input);
            }
        }

        static byte[] ComputeHashSha256(byte[] input, byte[] key)
        {
            using (var sha256 = new HMACSHA256())
            {
                return sha256.ComputeHash(input);
            }
        }
        static byte[] ComputeHashSHA384(byte[] input, byte[] key)
        {
            using (var sha384 = new HMACSHA384(key))
            {
                return sha384.ComputeHash(input);
            }
        }
        static byte[] ComputeHashSHA512(byte[] input, byte[] key)
        {
            using (var sha512 = new HMACSHA512(key))
            {
                return sha512.ComputeHash(input);
            }
        }
    }
}