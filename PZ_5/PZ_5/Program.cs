using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("Key:");
        string key = Console.ReadLine();
        byte[] salt = Hash.GenerateSalt();
        Console.WriteLine("Salt = " + Convert.ToBase64String(salt) + "\n");
        var hashedPassword1 = Hash.HashPasswordWithSalt(Encoding.UTF8.GetBytes(key),salt);
        Console.WriteLine("Hashed Password = " + Convert.ToBase64String(hashedPassword1));
    }
}


public class Hash
{
    public static byte[] GenerateSalt()
    {
        const int saltLength = 32;
        using (var randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[saltLength];
            randomNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    private static byte[] Combine(byte[] first, byte[] second)
    {
        var ret = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, ret, 0, first.Length);
        Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
        return ret;
    }
    public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Combine(toBeHashed, salt));
        }
    }
}