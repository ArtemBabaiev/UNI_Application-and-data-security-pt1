using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class PrintFunctions
    {
        public static void PrintEnumerable<T>(IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }

        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j]);
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        public static void PrintMatrix(int[][] matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var item in row)
                {
                    Console.Write(item);
                }
                Console.Write(" ");
            }
            Console.WriteLine();
        }

    }
}
