using Grpc.Net.Client;
using Server;
using System;
using System.Threading.Tasks;
namespace Client
{
    class ClientInterface
    {
        static async Task Main(string[] args)
        {
            string nume, cnp;
            Console.WriteLine("#Enter your full name: ");
            nume = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("#Enter your CNP: ");
            cnp = Console.ReadLine();
            while(!isCNPOk(cnp))
            {
                Console.WriteLine();
                Console.WriteLine("#The CNP you entered is invalid, please try again: ");
                cnp = Console.ReadLine();
            }
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new User.UserClient(channel);
            var clientRequest = new UserInput { Nume = nume, Cnp = cnp };
            var reply = await client.ParseUserInfoAsync(clientRequest);
            Console.WriteLine();
            Console.WriteLine("#Reply from Server");
            Console.WriteLine($" *User's name:{reply.Nume}\n *User's gender:{reply.Gen}\n *User's age:{reply.Varsta}");
            Console.WriteLine();
        }
        static bool isCNPOk(string cnp)
        {
            if (cnp.Length != 13)
                return false;
            return true;
        }
    }
}
