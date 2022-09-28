using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;

class Programma
{
    public static void Main(string[] args)
    {
        string path = @"C:\programme\inform_bez\PZ_2\pz_2\encfile5.dat";
        byte[] txtmsg = File.ReadAllBytes(@"C:\programme\inform_bez\PZ_2\pz_2\mit.txt").ToArray();


        var enc = new XOR();
        Console.WriteLine("password: ");
        string psswrd = Console.ReadLine();
        var finally_enc = enc.prog(txtmsg, Encoding.UTF8.GetBytes(psswrd));
        Console.WriteLine("1) the file is encrypted\n");
        File.WriteAllBytes(path, finally_enc);


        byte[] encryptContent = File.ReadAllBytes(path).ToArray();
        var dec = new XOR();
        var finally_dec = dec.prog(encryptContent, Encoding.UTF8.GetBytes(psswrd));
        Console.WriteLine("2) decrypted text:");
        Console.WriteLine(Encoding.UTF8.GetString(finally_dec));    
    }


    public class XOR
    {
        public byte[] prog(byte[] text, byte[] pas)
        {
            byte[] secret = new byte[text.Length];
            for (int i = 0; i < secret.Length; i++)
            {
                secret[i] = pas[i % pas.Length];
            }

            byte[] secretKey = secret;
            int array_size = text.Length;
            for (int i = 0; i < text.Length; i++)
            {
                text[i] = (byte)(text[i] ^ secretKey[i]);
            }
            return text;
        }
    }   
}