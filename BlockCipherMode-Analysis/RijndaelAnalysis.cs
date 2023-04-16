using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.Unicode;

namespace BlockCipherMode_Analysis
{
    internal class RijndaelAnalysis
    {
        static string algorithmEnc = "Rijndael-Enc-";
        static string algorithmDec = "Rijndael-Dec-";
        private byte[] key;
        private byte[] iv;
        HexFile hex;

        public RijndaelAnalysis()
        {
            hex = new HexFile();
            using (Rijndael myRijndael = Rijndael.Create()) {
                this.key = myRijndael.Key;
                this.iv = myRijndael.IV;
            }
        }

        public void PerformEncryptionAnalysis(string plainText)
        {
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;
                rijAlg.BlockSize = 128;
                rijAlg.Padding = PaddingMode.None;


                var ecb = Encrypt(plainText, rijAlg, CipherMode.ECB);
                var cbc = Encrypt(plainText, rijAlg, CipherMode.CBC);
                var cfb = Encrypt(plainText, rijAlg, CipherMode.CFB);
                hex.Write(ecb, ModeFile.ECB.Replace("{}", algorithmEnc));
                hex.Write(cbc, ModeFile.CBC.Replace("{}", algorithmEnc));
                hex.Write(cfb, ModeFile.CFB.Replace("{}", algorithmEnc));
            }
        }

        private byte[] Encrypt(string plainText, Rijndael rijAlg, CipherMode mode)
        {
            rijAlg.Mode = mode;
            byte[] encrypted;
            using (ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV))
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
            return encrypted;
        }

        public void PerformDecryptionAnalysis()
        {
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;
                rijAlg.Padding = PaddingMode.None;


                var ecb = Decrypt(hex.Read(ModeFile.ECB.Replace("{}", algorithmEnc)), rijAlg, CipherMode.ECB);
                var cbc = Decrypt(hex.Read(ModeFile.CBC.Replace("{}", algorithmEnc)), rijAlg, CipherMode.CBC);
                var cfb = Decrypt(hex.Read(ModeFile.CFB.Replace("{}", algorithmEnc)), rijAlg, CipherMode.CFB);

                hex.Write(ecb, ModeFile.ECB.Replace("{}", algorithmDec));
                hex.Write(cbc, ModeFile.CBC.Replace("{}", algorithmDec));
                hex.Write(cfb, ModeFile.CFB.Replace("{}", algorithmDec));
            }
        }

        private byte[] Decrypt(byte[] cipherText, Rijndael rijAlg, CipherMode mode)
        {
            rijAlg.Mode = mode;
            byte[] decrypted;
            try
            {
                using (ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV))
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decrypted = Encoding.UTF8.GetBytes(srDecrypt.ReadToEnd());
                        }
                    }
                }
                return decrypted;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return new byte[0];
        }
    }
}
