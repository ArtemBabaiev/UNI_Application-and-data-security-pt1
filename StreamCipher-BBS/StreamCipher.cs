using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamCipher_BBS
{
    internal class StreamCipher
    {
        long n;
        long x;

        public StreamCipher(long n, long x)
        {
            this.n = n;
            this.x = x;
        }

        public IEnumerable<int> Proccess(IEnumerable<int> message)
        {
            List<int> result = new List<int>();
            IEnumerable<int> sequense = GenerateSequence(message.Count());
            for (int i = 0; i < message.Count(); i++)
            {
                var bit = message.ElementAt(i);
                var seqBit = sequense.ElementAt(i);
                result.Add((bit + seqBit) % 2);
            }
            return result;
        }

        private IEnumerable<int> GenerateSequence(int length)
        {
            return BbsAlgorithm.GetSequence(length, n, x);
        }
    }
}
