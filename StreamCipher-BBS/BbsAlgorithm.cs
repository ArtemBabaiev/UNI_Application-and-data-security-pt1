using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamCipher_BBS
{
    internal class BbsAlgorithm
    {
        long p;
        long q;
        long n;
        long x;

        public BbsAlgorithm(long p, long q)
        {
            n = p * q;
            var primeNumbers = MathHelper.SieveOfEratosthenes(1000);
            var index = new Random().Next(primeNumbers.Count());
            x = primeNumbers.ElementAt(index);
            if (!MathHelper.IsCoprime((int)x, (int)(p * q)))
            {
                throw new ArgumentException("X and N are not coprime");
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Item1=n; Item2=x</returns>
        public Tuple<long, long> GetKeys()
        {
            return new Tuple<long, long>(n, x);
        }

        public List<int> GetSequence(int length)
        {
            return GetSequence(length, n, x);
        }
        public static List<int> GetSequence(int length, long n, long x)
        {
            long x0 = (long)(Math.Pow(x, 2) % n);
            List<long> xs = new List<long>();
            List<int> ss = new List<int>();
            xs.Add(x0);
            ss.Add((int)x0 % 2);
            for (int i = 1; i < length; i++)
            {
                var xi = (long)(Math.Pow(xs[i - 1], 2) % n);
                xs.Add(xi);
                ss.Add((int)xi % 2);
            }
            return ss;
        }
    }
}
