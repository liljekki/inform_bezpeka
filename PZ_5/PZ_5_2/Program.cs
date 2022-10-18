using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
class Program
{
    static void Main()
    {
        Console.WriteLine("Key:");
        string key = Console.ReadLine();
        Console.WriteLine("Way:");
        int way = Convert.ToInt16(Console.ReadLine());
        for (int i = 0; i < 10; i++)
        {
            HashPassword(key, 13 * 10000 + i * 50000, way);
        }
    }
    private static void HashPassword(string passwordToHash, int numberOfRounds, int way)
    {
        var sw = new Stopwatch();
        sw.Start();
        var hashedPassword = PBKDF2.HashPasswordWithSalt(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), way, numberOfRounds);
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
    private static byte[] Combine(byte[] first, byte[] second)
    {
        var ret = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, ret, 0, first.Length);
        Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
        return ret;
    }
    /*    public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
            {
                return rfc2898.GetBytes(20);
            }
        }*/
    public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt, int method, int numberOfRounds)
    {
        switch (method)
        {
            case 1:
                using (var hash_way = MD5.Create())
                {

                    /*ComputeHashMd5(toBeHashed, numberOfRounds);*/

                    var finnal = ComputeHashMd5(Combine(toBeHashed, salt), numberOfRounds);
                    return finnal;
                }
            case 2:
                using (var hash_way = SHA1.Create())
                {
                    return hash_way.ComputeHash(Combine(toBeHashed, salt));
                }
            case 3:
                using (var hash_way = SHA256.Create())
                {
                    return hash_way.ComputeHash(Combine(toBeHashed, salt));
                }
            case 4:
                using (var hash_way = SHA384.Create())
                {
                    return hash_way.ComputeHash(Combine(toBeHashed, salt));
                }
            case 5:
                using (var hash_way = SHA512.Create())
                {
                    return hash_way.ComputeHash(Combine(toBeHashed, salt));
                }
        }
        return null;
    }
    static byte[] ComputeHashMd5(byte[] input, int rounds)
    {
        var md5 = MD5.Create();
        if (rounds == 1) return md5.ComputeHash(input);
        return md5.ComputeHash(md5.ComputeHash(input));

    }
}