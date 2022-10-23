using System;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace hesh
{
    class Program
    {
        public static async Task Main()
        {
            string path = @"C:\programme\inform_bez\PZ_5\loginpasssalt.txt";
            do
            {
                Console.WriteLine("Choose:");
                Console.WriteLine("1 - register");
                Console.WriteLine("2 - login");
                Console.WriteLine("Write here: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Create login: ");
                        string login = Console.ReadLine();
                        Console.WriteLine("Create password: ");
                        string password = Console.ReadLine();
                        byte[] password_b = Encoding.Unicode.GetBytes(password);
                        byte[] salt = PBKDF2.GenerateSalt();

                        var sha256 = PBKDF2.HashPassword(password_b, salt, 13000);

                        await File.AppendAllTextAsync(path, login + "\n");
                        await File.AppendAllTextAsync(path, Convert.ToBase64String(sha256) + "\n");
                        await File.AppendAllTextAsync(path, Convert.ToBase64String(salt) + "\n");
                        break;
                    case 2:
                        Console.WriteLine("Login: ");
                        string log = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        string pass_2 = Console.ReadLine();
                        byte[] pass_2b = Encoding.Unicode.GetBytes(pass_2);

                        //var second_sha256 = ComputeHashSha256(logint_b, pass_b);
                        //string fileText = await File.ReadAllTextAsync(path);
                        string[] fileText = await File.ReadAllLinesAsync(path);

                        int switch_in = 1;
                        for (int i = 0; i < fileText.Length; i += 3)
                        {
                            byte[] salt_2 = Convert.FromBase64String(fileText[i + 2]);
                            var second_sha256 = PBKDF2.HashPassword(pass_2b, salt_2, 13000);
                            if (fileText[i + 1] == Convert.ToBase64String(second_sha256) && log == fileText[i])
                            {
                                switch_in++;
                            }
                        }

                        if (switch_in > 1) Console.Write("done\n\n");
                        else Console.WriteLine("wrong pass or login\n\n");

                        break;
                }
            } while (true);
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
            public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
            {
                using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
                {
                    return rfc2898.GetBytes(20);
                }
            }
        }
    }
}