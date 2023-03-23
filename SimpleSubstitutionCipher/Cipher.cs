using CesarCipher;
using System.Text;

namespace SimpleSubstitutionCipher
{
    internal class Cipher
    {
        readonly string alphabet;

        public Cipher(string alphabet)
        {
            this.alphabet = alphabet;
        }

        public IEnumerable<int> Encrypt(string message, int shift, IEnumerable<int> substitution)
        {
            List<int> result = new List<int>();
            var encryptedCesar = CesarEncryptor.Encrypt(message, shift);
            foreach (var symbol in encryptedCesar)
            {
                var indexOfSymbol = alphabet.IndexOf(symbol);
                var subPosition = substitution.ElementAt(indexOfSymbol);
                result.Add(subPosition);
            }
            return result;
        }

        public string Decrypt(IEnumerable<int> cipher, int shift, IEnumerable<int> substitution)
        {
            var cesarChars = new StringBuilder();
            foreach (var symbol in cipher)
            {
                var alphPosition = substitution.TakeWhile(x => x != symbol).Count();
                var letter = alphabet[alphPosition];
                cesarChars.Append(letter);
            }

            var result = CesarDecryptor.Decrypt(cesarChars.ToString(), shift);

            return result;
        }
    }
}
