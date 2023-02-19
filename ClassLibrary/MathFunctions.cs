using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffineCipher
{
    public static class MathFunctions
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
                if (((A % M) * (X % M)) % M == 1)
                    return X;
            return 1;
        }
    }
}
