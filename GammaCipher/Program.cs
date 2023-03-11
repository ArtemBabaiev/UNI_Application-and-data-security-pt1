using ClassLibrary;

namespace GammaCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sentence = FileManager.ReadSentence();
            string alphabet = FileManager.ReadAplhabet().ToLower();
            var encryptor = new Encryptor(alphabet);
            var encrypted = encryptor.Encrypt(sentence, new Tuple<int, int, int>(04, 31, 15));
            foreach (var item in encrypted)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            var decryptor = new Decryptor(alphabet);
            var decrypted = decryptor.Decrypt(encrypted, new Tuple<int, int, int>(04, 31, 15));
            Console.WriteLine(decrypted);
        }
    }
}