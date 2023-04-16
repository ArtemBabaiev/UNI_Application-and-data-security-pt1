using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockCipherMode_Analysis
{
    internal static class ModeFile
    {
        public const string ECB = @"output/{}ECB.txt";
        public const string CTS = @"output/{}CTS.txt";
        public const string CBC = @"output/{}CBC.txt";
        public const string CFB = @"output/{}CFB.txt";
        public const string OFB = @"output/{}OFB.txt";
    }
}
