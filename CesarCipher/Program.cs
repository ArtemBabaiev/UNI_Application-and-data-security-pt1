using System.Text;
using System.Text.RegularExpressions;

namespace CesarCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1-Encrypt\n2-Decrypt\n3-Decrypt using analysis\nChoose option: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        EncryptTask();
                        break;
                    case "2":
                        DecryptTask();
                        break;
                    case "3":
                        AnalyzeTask();
                        break;
                    default:
                        Console.WriteLine("Incorrect option");
                        break;
                }
                Console.WriteLine("".PadLeft(25, '-'));
                Console.WriteLine("Close app?(Y/y)");
                string? exit = Console.ReadLine();
                if (exit == "Y" || exit == "y")
                {
                    break;
                }
            }
        }

        static void EncryptTask()
        {
            try
            {
                Console.WriteLine("Write text in sentence.txt. Press Enter to continue");
                Console.ReadLine();
                Console.Write("Key: ");
                int shift;
                while (!int.TryParse(Console.ReadLine(), out shift))
                {
                }
                string sentence = FileManager.ReadSentence();
                string encrypted = CesarEncryptor.Encrypt(sentence, shift);

                FileManager.WriteEncrypted(encrypted);

                Console.WriteLine("File succesfully encrypted");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Show Error message?(Y/y)");
                string? error = Console.ReadLine();
                if (error == "Y" || error == "y")
                {
                    Console.WriteLine(e.Message);
                }
            }

        }

        static void DecryptTask()
        {
            try
            {
                Console.WriteLine("Put encrypted text in encrypted.txt. Press Enter to continue");
                Console.ReadLine();
                Console.Write("Key: ");
                int shift;
                while (!int.TryParse(Console.ReadLine(), out shift))
                {
                }
                string encrypted = FileManager.ReadEncrypted();
                Console.WriteLine(CesarDecryptor.Decrypt(encrypted, shift));
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Show Error message?(Y/y)");
                string? error = Console.ReadLine();
                if (error == "Y" || error == "y")
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void AnalyzeTask()
        {
            try
            {
                Analyzator analyzator = new Analyzator();
                string encrypted = FileManager.ReadEncrypted();
                int i = 0;
                while (true)
                {
                    Console.WriteLine(analyzator.TryDecrypt(encrypted, i));
                    Console.WriteLine("".PadLeft(25, '-'));
                    Console.WriteLine("Sentence decypted?(Y/y)");
                    string? error = Console.ReadLine();
                    if (error == "Y" || error == "y")
                    {
                        break;
                    }
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Show Error message?(Y/y)");
                string? error = Console.ReadLine();
                if (error == "Y" || error == "y")
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}