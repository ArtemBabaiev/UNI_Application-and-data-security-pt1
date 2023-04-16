namespace BlockCipherMode_Analysis
{
    internal class Program
    {
        static string plainTextRijndael = "Babaiev Babaiev Babaiev Babaiev Artem Illich Art";
        static string plainTextDes = "Babaiev Babaiev Babaiev Babaiev Babaiev Artem Il";
        static void Main(string[] args)
        {
            //RijndaelTask();
            DesTask();
        }
        public static void RijndaelTask()
        {
            RijndaelAnalysis rijndael = new RijndaelAnalysis();
            rijndael.PerformEncryptionAnalysis(plainTextRijndael);

            while (true)
            {
                rijndael.PerformDecryptionAnalysis();
                Console.WriteLine("Enter to continue");
                Console.ReadLine();
            }
        }
        public static void DesTask()
        {
            DesAnalysis des = new DesAnalysis();
            des.PerformEncryptionAnalysis(plainTextDes);
            while (true)
            {
                des.PerformDecryptionAnalysis();
                Console.WriteLine("Enter to continue");
                Console.ReadLine();
            }
        }
    }
}