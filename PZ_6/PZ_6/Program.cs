using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        const string original = "This is Elon Mask";
        var key_aes = enc_dec.GenerateRandomNumber(32);
        var iv_aes = enc_dec.GenerateRandomNumber(16);
        var key = enc_dec.GenerateRandomNumber(8);
        var iv = enc_dec.GenerateRandomNumber(8);
        var key2 = enc_dec.GenerateRandomNumber(16);
        var iv2 = enc_dec.GenerateRandomNumber(8);



        var encrypted_des = enc_dec.Encrypt_des(Encoding.UTF8.GetBytes(original), key, iv);
        var decrypted_des = enc_dec.Decrypt_des(encrypted_des, key, iv);
        var encrypted_tr_des = enc_dec.Encrypt_triple(Encoding.UTF8.GetBytes(original), key2, iv2);
        var decrypted_tr_des = enc_dec.Decrypt_triple(encrypted_tr_des, key2, iv2);
        var encrypted_aes = enc_dec.Encrypt_aes(Encoding.UTF8.GetBytes(original), key_aes, iv_aes);
        var decrypted_aes = enc_dec.Decrypt_aes(encrypted_aes, key_aes, iv_aes);

        Console.WriteLine("Original Text = " + original);
        Console.WriteLine("DES: ");       
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted_des));
        Console.WriteLine("Decrypted Text = " + Encoding.UTF8.GetString(decrypted_des));
        Console.WriteLine("TRIPLE DES: ");
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted_tr_des));
        Console.WriteLine("Decrypted Text = " + Encoding.UTF8.GetString(decrypted_tr_des));
        Console.WriteLine("AES: ");
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted_aes));
        Console.WriteLine("Decrypted Text = " + Encoding.UTF8.GetString(decrypted_aes));
    }
    public class enc_dec{
        public static byte[] GenerateRandomNumber(int length){
            using (var randomNumberGenerator = new RNGCryptoServiceProvider()){
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }
        public static byte[] Encrypt_aes(byte[] dataToEncrypt, byte[] key, byte[] iv){
            using (var aes = new AesCryptoServiceProvider()){
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] Decrypt_aes(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider()){
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] Decrypt_des(byte[] dataToDecrypt, byte[] key, byte[] iv){
            using (var des = new DESCryptoServiceProvider()){
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }

            }
        }
        public static byte[] Encrypt_des(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var des = new DESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] Encrypt_triple(byte[] dataToEncrypt, byte[] key, byte[] iv){
            using (var des = new TripleDESCryptoServiceProvider()){
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream()){
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] Decrypt_triple(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }
}