using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace S_DES
{
    public enum DesProcess
    {
        Ecryption, Decryption
    }

    public class SDesAlgorithm
    {
        int[] P10 = { 3, 5, 2, 7, 4, 10, 1, 9, 8, 6 };
        int[] P8 = { 6, 3, 7, 4, 8, 5, 10, 9 };
        int[] IP = { 2, 6, 3, 1, 4, 8, 5, 7 };
        int[] IPrev = { 4, 1, 3, 5, 7, 2, 8, 6 };
        int[] EP = { 4, 1, 2, 3, 2, 3, 4, 1 };
        int[] P4 = { 2, 4, 3, 1 };
        int[,] S0 = { { 1, 0, 3, 2 },
                      { 3, 2, 1, 0 },
                      { 0, 2, 1, 3 },
                      { 3, 1, 3, 2 } };
        int[,] S1 = { { 0, 1, 2, 3 },
                      { 2, 0, 1, 3 },
                      { 3, 0, 1, 0 },
                      { 2, 1, 0, 3 } };

        int[] key;

        public int[] Key { get => key; set => key = value; }
        public int[,] S01 { get => S0; set => S0 = value; }

        public SDesAlgorithm(int[] key)
        {
            this.key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public string Process(string plainText, DesProcess desProcess)
        {
            var keys = GenerateKeys();
            var key1 = keys.Item1;
            var key2 = keys.Item2;
            var textBlocks = ParsePlainText(plainText);
            StringBuilder result = new();
            for (int i = 0; i < textBlocks.GetLength(0); i++)
            {
                int[] ipPermuted = IP_Permutation(textBlocks[i]);

                var processed = desProcess == DesProcess.Ecryption ? CycleFunction(ipPermuted, key1, key2) : CycleFunction(ipPermuted, key2, key1);

                var encryptedBlock = IPrev_Permutation(processed);
                foreach (var item in encryptedBlock)
                {
                    result.Append(item);
                }
                if (i != textBlocks.GetLength(0) - 1)
                {
                    result.Append(" ");
                }
            }
            return result.ToString();
        }

        public Tuple<int[], int[]> GenerateKeys()
        {
            var permutated10Key = P10_Permutation();
            var firstHalf = Shift(permutated10Key[0..5], 1);
            var secondHalf = Shift(permutated10Key[5..], 1);
            var key1 = P8_Permutation(firstHalf.Concat(secondHalf).ToArray());
            firstHalf = Shift(firstHalf, 2);
            secondHalf = Shift(secondHalf, 2);
            var key2 = P8_Permutation(firstHalf.Concat(secondHalf).ToArray());

            return new Tuple<int[], int[]>(key1, key2);
        }

        public int[] P10_Permutation()
        {
            var permutated = new int[10];
            for (int i = 0; i < 10; i++)
            {
                permutated[i] = key[P10[i] - 1];
            }
            return permutated;
        }

        public int[] P8_Permutation(int[] arr)
        {
            var permutated = new int[8];
            for (int i = 0; i < 8; i++)
            {
                permutated[i] = arr[P8[i] - 1];
            }
            return permutated;
        }

        public int[] Shift(int[] arr, int n)
        {
            while (n > 0)
            {
                int temp = arr[0];
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    arr[i] = arr[i + 1];
                }
                arr[arr.Length - 1] = temp;
                n--;
            }
            return arr;
        }

        public int[][] ParsePlainText(string plainText)
        {
            var bytes = plainText.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
            int[][] blocks = new int[bytes.Length][];
            for (int i = 0; i < bytes.Length; i++)
            {
                string? str = bytes[i];
                blocks[i] = new int[str.Length];
                for (int j = 0; j < str.Length; j++)
                {
                    char number = str[j];
                    blocks[i][j] = int.Parse(number.ToString());
                }
            }
            return blocks;
        }

        public int[] IP_Permutation(int[] block)
        {
            int[] permutated = new int[block.Length];
            for (int i = 0; i < permutated.Length; i++)
            {
                permutated[i] = block[IP[i] - 1];
            }
            return permutated;
        }

        public int[] IPrev_Permutation(int[] block)
        {
            int[] permutated = new int[block.Length];
            for (int i = 0; i < permutated.Length; i++)
            {
                permutated[i] = block[IPrev[i] - 1];
            }
            return permutated;
        }

        public int[] CycleFunction(int[] block, int[] key1, int[] key2)
        {
            int[] L = block[0..4];
            int[] R = block[4..];
            int[] newL = CycleLogic(L, R, key1);
            int[] newR = CycleLogic(R, newL, key2);
            return newR.Concat(newL).ToArray();
        }

        public int[] CycleLogic(int[] L, int[] R, int[] key)
        {
            var expanded = EP_Permutation(R, key);
            for (int i = 0; i < expanded.Length; i++)
            {
                expanded[i] = expanded[i] ^ key[i];
            }
            var firstBits = S0_Subtitution(expanded[0..4]);
            var secondBits = S1_Subtitution(expanded[4..]);
            var permutated4 = P4_Permutation(firstBits.Concat(secondBits).ToArray());
            int[] newL = new int[4];
            for (int i = 0; i < 4; i++)
            {
                newL[i] = (permutated4[i] ^ L[i]);

            }
            return newL;
        }


        public int[] EP_Permutation(int[] R, int[] key)
        {
            int[] expanded = new int[R.Length * 2];
            for (int i = 0; i < expanded.Length; i++)
            {
                expanded[i] = R[EP[i] - 1];
            }

            return expanded;
        }

        public int[] S0_Subtitution(int[] arr)
        {
            int[] bits = new int[2];
            int i = Convert.ToInt32(arr[0].ToString() + arr[3].ToString(), 2);
            int j = Convert.ToInt32(arr[1].ToString() + arr[2].ToString(), 2);
            var value = Convert.ToString(S0[i, j], 2).PadLeft(2, '0');
            bits[0] = Convert.ToInt32(value[0].ToString());
            bits[1] = Convert.ToInt32(value[1].ToString());
            return bits;
        }

        public int[] S1_Subtitution(int[] arr)
        {
            int[] bits = new int[2];
            int i = Convert.ToInt32(arr[0].ToString() + arr[3].ToString(), 2);
            int j = Convert.ToInt32(arr[1].ToString() + arr[2].ToString(), 2);
            var value = Convert.ToString(S1[i, j], 2).PadLeft(2, '0');
            bits[0] = Convert.ToInt32(value[0].ToString());
            bits[1] = Convert.ToInt32(value[1].ToString());
            return bits;
        }

        public int[] P4_Permutation(int[] arr)
        {
            int[] permutated = new int[arr.Length];
            for (int i = 0; i < permutated.Length; i++)
            {
                permutated[i] = arr[P4[i] - 1];
            }
            return permutated;
        }
    }
}
