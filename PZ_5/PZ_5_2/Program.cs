using System;
using System.Diagnostics;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
class Program
{
    static void Main()
    {
        Console.WriteLine("Key:");
        string key = Console.ReadLine();
        Console.WriteLine("Way:");
        Console.WriteLine("1 - md5:\n2 - sh1:\n3 - sh256:\n4 - sh384:\n5 - sh512:\n");
        int way = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < 10; i++)
        {
            HashPassword_pr(key, 13 * 10000 + i * 50000, way);
        }
    }
    private static void HashPassword_pr(string passwordToHash, int numberOfRounds, int way)
    {
        HashAlgorithmName method = HashAlgorithmName.MD5;
        switch (way)
        {
            case 1:
                 method = HashAlgorithmName.MD5;
                break;
            case 2:
                method = HashAlgorithmName.SHA1;
                break;
            case 3:
                method = HashAlgorithmName.SHA256;
                break;
            case 4:
                method = HashAlgorithmName.SHA384;
                break;
            case 5:
                method = HashAlgorithmName.SHA512;
                break;
        }
        
        var sw = new Stopwatch();
        sw.Start();
        var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(),numberOfRounds, method);
        sw.Stop();
        Console.WriteLine();
        Console.WriteLine("Password to hash : " + passwordToHash);
        Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
        Console.WriteLine("Iterations <" + numberOfRounds + ">Elapsed Time: " + sw.ElapsedMilliseconds + "ms");
    }
}
public class PBKDF2
{
    public static byte[] GenerateSalt()
    {
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[32];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName method)
    {

        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, method))
        {
            return rfc2898.GetBytes(20);
        }
    }
}