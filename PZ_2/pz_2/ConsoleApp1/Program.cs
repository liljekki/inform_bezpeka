using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;

class File_Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("List of options you can do:");
        Console.WriteLine("1 - read the content");
        Console.WriteLine("2 - to encrypt the content and to write the content into new doc");
        Console.WriteLine("3 - to decrypt an ecrypted file");
        Console.WriteLine("Choose what you want to do with your file:");
        int option = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        string path = @"C:\info_security\practice2\file_encrypted.dat";
        byte[] readContent = File.ReadAllBytes(@"C:\info_security\practice2\file_sample.txt").ToArray();

        switch (option)
        {
            case 1:
                Console.WriteLine(Encoding.UTF8.GetString(readContent));
                break;
            case 2:
                try
                {
                    var e_x = new XOR_Program();
                    Console.WriteLine("Enter the password for encryption:");
                    string e_password = Console.ReadLine();
                    Console.WriteLine("\n");
                    byte[] password_e = Encoding.UTF8.GetBytes(e_password);
                    var encryptedProcess = e_x.Encryption(readContent, password_e);
                    Console.WriteLine("PROCESSING...\n");
                    Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("ENCRYPTION WAS DONE");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    File.WriteAllBytes(path, encryptedProcess);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 3:
                try
                {
                    byte[] encryptContent = File.ReadAllBytes(path).ToArray();
                    var d_x = new XOR_Program();
                    Console.WriteLine("Enter the password for encryption:");
                    string d_password = Console.ReadLine();
                    Console.WriteLine("\n");
                    byte[] password_d = Encoding.UTF8.GetBytes(d_password);
                    var decryptedProcess = d_x.Decryption(encryptContent, password_d);
                    Console.WriteLine("PROCESSING...\n");
                    Console.WriteLine(Encoding.UTF8.GetString(decryptedProcess));
                    Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("DECRYPTION WAS DONE");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                Console.WriteLine("Choose an option from the list above");
                break;
        }
    }

    public class XOR_Program
    {
        private byte[] GetSecretKey(byte[] key, byte[] array)
        {
            byte[] secret = new byte[array.Length];
            for (int i = 0; i < secret.Length; i++)
            {
                secret[i] = key[i % key.Length];
            }
            return secret;
        }

        private byte[] XoR(byte[] text, byte[] pas)
        {
            byte[] secretKey = GetSecretKey(pas, text);
            int array_size = text.Length;
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = (byte)(text[i] ^ secretKey[i]);
            }
            return text;
        }

        public byte[] Encryption(byte[] ourText, byte[] ourPas)
        {
            return XoR(ourText, ourPas);
        }

        public byte[] Decryption(byte[] encryptedText, byte[] ourPas)
        {
            return XoR(encryptedText, ourPas);
        }
    }
}