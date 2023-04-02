using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using System.IO;

namespace Lab10.Client
{
    internal class Program
    {
        static SHA256 mySHA256 = SHA256.Create();
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7277/");
            int option;
            string strOption;
            string answer;
            do
            {

                do
                {
                    Console.WriteLine("Choose option:\n1.Register\n2.Login");
                } while (!int.TryParse(Console.ReadLine(), out option));

                switch (option)
                {
                    case 1:
                        await Register(client);
                        break;
                    case 2:
                        await Login(client);
                        break;
                    default:
                        Console.WriteLine("Incorect option");
                        break;
                }

                Console.WriteLine("Exit? (Y/y)");
                answer = Console.ReadLine();
            } while (answer != "Y" && answer != "y");
        }

        public async static Task Login(HttpClient client)
        {
            var user = FillUser();
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Auth/login", content);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        public static async Task Register(HttpClient client)
        {

            var user = FillUser();
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Auth/register", content);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        public static User FillUser()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            Console.WriteLine("Original bytes:");
            PrintByteArray(bytes);
            var hashBytes = mySHA256.ComputeHash(bytes);
            Console.WriteLine("Hash bytes:");
            PrintByteArray(hashBytes);
            string hashPassword = Convert.ToHexString(hashBytes);
            Console.WriteLine($"Hash password: {hashPassword}");
            return new User { Password = hashPassword, Username = username };
        }

        public static void PrintByteArray(byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]:X2}");
                if ((i % 4) == 3) Console.Write(" ");
            }
            Console.WriteLine();
        }
    }

    class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}