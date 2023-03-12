namespace ClassLibrary
{
    public static class PrintFunctions
    {
        public enum Print1dOption
        {
            /// <summary>
            /// Without spacing
            /// </summary>
            MERGED,
            /// <summary>
            /// With spacing
            /// </summary>
            SEPARATED
        }
        public enum Print2dOption
        {
            /// <summary>
            /// Primary usage for bit array
            /// </summary>
            IN_POW,
            /// <summary>
            /// Primary usage for printing matrix
            /// </summary>
            IN_TABLE
        }
        public static void PrintEnumerable<T>(IEnumerable<T> values, Print1dOption option, string separator = " ")
        {
            bool isSeparated = option == Print1dOption.SEPARATED;
            foreach (var item in values)
            {
                Console.Write($"{item}{(isSeparated ? separator : string.Empty)}");
            }
            Console.WriteLine();
        }

        public static void PrintMatrix<T>(T[,] matrix, Print2dOption option)
        {
            bool inRow = option == Print2dOption.IN_POW;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]}{(inRow ? string.Empty : '\t')}");
                }
                Console.Write(inRow ? ' ' : '\n');
            }
            if (inRow) { Console.WriteLine(); }
        }
        public static void PrintMatrix<T>(T[][] matrix, Print2dOption option)
        {
            bool inRow = option == Print2dOption.IN_POW;
            foreach (var row in matrix)
            {
                foreach (var item in row)
                {
                    Console.Write($"{item}{(inRow ? string.Empty : '\t')}");
                }
                Console.Write(inRow ? ' ' : '\n');
            }
            if (inRow) { Console.WriteLine(); }
        }

    }
}
