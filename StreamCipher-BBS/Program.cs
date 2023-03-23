using ClassLibrary;
using System.Linq;

namespace StreamCipher_BBS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var primeNumbers = MathHelper.SieveOfEratosthenes(1000);
            long p = 19; 
            long q = 23;
            if (!CheckParams(p, q))
            {
                return;
            }

            var bbs = new BbsAlgorithm(p, q);
            var keys = bbs.GetKeys();
            var streamCipher = new StreamCipher(keys.Item1, keys.Item2);

            var message = StringHelper.StringToBinaryString(
                System.Text.Encoding.UTF8,
                "Hello world",
                "")
                .Select(bit => Convert.ToInt32(bit.ToString()));

            var sequence = bbs.GetSequence(message.Count());
            Console.WriteLine("Sequence");
            PrintFunctions.PrintEnumerable(sequence, PrintFunctions.Print1dOption.MERGED);

            Console.WriteLine("Message");
            PrintFunctions.PrintEnumerable(message, PrintFunctions.Print1dOption.MERGED);

            var encoded = streamCipher.Proccess(message);
            Console.WriteLine("Encoded");
            PrintFunctions.PrintEnumerable(encoded, PrintFunctions.Print1dOption.MERGED);

            var decoded = streamCipher.Proccess(encoded);
            Console.WriteLine("Decoded");
            PrintFunctions.PrintEnumerable(decoded, PrintFunctions.Print1dOption.MERGED);

            Console.WriteLine(StringHelper.BinaryStringToString(string.Join("", decoded)));





        }

        static bool CheckParams(long p, long q)
        {
            if ((p - 3) % 4 != 0)
            {
                Console.WriteLine("P is not valid");
                return false;
            }
            if ((q - 3) % 4 != 0)
            {
                Console.WriteLine("Q is not valid");
                return false;
            }
            return true;
        }
    }
}