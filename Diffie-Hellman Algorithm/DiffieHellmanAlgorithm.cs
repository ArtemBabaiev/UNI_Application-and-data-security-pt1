using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diffie_Hellman_Algorithm
{
    internal class DiffieHellmanAlgorithm
    {
        long a;
        long p;
        public long PrivateKey { get; set; }

        public DiffieHellmanAlgorithm(long a, long p)
        {
            this.a = a;
            this.p = p;
            PrivateKey = new Random().Next(1, 10);
        }

        public long GenerateKey()
        {
            return PowerModule(a, PrivateKey, p);
        }

        public long GetSecretKey(long x)
        {
            return PowerModule(x, PrivateKey, p);
        }

        private long PowerModule(long number, long power, long mod)
        {
            if (power == 1)
                return number;
            var powered = Math.Pow(number, power);
            return (long)powered % mod;
        }
    }
}
