using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab11.Data_For_NIST_STS
{
    internal class EncryptionPart
    {
        public static void ProccessAes()
        {
            string plainText;
            Console.WriteLine("Getting plaintext");
            using (var stream = new FileStream(PathConst.TEXT_PATH, FileMode.OpenOrCreate))
            {
                stream.SetLength(12_500_000);
                using (StreamReader reader = new StreamReader(stream))
                {
                    plainText = reader.ReadToEnd();
                }
            }
            byte[] encrypted;
            Console.WriteLine("Starting encryption");
            using (var aes = Aes.Create())
            {
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            var str = string.Join("", encrypted.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            Console.WriteLine(str.Length);
            Console.WriteLine("Writing to file");
            Console.WriteLine("Writing to file");
            FileManager.WriteTextTo("res/AES.txt", str);
        }

        public static void ProccessTDes()
        {
            string plainText;
            Console.WriteLine("Getting plaintext");
            using (var stream = new FileStream(PathConst.TEXT_PATH, FileMode.OpenOrCreate))
            {
                stream.SetLength(12_500_000);
                using (StreamReader reader = new StreamReader(stream))
                {
                    plainText = reader.ReadToEnd();
                }
            }
            byte[] encrypted;
            Console.WriteLine("Starting encryption");
            using (var des = TripleDES.Create())
            {
                using (ICryptoTransform encryptor = des.CreateEncryptor())
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            var str = string.Join("", encrypted.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            Console.WriteLine(str.Length);
            Console.WriteLine("Writing to file");
            FileManager.WriteTextTo("res/TDES.txt", str);
        }
        public static void ProccessDes()
        {
            string plainText;
            Console.WriteLine("Getting plaintext");
            using (var stream = new FileStream(PathConst.TEXT_PATH, FileMode.OpenOrCreate))
            {
                stream.SetLength(12_500_000);
                using (StreamReader reader = new StreamReader(stream))
                {
                    plainText = reader.ReadToEnd();
                }
            }
            byte[] encrypted;
            Console.WriteLine("Starting encryption");
            using (var des = DES.Create())
            {
                using (ICryptoTransform encryptor = des.CreateEncryptor())
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            var str = string.Join("", encrypted.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            Console.WriteLine(str.Length);
            Console.WriteLine("Writing to file");
            FileManager.WriteTextTo("res/DES.txt", str);
        }
    }
}
