namespace Lab11.Data_For_NIST_STS
{
    ///cygdrive/c/users/artem/documents/My Programs/sts-2.1.2
    internal class Program
    {
        static void Main(string[] args)
        {
            var started = DateTime.Now;
            /*Console.WriteLine("Random sequence");
            RngPart.Generate();*/
            Console.WriteLine("Encryption");
            /*EncryptionPart.ProccessAes();
            EncryptionPart.ProccessTDes();*/
            EncryptionPart.ProccessDes();
            var ended = DateTime.Now;

            Console.WriteLine($"Total Time {(ended - started).TotalMinutes}");

        }
    }
}