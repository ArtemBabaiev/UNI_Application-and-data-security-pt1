using ClassLibrary;

namespace RSA
{
    internal class RsaEncryptor
    {
        public int N { get; private set; }
        public int E { get; private set; }
        public string ALPHABET { get; set; }

        public RsaEncryptor(int n, int e)
        {
            N = n;
            E = e;
            ALPHABET = FileManager.ReadTextFrom(PathConst.ALPABET_PATH).ToLower();
        }

        public IEnumerable<int> Encrypt(string plainText)
        {
            var result = new List<int>();
            foreach (var item in plainText)
            {
                int index = ALPHABET.IndexOf(char.ToLower(item));
                if (index == -1)
                {
                    throw new Exception($"Symbol \"{item}\" is not in the alphabet");
                }
                result.Add(Convert.ToInt32(Math.Pow(index, E) % N));
            }

            return result;
        }

        public IEnumerable<int> Encrypt(params int[] args)
        {
            var result = new List<int>();
            foreach (var item in args)
            {
                result.Add(Convert.ToInt32(Math.Pow(item, E) % N));
            }

            return result;
        }

    }
}
