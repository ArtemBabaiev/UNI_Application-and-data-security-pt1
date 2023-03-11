using ClassLibrary;
using System.Collections;
using System.Text;

namespace S_DES
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var key = new int[] { 1, 0, 1, 0, 1, 1, 0, 0, 1, 0 };
            var sdes = new SDesAlgorithm(key);
            string message = "Hello world";
            //string message = FileManager.ReadSentence();
            string binaryMessage = StringToBinaryString(Encoding.UTF8, message);
            Console.WriteLine($"Message: {message}");
            Console.WriteLine($"Original message in binary:\t{binaryMessage}");
            var encrypted = sdes.Process(binaryMessage, DesProcess.Ecryption);
            Console.WriteLine($"Encrypted message in binary:\t{encrypted}");
            Console.WriteLine("".PadLeft(100, '*'));
            var decrypted = sdes.Process(encrypted, DesProcess.Decryption);
            Console.WriteLine($"Decrypted message in binary:\t{decrypted}");
            Console.WriteLine($"Decrypted message: {BinaryStringToString(decrypted)}");

        }

        public static string StringToBinaryString(Encoding encoding, string text)
        {
            return string.Join(" ", encoding.GetBytes(text).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
        }

        public static string BinaryStringToString(string byteString)
        {
            string[] bytesAsString = byteString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            byte[] bytes = new byte[bytesAsString.Length];
            for (int i = 0; i < bytesAsString.Length; i++)
            {
                string? item = bytesAsString[i];
                bytes[i] = Convert.ToByte(Convert.ToInt32(item, 2));
            }
            return Encoding.UTF8.GetString(bytes);
        }

    }
}