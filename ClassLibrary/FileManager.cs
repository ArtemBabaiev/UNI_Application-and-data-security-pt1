namespace ClassLibrary
{
    public static class FileManager
    {
        public static string ReadTextFrom(string path)
        {
            return File.ReadAllText(path);
        }

        public static void WriteTextTo(string path, string text)
        {
            File.WriteAllText(path, text);
        }

        public static Dictionary<char, double> ReadFrequency()
        {
            string[] strings = File.ReadAllLines(PathConst.FREQUENCY_PATH);
            Dictionary<char, double> count = new Dictionary<char, double>();
            foreach (var dataString in strings)
            {
                string[] datas = dataString.Split(" ");
                count.Add(datas[0].ToLower()[0], Double.Parse(datas[1]));
            }
            count = count.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return count;
        }

        #region Legacy Functions
        public static string ReadAplhabet()
        {
            return ReadTextFrom(PathConst.ALPABET_PATH);
        }

        public static string ReadEncrypted()
        {
            return ReadTextFrom(PathConst.ENCRYPTED_PATH);
        }

        public static string ReadSentence()
        {
            return ReadTextFrom(PathConst.TEXT_PATH);
        }

        public static void WriteEncrypted(string encryptedSentence)
        {
            WriteTextTo(PathConst.ENCRYPTED_PATH, encryptedSentence);
        }
        #endregion



    }
}
