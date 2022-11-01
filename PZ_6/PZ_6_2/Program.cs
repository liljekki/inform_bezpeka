using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Pass: ");
        string pass = Console.ReadLine();
        byte[] salt = prog.GenerateSalt();
        byte[] key = prog.rfc(pass, salt, 130000,32);
        byte[] iv = prog.rfc(pass, salt, 130000, 16);

        Console.WriteLine("Message: ");
        string msg = Console.ReadLine();
        byte[] enc_msg = prog.Encrypt_aes(Encoding.UTF8.GetBytes(msg), key, iv);
        Console.WriteLine("AES Encrypt: " + Convert.ToBase64String(enc_msg));

        byte[] dec_msg = prog.Decrypt_aes(enc_msg, key, iv);
        Console.WriteLine("AES Decrypt: " + Encoding.UTF8.GetString(dec_msg));
    }


    public class prog{
            public static byte[] GenerateSalt(){
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[32];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] rfc(string toBeHashed, byte[] salt, int numberOfRounds, int length)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
            {
                return rfc2898.GetBytes(length);
            }
        }
        public static byte[] Encrypt_aes(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] Decrypt_aes(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}