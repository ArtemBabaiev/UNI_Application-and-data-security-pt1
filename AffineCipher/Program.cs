using ClassLibrary;
using System.Numerics;

namespace AffineCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string source = "";
            string encrypted = "";
            try
            {
                AffineEncryptor affineEncryptor = new AffineEncryptor();
                encrypted = affineEncryptor.Encrypt(
                    FileManager.ReadSentence(),
                    new Tuple<int, int>(5, 8)
                    );
                Console.WriteLine(encrypted);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("".PadLeft(50, '*'));

            try
            {
                AffineDecryptor decryptor = new AffineDecryptor();
                source = decryptor.Decrypt(FileManager.ReadEncrypted(), new Tuple<int, int>(5, 8));
                Console.WriteLine(source);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine(source == FileManager.ReadSentence());

        }
    }
}