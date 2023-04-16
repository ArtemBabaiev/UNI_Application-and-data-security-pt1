using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockCipherMode_Analysis
{
    internal class HexFile
    {
        public HexFile()
        {
        }

        public void Write(byte[] bytes, string outputFile)
        {
            File.Delete(outputFile);
            using (var writer = new BinaryWriter(File.OpenWrite(outputFile)))
            {
                writer.Write(bytes);
            }
        }
        public byte[] Read(string inputFile)
        {
            byte[] read;
            using (var reader = new BinaryReader(File.OpenRead(inputFile)))
            {
                read = reader.ReadAllBytes();
            }
            return read;
        }
    }

}
