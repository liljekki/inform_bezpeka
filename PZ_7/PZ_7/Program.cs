using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        const string text = "Privet everybody";
        RSA.AssignNewKey();
        var encrypted = RSA.EncryptData(Encoding.UTF8.GetBytes(text));
        var decrypted = RSA.DecryptData(encrypted);
        
        Console.WriteLine(" Original Text: " + text);
        Console.WriteLine(" Encrypted Text: " + Convert.ToBase64String(encrypted));
        Console.WriteLine(" Decrypted Text: " + Encoding.Default.GetString(decrypted));

    }
}
public class RSA
{
    private static RSAParameters _publicKey, _privateKey;
    public static void AssignNewKey()
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            _publicKey = rsa.ExportParameters(false);
            _privateKey = rsa.ExportParameters(true);
        }
    }

    public static byte[] EncryptData(byte[] dataToEncrypt)
    {
        byte[] cipherbytes;
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(_publicKey);
            cipherbytes = rsa.Encrypt(dataToEncrypt, true);
        }
        return cipherbytes;
    }

    public static byte[] DecryptData(byte[] dataToEncrypt)
    {
        byte[] plain;
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(_privateKey);
            plain = rsa.Decrypt(dataToEncrypt, true);
        }
        return plain;
    }
}
