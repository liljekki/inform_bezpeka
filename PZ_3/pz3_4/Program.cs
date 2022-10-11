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
            string path = @"C:\programme\inform_bez\PZ_3\pz3\loginandpass.txt";
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
                        byte[] login_b = Encoding.Unicode.GetBytes(login);
                        Console.WriteLine("Create password: ");
                        string password = Console.ReadLine();
                        byte[] password_b = Encoding.Unicode.GetBytes(password);
                        var sha256 = ComputeHashSha256(login_b, password_b);
                        string Hash_r = Convert.ToBase64String(sha256);
                   
                        await File.AppendAllTextAsync(path, login + "\n");
                        await File.AppendAllTextAsync(path, Hash_r + "\n");
                        break;
                    case 2:
                        Console.WriteLine("Login: ");
                        string log = Console.ReadLine();
                        byte[] logint_b = Encoding.Unicode.GetBytes(log);
                        Console.WriteLine("Password: ");
                        string pass = Console.ReadLine();
                        byte[] pass_b = Encoding.Unicode.GetBytes(pass);
                        var second_sha256 = ComputeHashSha256(logint_b, pass_b);
                        /*string fileText = await File.ReadAllTextAsync(path);*/
                        string[] fileText = await File.ReadAllLinesAsync(path);

                        int switch_in = 1;
                        for (int i = 0; i < fileText.Length; i+=2)
                        {
                            if (fileText[i+1] == Convert.ToBase64String(second_sha256) && log == fileText[i]) 
                            {
                                switch_in++;
                            }
                        }

                        if (switch_in > 1) Console.Write("done\n\n");
                        else Console.WriteLine("wrong pass or login\n\n");

                        break;
                }
            } while (true);

            static byte[] ComputeHashSha256(byte[] login, byte[] key)
            {
                using (var sha256 = new HMACSHA256(key))
                {
                    return sha256.ComputeHash(login);
                }
            }
        }
    }
}