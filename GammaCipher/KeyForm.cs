using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GammaCipher
{
    internal class KeyFormer
    {
        int alphLength;
        int textLength;

        public KeyFormer(int alphLength, int textLength)
        {
            this.alphLength = alphLength;
            this.textLength = textLength;
        }

        public List<int> GenerateGamma(Tuple<int, int, int> key)
        {
            var sequence = CalculateSequence(key);
            var gamma = new List<int>();
            
            for (int i = 0; i < textLength; i++)
            {
                //Zt=(Yt+Yt+1) mod length
                gamma.Add((sequence[i] + sequence[i + 1]) % alphLength);
            }
            return gamma;
        }

        private List<int> CalculateSequence(Tuple<int, int, int> key)
        {
            var sequence = new List<int>();
            sequence.Add(key.Item1);
            sequence.Add(key.Item2);
            sequence.Add(key.Item3);
            for (int i = 3; i <= textLength; i++)
            {
                //Yt=( Yt-1+ Yt-3) mod length
                sequence.Add((sequence[i - 1] + sequence[i - 3]) % alphLength);
            }
            /*foreach (var item in sequence)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();*/
            return sequence;
        }
    }
}
