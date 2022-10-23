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
                        byte[] salt = Hash.GenerateSalt();

                        var sha256 = Hash.HashPasswordWithSalt(password_b, salt);
                        
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
                            byte[] salt_2 = Convert.FromBase64String(fileText[i+2]);
                            var second_sha256 = Hash.HashPasswordWithSalt(pass_2b, salt_2);
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
            public class Hash
        { 
            public static byte[] GenerateSalt()
            {
                const int saltLength = 32;
                using (var randomNumberGenerator =
                new RNGCryptoServiceProvider())
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
                Buffer.BlockCopy(second, 0, ret, first.Length,
                second.Length);
                return ret;
            }
            public static byte[] HashPasswordWithSalt(
            byte[] toBeHashed, byte[] salt)
            {
                using (var sha256 = SHA256.Create())
                {
                    return sha256.ComputeHash(Combine(toBeHashed,
                    salt));
                }
            }
        }
    }
    }
