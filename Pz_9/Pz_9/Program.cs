using System;
using System.Security.Cryptography;
using System.Text;

class program
{
    static void Main(string[] args)
    {
        string publicKeyPath = "C:\\programme\\inform_bez\\Pz_9\\";
        Console.WriteLine("Enter the name of xml file");
        string name_path = Console.ReadLine();
        string path_plus_name = publicKeyPath + name_path + ".xml";
        Console.WriteLine("Enter the text");
        var document = Encoding.UTF8.GetBytes(Console.ReadLine());
        byte[] hashedDocument;
        using (var sha512 = SHA512.Create())
        {
            hashedDocument = sha512.ComputeHash(document);
        }


        var digitalSignature = new DigitalSignature();
        digitalSignature.AssignNewKey(path_plus_name);
        var signature = digitalSignature.SignData(hashedDocument);
        var verified = digitalSignature.VerifySignature(path_plus_name, hashedDocument, signature);
        Console.WriteLine(" Original Text = " + Encoding.Default.GetString(document));
        Console.WriteLine(" Digital Signature = " + Convert.ToBase64String(signature));
        Console.WriteLine(verified ? "The digital signature has been correctly verified." : "The digital signature has NOT been correctly verified.");
    }
}


public class DigitalSignature
{
    private readonly static string CspContainerName = "RsaContainer";
    public void AssignNewKey(string publicKeyPath)
    {
        var cspParams = new CspParameters   
        {
            KeyContainerName = CspContainerName, Flags = CspProviderFlags.UseMachineKeyStore
        };
        using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
        {
            rsa.PersistKeyInCsp = true;
            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        }
    }

    public byte[] SignData(byte[] hashOfDataToSign)
    {
        var cspParams = new CspParameters
        {
            KeyContainerName = CspContainerName, Flags = CspProviderFlags.UseMachineKeyStore,
        };

        using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
        {
            rsa.PersistKeyInCsp = false;
            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            rsaFormatter.SetHashAlgorithm(nameof(SHA512));
            return rsaFormatter.CreateSignature(hashOfDataToSign);
        }
    }

    public bool VerifySignature(string publicKeyPath, byte[] hashedDocument, byte[] signature)
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            rsa.PersistKeyInCsp = false;
            rsa.FromXmlString(File.ReadAllText(publicKeyPath));
            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm(nameof(SHA512));
            return rsaDeformatter.VerifySignature(hashedDocument, signature);
        }
    }
}
