using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;
public class main{
    static void Main(){
        byte[] encfile = File.ReadAllBytes(@"C:\programme\inform_bez\PZ_2\pz_2\encfile5.dat").ToArray();
        string finall = @"C:\programme\inform_bez\PZ_2\pz_2\result.txt";
        string sec = "Mit21";
        byte[] key = new byte[5];
        var xor_n = new program.XOR();
        for (int i = 0; i < encfile.Length-sec.Length; i++){
            for (int j = 0; j < 5; j++){
                key[j] = encfile[j + i];
            }
            var pass = xor_n.Xor(key, Encoding.UTF8.GetBytes(sec));
            var text = xor_n.Xor(encfile, pass);
            string dec = Encoding.UTF8.GetString(text);
            if (dec.Contains(" Mit21 ")){
                File.AppendAllText(finall, Encoding.UTF8.GetString(pass) + "\n" + dec);
                Console.WriteLine($"Done, check txt file: {finall}");
            }
        }
    }
}
public class program{
    public class XOR{
        private byte[] GetKey(byte[] key, byte[] array){
            byte[] secret = new byte[array.Length];
            for (int i = 0; i < secret.Length; i++)
            {
                secret[i] = key[i % key.Length];
            }
            return secret;
        }
        public byte[] Xor(byte[] file, byte[] pass){
            byte[] results = new byte[file.Length];
            for (int i = 0; i < file.Length; i++)
            {
                results[i] = (byte)(file[i] ^ GetKey(pass, file)[i]);
            }
            return results;
        }
    }
}
