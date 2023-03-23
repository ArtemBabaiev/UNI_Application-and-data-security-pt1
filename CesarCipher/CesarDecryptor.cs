using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CesarCipher
{
    public static class CesarDecryptor
    {
        private static string ALPHABET = FileManager.ReadAplhabet();

        public static string Decrypt(string encryted, int shift)
        {
            StringBuilder sourceBuilder = new StringBuilder();
            encryted = encryted.ToLower();

            for (int i = 0; i < encryted.Length; i++)
            {
                int encryptedCharPos = ALPHABET.IndexOf(encryted[i]);
                if (encryptedCharPos == -1)
                {
                    sourceBuilder.Append(encryted[i]);
                    continue;
                }
                int possiblePos = (encryptedCharPos - shift) % ALPHABET.Length;
                int sourceCharPos = possiblePos >= 0? possiblePos: ALPHABET.Length + possiblePos;
                sourceBuilder.Append(ALPHABET[sourceCharPos]);
            }

            return sourceBuilder.ToString();
        }
    }
}
