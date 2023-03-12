using ClassLibrary;
using System.Text;

namespace RSA
{
    internal class RsaDecryptor
    {

        public int N { get; private set; }
        public int E { get; private set; }
        public int D { get; private set; }

        string ALPHABET { get; set; }

        public RsaDecryptor(int n, int e, int d)
        {
            N = n;
            E = e;
            D = d;
            ALPHABET = FileManager.ReadTextFrom(PathConst.ALPABET_PATH).ToLower();
        }

        public string Decrypt(IEnumerable<int> cipher)
        {
            var result = new StringBuilder();
            foreach (var item in cipher)
            {
                var index = Convert.ToInt32(Math.Pow(item, D) % N);
                result.Append(ALPHABET[index]);
            }
            return result.ToString();
        }

        public IEnumerable<int> Decrypt(params int[] cipher)
        {
            var result = new List<int>();
            foreach (var item in cipher)
            {
                var index = Convert.ToInt32(Math.Pow(item, D) % N);
                result.Add(index);
            }
            return result;
        }
    }
}
