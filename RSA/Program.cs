using ClassLibrary;

namespace RSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //abcdefghijklmnopqrstuvwxyz ,.?!;-"'
            //АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ 
            int p = 3;
            int q = 11;
            int n = p * q;
            int E = 7;
            int D = 3;

            try
            {
                CheckParameters(p, q, E, D);
                EncryptionTask(n, E);
                Console.WriteLine("".PadLeft(50, '*'));
                DecryptionTask(n, E, D);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void EncryptionTask(int n, int E)
        {
            var rsaEnc = new RsaEncryptor(n, E);
            var hashing = new SimpleHash();

            var message = FileManager.ReadTextFrom(PathConst.TEXT_PATH).ToLower();

            var hash = hashing.GetHashOf(message);
            var e_sign = rsaEnc.Encrypt(hash).First();
            FileManager.WriteTextTo(PathConst.SIGNATURE_PATH, $"{e_sign}");
            Console.WriteLine($"Original hash: {hash}");
            Console.WriteLine($"Original signature: {e_sign}");

            var encrypted = rsaEnc.Encrypt(message);
            Console.WriteLine("Encrypted:");
            PrintFunctions.PrintEnumerable(encrypted, PrintFunctions.Print1dOption.SEPARATED);
            Console.WriteLine("\n".PadRight(50, '*'));
            FileManager.WriteTextTo(PathConst.ENCRYPTED_PATH, string.Join(' ', encrypted));
        }

        public static void DecryptionTask(int n, int E, int D)
        {
            var rsaDec = new RsaDecryptor(n, E, D);
            var encrypted = FileManager.ReadTextFrom(PathConst.ENCRYPTED_PATH)
                .Split(' ')
                .Select(str => Convert.ToInt32(str));
            var decrypted = rsaDec.Decrypt(encrypted);
            Console.WriteLine($"Decrypted:\n{decrypted}");

            var hashing = new SimpleHash();
            var hash = hashing.GetHashOf(decrypted);
            Console.WriteLine($"Hash of decrypted: {hash}");

            var key = Convert.ToInt32(FileManager.ReadTextFrom(PathConst.SIGNATURE_PATH));
            var decryptedHash = rsaDec.Decrypt(key).First();
            Console.WriteLine($"Singature from file: {key}");
            Console.WriteLine($"Decrypted hash: {decryptedHash}");

            if (decryptedHash != hash)
            {
                Console.WriteLine("Inappropriate Signature. File was tampered with");
            }
        }


        static bool CheckParameters(int p, int q, int E, int D)
        {
            int n = p * q;
            int phi = (p - 1) * (q - 1);
            if (E <= 1 || E >= phi)
            {
                throw new ArgumentException($"E({E}) out of Range ({1}; {phi})");
            }
            if (!MathHelper.IsCoprime(E, phi))
            {
                throw new ArgumentException($"E({E}) and phi({phi}) are not coprime");
            }
            if (MathHelper.ModInverse(E, phi) != D)
            {
                throw new ArgumentException($"E({E}) and D({D}) are not modular multiplicative inversable");
            }
            return true;
        }
    }
}