using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.IO;

class Program
{
    static void Main()
    {
        string publicKeyPath = "C:\\programme\\inform_bez\\PZ_7\\Oleksiy_Kvartiuk.xml";
        string chipherTextPath = "C:\\programme\\inform_bez\\PZ_7\\2.xml";
        string friend_publicKeyPath = "C:\\programme\\inform_bez\\PZ_7\\";


        Console.WriteLine(" 1 - generate\n 2 - encrypt\n 3 - encrypt text for friends\n 4 - decrypt\n 5 - decrypt a friend's text");
        int switch_x;
        switch_x = Convert.ToInt16(Console.ReadLine());
        switch (switch_x)
        {
            case 1:
                {
                    RSA.GenerateOwnKeys(publicKeyPath);
                    Console.WriteLine("Done");
                    break;
                }
            case 2:
                {
                    Console.WriteLine("Enter text to Encrypt");
                    RSA.EncryptData(publicKeyPath, Encoding.UTF8.GetBytes(Console.ReadLine()), chipherTextPath);
                    Console.WriteLine("Done");
                    break ;
                }

            case 3:
                {
                    Console.WriteLine("Enter file name: ");
                    string path_plus_name = friend_publicKeyPath + Console.ReadLine() + ".xml";
                    Console.WriteLine(path_plus_name);
                    Console.WriteLine("Enter text to Encrypt");
                    RSA.EncryptData(publicKeyPath, Encoding.UTF8.GetBytes(Console.ReadLine()), path_plus_name);
                    Console.WriteLine("Done");
                    break;
                }
            case 4:
                {
                    var decrypted = RSA.DecryptData(chipherTextPath);
                    Console.WriteLine(" Decrypted Text: " + Encoding.Default.GetString(decrypted));
                    break;
                }
            case 5:
                {
                    Console.WriteLine("Enter file name: ");
                    string path_plus_name = friend_publicKeyPath + Console.ReadLine() + ".xml";
                    var decrypted = RSA.DecryptData(path_plus_name);
                    Console.WriteLine(" Decrypted Text: " + Encoding.Default.GetString(decrypted));                    
                    break;
                }
        }
    }
}
public class RSA
{
    private readonly static string CspContainerName = "RsaContainer";
    public static void GenerateOwnKeys(string publicKeyPath)
    {
        CspParameters cspParameters = new CspParameters(1)
        {
            KeyContainerName = CspContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore,
            ProviderName = "Microsoft Strong Cryptographic Provider",
        };
        using (var rsa = new RSACryptoServiceProvider(2048, cspParameters))
        {
            rsa.PersistKeyInCsp = true;
            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        }
    }

    public static void EncryptData(string publicKeyPath, byte[] dataToEncrypt, string chipherTextPath)
    {
        byte[] chipherBytes;
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.FromXmlString(File.ReadAllText(publicKeyPath));
            chipherBytes = rsa.Encrypt(dataToEncrypt, true);
        }
        File.WriteAllBytes(chipherTextPath, chipherBytes);
    }
    public static byte[] DecryptData(string chipherTextPath)
    {
        byte[] chipherBytes = File.ReadAllBytes(chipherTextPath);
        byte[] plainTextBytes;
        var cspParams = new CspParameters
        {
            KeyContainerName = CspContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore
        };
        using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
        {
            rsa.PersistKeyInCsp = true;
            plainTextBytes = rsa.Decrypt(chipherBytes, true);
        }
        return plainTextBytes;
    }
    public static void DeleteKeyInCsp()
    {
        CspParameters cspParameters = new CspParameters
        {
            KeyContainerName = CspContainerName,
            Flags = CspProviderFlags.UseMachineKeyStore
        };
        var rsa = new RSACryptoServiceProvider(cspParameters)
        {
            PersistKeyInCsp = false
        };
        rsa.Clear();
    }
}
