using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CesarCipher
{
    internal static class FileManager
    {
        public static string ReadAplhabet()
        {
            return File.ReadAllText(@"res\alphabet.txt");
        }

        public static string ReadEncrypted()
        {
            return File.ReadAllText(@"res\encrypted.txt");
        }

        public static string ReadSentence()
        {
            return File.ReadAllText(@"res\sentence.txt");
        }

        public static void WriteEncrypted(string encryptedSentence)
        {
            File.WriteAllText(@"res\encrypted.txt", encryptedSentence);
        }

        public static Dictionary<char, double> ReadFrequency()
        {
            string[] strings = File.ReadAllLines(@"res\frequency.txt");
            Dictionary<char, double> count = new Dictionary<char, double>();
            foreach (var dataString in strings)
            {
                string[] datas = dataString.Split(" ");
                count.Add(datas[0].ToLower()[0], Double.Parse(datas[1]));
            }
            count = count.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return count;
        }
    }
}
