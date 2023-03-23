using ClassLibrary;

namespace SimpleSubstitutionCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SubstitutionTableGenerator generator = new SubstitutionTableGenerator();
            string ALPHABET = FileManager.ReadAplhabet();
            string message = FileManager.ReadTextFrom(PathConst.TEXT_PATH);
            var table = generator.GenerateTable(ALPHABET.Length);
            int shift;
            Console.Write("Enter shift: ");
            while (!int.TryParse(Console.ReadLine(), out shift))
            {
            }
            EncryptTask(ALPHABET, message, shift, table);

            Console.WriteLine("".PadLeft(100, '*'));

            var encrypted = FileManager.ReadTextFrom(PathConst.ENCRYPTED_PATH);
            var numbers = encrypted.Split(' ');
            int fileShift = Convert.ToInt32(numbers[0]);
            var size = ALPHABET.Length;
            var fileTable = numbers[1..(size + 1)].Select(num => Convert.ToInt32(num));
            var fileCipher = numbers[(size + 1)..].Select(num => Convert.ToInt32(num));

            DecryptTask(ALPHABET, fileCipher, fileShift, fileTable);

        }

        static void DecryptTask(string alphabet, IEnumerable<int> encrypted, int shift, IEnumerable<int> table)
        {
            Cipher cipher = new Cipher(alphabet);
            var decrypted = cipher.Decrypt(encrypted, shift, table);
            Console.WriteLine($"Decrypted:\n{decrypted}");
        }


        static void EncryptTask(string alphabet, string message, int shift,IEnumerable<int> table)
        {
            Cipher cipher = new Cipher(alphabet);
            
            var encrypted = cipher.Encrypt(message, shift, table);

            var toWrite = $"{shift} {string.Join(" ", table)} {string.Join(" ", encrypted)}";
            FileManager.WriteTextTo(PathConst.ENCRYPTED_PATH, toWrite);

            Console.WriteLine($"Encrypted:");
            PrintFunctions.PrintEnumerable(encrypted, PrintFunctions.Print1dOption.SEPARATED, " ");
        }
    }
}