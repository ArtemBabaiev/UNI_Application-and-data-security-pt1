namespace SimpleSubstitutionCipher
{
    internal class SubstitutionTableGenerator
    {
        public IEnumerable<int> GenerateTable(int size)
        {
            List<int> table = new List<int>();
            List<int> values = Enumerable.Range(0, size).ToList();

            Random rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                var index = rnd.Next(0, size - i);
                table.Add(values[index]);
                values.RemoveAt(index);
            }

            return table;
        }
    }
}
