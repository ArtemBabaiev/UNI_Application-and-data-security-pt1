using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CesarCipher
{
    internal class Analyzator
    {
        private static string ALPHABET = FileManager.ReadAplhabet();
        private static Dictionary<char, double> FREQUENCIES = FileManager.ReadFrequency();

        private Dictionary<char, double> GetFrequencies(string encrypted)
        {
            Dictionary<char, double> count = new Dictionary<char, double>();
            Regex reg = new Regex(@"[^\p{L}]");
            encrypted = reg.Replace(encrypted.ToLower(), string.Empty);
            foreach (var character in encrypted)
            {
                if (!count.ContainsKey(character))
                {
                    count.Add(character, 0);
                }
                count[character]++;
            }
            count = count.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            int length = encrypted.Length;
            foreach (var item in count)
            {
                count[item.Key] = (item.Value / length) * 100;
            }
            return count;
        }

        public string TryDecrypt(string encrypted, int tryNumber)
        {
            StringBuilder source = new StringBuilder();
            Dictionary<char, double> encryptedFrequecies = GetFrequencies(encrypted);
            if (tryNumber >= FREQUENCIES.Count)
            {
                return "Out of possibilities";
            }
            var tableValue = FREQUENCIES.ElementAt(tryNumber).Key;
            int sourceIndex = ALPHABET.IndexOf(tableValue);
            int ecryptedIndex = ALPHABET.IndexOf(encryptedFrequecies.First().Key);

            int shift = ecryptedIndex - sourceIndex;
            Console.WriteLine(shift);
            return CesarDecryptor.Decrypt(encrypted, shift);
        }
    }
}
