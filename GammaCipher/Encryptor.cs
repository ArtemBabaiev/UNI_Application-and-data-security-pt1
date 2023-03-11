using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GammaCipher
{
    internal class Encryptor
    {
        readonly string alphabet;
        public Encryptor(string alphabet)
        {
            this.alphabet = alphabet;
        }

        public List<int> Encrypt(string message, Tuple<int, int, int> key)
        {
            List<int> encrypted = new();
            KeyFormer keyFormer = new KeyFormer(alphabet.Length, message.Length);
            List<int> gamma = keyFormer.GenerateGamma(key);

            message = CleanText(message);
            /*foreach (var item in message)
            {
                Console.Write(alphabet.IndexOf(item) + " ");
            }
            Console.WriteLine();
            foreach (var item in gamma)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();*/
            for (int i = 0; i < message.Length; i++)
            {
                //(C(ti)+ki) mod length,
                encrypted.Add(
                    (alphabet.IndexOf(message[i]) + gamma[i]) % alphabet.Length);
            }


            return encrypted;
        }

        private char GetLeastUsedLetter()
        {
            return FileManager.ReadFrequency().Last().Key;
        }

        private string CleanText(string text)
        {
            Regex regex = new Regex(@"[^\p{L}]");
            return regex.Replace(text.ToLower(), GetLeastUsedLetter().ToString());
        }

    }
}
