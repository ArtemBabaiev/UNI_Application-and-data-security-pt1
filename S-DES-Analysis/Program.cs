using ClassLibrary;
using S_DES;
using System;
using System.Text;

namespace S_DES_Analysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task2();
            Console.WriteLine("".PadLeft(50, '-'));
            Task3();
            Console.WriteLine("".PadLeft(50, '-'));
            Task4();
            Console.WriteLine("".PadLeft(50, '-'));
            Task5();
        }

        public static void Task2()
        {
            int[] key = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string message = StringToBinaryString("H");
            SDesAlgorithm sdes = new(key);
            PrintData(message, key, sdes.Process(message, DesProcess.Ecryption));
            key[key.Length - 1] = 1;
            for (int i = 0; i < key.Length; i++)
            {
                PrintData(message, key, sdes.Process(message, DesProcess.Ecryption));
                ArrayOperations.Shift(key, 1);
            }
            key = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            PrintData(message, key, sdes.Process(message, DesProcess.Ecryption));
        }

        public static void Task3()
        {
            StringBuilder message = new("00000000");
            int[] key = { 1, 0, 1, 1, 0, 0, 1, 1, 0, 1 };
            var sdes = new SDesAlgorithm(key);

            PrintData(message.ToString(), key, sdes.Process(message.ToString(), DesProcess.Ecryption));

            message[message.Length - 1] = '1';
            for (int i = 0; i < message.Length; i++)
            {
                PrintData(message.ToString(), key, sdes.Process(message.ToString(), DesProcess.Ecryption));
                ShiftString(message, 1);
            }
        }

        public static void Task4()
        {
            int[] key = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string message = StringToBinaryString("kljsdgflkdsgnsfdl");
            SDesAlgorithm sdes = new SDesAlgorithm(key);
            int[,] S = { { 0, 0, 0, 0 },
                         { 0, 0, 0, 0 },
                         { 0, 0, 0, 0 },
                         { 0, 0, 0, 0 } };
            int[,] Smax = new int[4,4];
            int maxCount = -1;
            for (int i = 0; i < Math.Pow(4, 16); i++)
            {
                var base4 = IntToString(i, new char[] { '0', '1', '2', '3' }).PadLeft(16, '0').ToArray().Select(el => Convert.ToInt32(el.ToString()));
                for (int j = 0; j < base4.Count(); j++)
                {
                    int x = j % 4;
                    int y = j / 4;

                    S[y, x] = base4.ElementAt(j);
                }
                sdes.S01 = S;
                var encrypted = sdes.Process(message, DesProcess.Ecryption);

                int count = 0;
                for (int j = 0; j < encrypted.Length; j++)
                {
                    if (message[j] != encrypted[j])
                    {
                        count++;
                    }
                }
                if (count> maxCount)
                {
                    maxCount = count;
                    Smax = S.Clone() as int[,];
                }
                if (i == 16384)
                {
                    break;
                }
            }
            Console.WriteLine($"Biggest amout of difference = {maxCount} with S=\n");
            PrintFunctions.PrintMatrix(Smax, PrintFunctions.Print2dOption.IN_TABLE);
        }

        public static void Task5()
        {
            int[] realKey = { 1, 0, 1, 1, 0, 0, 1, 1, 0, 1 };
            string message = StringToBinaryString("Hello");
            SDesAlgorithm sdes = new(realKey);

            string encrypted = sdes.Process(message, DesProcess.Ecryption);
            //Console.WriteLine($"Original: {message}");
            //Console.WriteLine($"Encrypted: {encrypted}");

            for (int i = 0; i < Math.Pow(2, 10); i++)
            {
                var binary = Convert.ToString(i, 2).PadLeft(10, '0').ToArray().Select(el => Convert.ToInt32(el.ToString()));
                sdes.Key = binary.ToArray();
                var decrypted = sdes.Process(encrypted, DesProcess.Decryption);
                if (decrypted == message)
                {
                    Console.WriteLine($"Cipher decrypted. Number of required operations {i + 1}");
                    //Console.WriteLine($"Decrypted: {decrypted}");
                    //PrintFunctions.PrintEnumerable(binary, PrintFunctions.Print1dOption.MERGED);
                    break;
                }
            }

        }



        public static string StringToBinaryString(string text)
        {
            return string.Join(" ", Encoding.UTF8.GetBytes(text).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
        }

        public static void PrintData(string message, int[] key, string encrypted)
        {
            Console.WriteLine(
                $"{message}\t--({ArrayOperations.EnumerableToString(key)})-->\t{encrypted}"
                );
        }

        public static void ShiftString(StringBuilder sb, int n)
        {
            while (n > 0)
            {
                var temp = sb[0];
                for (int i = 0; i < sb.Length - 1; i++)
                {
                    sb[i] = sb[i + 1];
                }
                sb[sb.Length - 1] = temp;
                n--;
            }
        }

        public static string IntToString(int value, char[] baseChars)
        {
            // 32 is the worst cast buffer size for base 2 and int.MaxValue
            int i = 32;
            char[] buffer = new char[i];
            int targetBase = baseChars.Length;

            do
            {
                buffer[--i] = baseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            char[] result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }
    }
}