using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;
using ClassLibrary;

namespace Lab11.Data_For_NIST_STS
{
    internal class RngPart
    {
        public static void Generate()
        {
            Console.WriteLine("Generation started");
            var arr = RandomNumberGenerator.GetBytes(12_500_000);
            Console.WriteLine("Generation finished");
            string str = string.Join("", arr.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            Console.WriteLine(str.Length);
            Console.WriteLine("Writing to file started");
            FileManager.WriteTextTo(@"res/sequence.txt", str);
            Console.WriteLine("Writing to file finished");
        }
    }
}
