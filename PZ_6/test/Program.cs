using System;
using System.Security.Cryptography;
using System.Text;


class Program
{
    static void Main(string[] args)
    {
        var des = new DesEncryption();
        var key = des.GenerateRandomNumber(8);
        var iv = des.GenerateRandomNumber(8);
        const string original = "Text to encrypt";
        var encrypted = des.Encrypt(Encoding.UTF8.GetBytes(original), key, iv);
        var decrypted = des.Decrypt(encrypted, key, iv);
        var decryptedMessage = Encoding.UTF8.GetString(decrypted);
        Console.WriteLine("DES Demonstration in .NET");
        Console.WriteLine("-------------------------");
        Console.WriteLine();
        Console.WriteLine("Original Text = " + original);
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted));
        Console.WriteLine("Decrypted Text = " + decryptedMessage);


        var tripleDes = new TripleDesEncryption();
        var key1 = tripleDes.GenerateRandomNumber(16);
        var iv1 = tripleDes.GenerateRandomNumber(8);
        const string original1 = "Text to encrypt";
        var encrypted1 = tripleDes.Encrypt(Encoding.UTF8.GetBytes(original1), key1, iv1);
        var decrypted1 = tripleDes.Decrypt(encrypted1, key1, iv1);
        var decryptedMessage1 = Encoding.UTF8.GetString(decrypted1);
        Console.WriteLine("Triple DES Demonstration in .NET");
        Console.WriteLine("--------------------------------");
        Console.WriteLine();
        Console.WriteLine("Original Text = " + original);
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted1));
        Console.WriteLine("Decrypted Text = " + decryptedMessage1);



        var aes = new AesEncryption();
        var key2 = aes.GenerateRandomNumber(32);
        var iv2 = aes.GenerateRandomNumber(16);
        const string original2 = "Text to encrypt";
        var encrypted2 = aes.Encrypt(
        Encoding.UTF8.GetBytes(original2), key2, iv2);
        var decrypted2 = aes.Decrypt(encrypted2, key2, iv2);
        var decryptedMessage2 = Encoding.UTF8.GetString(decrypted2);
        Console.WriteLine("AES Encryption in .NET");
        Console.WriteLine("----------------------");
        Console.WriteLine();
        Console.WriteLine("Original Text = " + original2);
        Console.WriteLine("Encrypted Text = " + Convert.ToBase64String(encrypted2));
        Console.WriteLine("Decrypted Text = " + decryptedMessage2);
        Console.ReadLine();
    }
}


class DesEncryption
{
    public byte[] GenerateRandomNumber(int length)
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }

    public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
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

    public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
    {
        using (var des = new DESCryptoServiceProvider())
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }
}

class TripleDesEncryption
{
    public byte[] GenerateRandomNumber(int length)
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
    {
        using (var des = new TripleDESCryptoServiceProvider())
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

    public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
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
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

}


class AesEncryption
{
    public byte[] GenerateRandomNumber(int length)
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[length];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
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

    public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
    {
        using (var des = new AesCryptoServiceProvider())
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }


}