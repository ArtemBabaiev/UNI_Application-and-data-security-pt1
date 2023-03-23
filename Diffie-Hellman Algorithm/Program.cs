using ClassLibrary;

namespace Diffie_Hellman_Algorithm
{
    internal class Program
    {
       static void Main(string[] args)
        {
            var primeNumbers = MathHelper.SieveOfEratosthenes(2000);
            long p = primeNumbers.Last();
            long a = MathHelper.FindPrimitive(p);
            Console.WriteLine($"Public keys: p={p}; a={a}");
            var alg1 = new DiffieHellmanAlgorithm(a, p);
            var alg2 = new DiffieHellmanAlgorithm(a, p);

            var pr1 = alg1.GenerateKey();
            Console.WriteLine($"First generated key {pr1}");
            var pr2 = alg2.GenerateKey();
            Console.WriteLine($"Second generated key {pr2}");

            var key1 = alg1.GetSecretKey(pr2);
            var key2 = alg2.GetSecretKey(pr1);
            Console.WriteLine($"{key1} -> {key2}");
        }

        private static long power(long a, long b, long P)
        {
            if (b == 1)
                return a;

            else
                return (((long)Math.Pow(a, b)) % P);
        }
        

    }
}