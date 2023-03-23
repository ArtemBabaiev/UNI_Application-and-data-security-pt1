using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class StringHelper
    {
        public static string StringToBinaryString(Encoding encoding, string text, string byteSeparator = " ")
        {
            return string.Join(byteSeparator, encoding.GetBytes(text).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
        }
        public static string BinaryStringToString(string binaryString)
        {
            byte[] bytes = new byte[binaryString.Length/8];
            int index = 0;
            for (int i = 0; i < binaryString.Length; i+=8)
            {
                var strByte = binaryString[i..(i+8)];
                bytes[index] = Convert.ToByte(Convert.ToInt32(strByte, 2));
                index++;
            }
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
