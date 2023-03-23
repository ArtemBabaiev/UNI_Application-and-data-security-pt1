using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffineCipher
{
    internal class AffineDecryptor
    {
        public string Decrypt(string encrypted, Tuple<int, int> key)
        {
            int a = key.Item1;
            int b = key.Item2;
            string alphabet = FileManager.ReadAplhabet();
            int m = alphabet.Length;
            if (a <= 0 || a >= m || b <= 0 || b >= m)
            {
                throw new Exception("Key values bigger than alphabet capacity");
            }
            if (!MathHelper.IsCoprime(a, m))
            {
                throw new Exception("a key is not coprime with m");
            }
            int inverseA = MathHelper.ModInverse(a, m);

            StringBuilder source = new();
            foreach (var character in encrypted)
            {
                var isUpper = char.IsUpper(character);
                int ecryptedIndex = alphabet.IndexOf(char.ToLower(character));
                if (ecryptedIndex == -1) 
                {
                    source.Append(character);
                    continue;
                }
                int sourceIndex = (inverseA * (ecryptedIndex - b)) % m;
                sourceIndex = sourceIndex < 0? 26+sourceIndex: sourceIndex;
                var toAdd = alphabet[sourceIndex];
                source.Append(isUpper ? char.ToUpper(toAdd) : toAdd);
            }


            return source.ToString();
        }


        
    }
}
