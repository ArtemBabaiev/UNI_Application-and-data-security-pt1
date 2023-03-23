using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CesarCipher
{
    public static class CesarEncryptor
    {
        private static string ALPHABET = FileManager.ReadAplhabet();

        public static string Encrypt(string source, int shift)
        {
            StringBuilder encryptedBuilder = new StringBuilder();
            source = source.ToLower();

            for (int i = 0; i < source.Length; i++)
            {
                int sourceCharPos = ALPHABET.IndexOf(source[i]);
                if (sourceCharPos == -1)
                {
                    encryptedBuilder.Append(source[i]);
                    continue;
                }
                int possiblePos = (shift + sourceCharPos) % ALPHABET.Length;
                int encryptedCharPos = possiblePos >= 0? possiblePos: ALPHABET.Length + possiblePos;
                encryptedBuilder.Append(ALPHABET[encryptedCharPos]);
            }

            return encryptedBuilder.ToString();
        }
    }
}
