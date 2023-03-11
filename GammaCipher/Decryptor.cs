using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaCipher
{
    internal class Decryptor
    {
        readonly string alphabet;
        public Decryptor(string alphabet)
        {
            this.alphabet = alphabet;
        }

        public string Decrypt(List<int> cipher, Tuple<int, int, int> key)
        {
            KeyFormer keyFormer = new KeyFormer(alphabet.Length, cipher.Count);
            List<int> gamma = keyFormer.GenerateGamma(key);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < cipher.Count; i++)
            {
                sb.Append(
                    alphabet[
                            (cipher[i] + (alphabet.Length - gamma[i])) % alphabet.Length
                        ]
                    );
            }

            return sb.ToString();


        }
    }
}
