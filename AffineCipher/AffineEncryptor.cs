using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffineCipher
{
    internal class AffineEncryptor
    {
        public string Encrypt(string text, Tuple<int, int> key)
        {
            int a = key.Item1;
            int b = key.Item2;
            string alphabet = FileManager.ReadAplhabet();
            int m = alphabet.Length;
            if (a <= 0 || a >= m || b <= 0 || b >= m)
            {
                throw new Exception("Key values bigger than alphabet capacity");
            }
            if (!MathFunctions.IsCoprime(a, m))
            {
                throw new Exception("a key is not coprime with m");
            }

            //correspondens between source index and encrypted index
            Dictionary<int, int> indexCorrespondens = new Dictionary<int, int>();
            for (int i = 0; i < m; i++)
            {
                indexCorrespondens.Add(i, (a * i + b) % m);
            }

            StringBuilder encrypted = new StringBuilder();
            foreach (var character in text)
            {
                var isUpper = char.IsUpper(character);
                int sourceCharPos = alphabet.IndexOf(char.ToLower(character));
                if (sourceCharPos == -1)
                {
                    encrypted.Append(character);
                    continue;
                }
                var toAdd = alphabet[indexCorrespondens[sourceCharPos]];
                encrypted.Append(isUpper? char.ToUpper(toAdd): toAdd);
            }
            return encrypted.ToString();
        }


    }
}
