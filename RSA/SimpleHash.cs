namespace RSA
{
    internal class SimpleHash
    {
        public int GetHashOf(string text)
        {
            int result = 0;

            foreach (char c in text)
            {
                result = (result + c) % 33;
            }
            return result;
        }
    }
}
