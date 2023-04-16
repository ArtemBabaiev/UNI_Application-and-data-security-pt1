
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


namespace BlockCipherMode_Analysis
{
    internal class DesAnalysis
    {
        static string algorithmEnc = "Des-Enc-";
        static string algorithmDec = "Des-Dec-";
        private byte[] key;
        private byte[] iv;
        HexFile hex;

        public DesAnalysis()
        {
            hex = new HexFile();

            using (DES des = DES.Create())
            {
                key = des.Key;
                iv = des.IV;
            }
        }

        public void PerformEncryptionAnalysis(string plainText)
        {
            using (DES des = DES.Create())
            {
                des.Key = key;
                des.IV = iv;
                des.Padding = PaddingMode.None;

                var ecb = Encrypt(plainText, des, CipherMode.ECB);
                var cbc = Encrypt(plainText, des, CipherMode.CBC);
                var cfb = Encrypt(plainText, des, CipherMode.CFB);
                hex.Write(ecb, ModeFile.ECB.Replace("{}", algorithmEnc));
                hex.Write(cbc, ModeFile.CBC.Replace("{}", algorithmEnc));
                hex.Write(cfb, ModeFile.CFB.Replace("{}", algorithmEnc));
            }
        }

        public void PerformDecryptionAnalysis()
        {
            using (DES des = DES.Create())
            {
                des.Key = key;
                des.IV = iv;
                des.Padding = PaddingMode.None;

                var ecb = Decrypt(hex.Read(ModeFile.ECB.Replace("{}", algorithmEnc)), des, CipherMode.ECB);
                var cbc = Decrypt(hex.Read(ModeFile.CBC.Replace("{}", algorithmEnc)), des, CipherMode.CBC);
                var cfb = Decrypt(hex.Read(ModeFile.CFB.Replace("{}", algorithmEnc)), des, CipherMode.CFB);

                hex.Write(ecb, ModeFile.ECB.Replace("{}", algorithmDec));
                hex.Write(cbc, ModeFile.CBC.Replace("{}", algorithmDec));
                hex.Write(cfb, ModeFile.CFB.Replace("{}", algorithmDec));
            }
        }

        private byte[] Encrypt(string plainText, DES des, CipherMode mode)
        {
            des.Mode = mode;
            if (mode == CipherMode.CFB)
            {
                des.FeedbackSize = 8;
            }
            byte[] encrypted;

            using (ICryptoTransform encryptor = des.CreateEncryptor(key, iv))
            {
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
        }

        private byte[] Decrypt(byte[] cipherText, DES des, CipherMode mode)
        {
            des.Mode = mode;
            if (mode == CipherMode.CFB)
            {
                des.FeedbackSize = 8;
            }
            byte[] decrypted;
            try
            {

                using (ICryptoTransform decryptor = des.CreateDecryptor(des.Key, des.IV))
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
                Console.Error.WriteLine(ex.ToString());
            }
            return new byte[0];
        }
    }
}
