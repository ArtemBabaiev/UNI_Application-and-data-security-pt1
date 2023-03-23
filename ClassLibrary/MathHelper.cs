using System;

namespace ClassLibrary
{
    public static class MathHelper
    {
        public static bool IsCoprime(int x, int y)
        {
            return Gcd(x, y) == 1;
        }

        public static int Gcd(int a, int b)
        {
            if (a == 0 || b == 0)
                return 0;

            if (a == b)
                return a;

            if (a > b)
                return Gcd(a - b, b);

            return Gcd(a, b - a);
        }

        public static int ModInverse(int A, int M)
        {
            for (int X = 1; X < M; X++)
                if (A % M * (X % M) % M == 1)
                    return X;
            return 1;
        }

        public static IEnumerable<long> SieveOfEratosthenes(long n)
        {
            bool[] prime = new bool[n + 1];
            for (int i = 0; i < n+1; i++)
            {
                prime[i] = true;
            }
            for (int p = 2; p * p <= n; p++)
            {
                if (prime[p])
                {
                    for (int i = p * 2; i <= n; i += p)
                    {
                        prime[i] = false;
                    }
                }
            }
            var primeNumbers = new LinkedList<long>();
            for (int i = 2; i <= n; i++)
            {
                if (prime[i])
                {
                    primeNumbers.AddLast(i);
                }
            }
            return primeNumbers;
        }

        public static int FindPrimitive(long n)
        {
            long power(long x, long y, long p)
            {
                long res = 1;     // Initialize result

                x = x % p; // Update x if it is more than or
                           // equal to p

                while (y > 0)
                {
                    // If y is odd, multiply x with result
                    if (y % 2 == 1)
                    {
                        res = (res * x) % p;
                    }

                    // y must be even now
                    y = y >> 1; // y = y/2
                    x = (x * x) % p;
                }
                return res;
            }

            void findPrimefactors(HashSet<long> s, long n)
            {
                // Print the number of 2s that divide n
                while (n % 2 == 0)
                {
                    s.Add(2);
                    n = n / 2;
                }

                // n must be odd at this point. So we can skip
                // one element (Note i = i +2)
                for (int i = 3; i <= Math.Sqrt(n); i = i + 2)
                {
                    // While i divides n, print i and divide n
                    while (n % i == 0)
                    {
                        s.Add(i);
                        n = n / i;
                    }
                }

                // This condition is to handle the case when
                // n is a prime number greater than 2
                if (n > 2)
                {
                    s.Add(n);
                }
            }

            HashSet<long> s = new HashSet<long>();

            long phi = n - 1;

            findPrimefactors(s, phi);

            for (int r = 2; r <= phi; r++)
            {
                bool flag = false;
                foreach (int a in s)
                {

                    if (power(r, phi / (a), n) == 1)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == false)
                {
                    return r;
                }
            }

            return -1;
        }
    }
}
