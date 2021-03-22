using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class UsersService : User.UserBase
    {
        string genM;
        string genF;
        public UsersService()
        {
            genM = "Masculin";
            genF = "Feminin";
        }
        public override Task<Reply> ParseUserInfo(UserInput request, ServerCallContext context)
        {
            Reply reply = new Reply();
            reply.Nume = request.Nume;
            DateTime today = DateTime.Today;
            int an, luna, zi;
            int zeroInASCII = 48;
            switch(request.Cnp[0])
            {
                case '1':
                    {
                        reply.Gen = genM;
                        an = 1900 + (10 * request.Cnp[1] + request.Cnp[2] - 11 * zeroInASCII);
                        break;
                    }
                case '2':
                    {

                        reply.Gen = genF;
                        an = 1900 + (10 * request.Cnp[1] + request.Cnp[2] - 11 * zeroInASCII);
                        break;
                    }
               
                case '5':
                    {

                        reply.Gen = genM;
                        an = 2000 + (10 * request.Cnp[1] + request.Cnp[2] - 11 * zeroInASCII);
                        break;
                    }
                case '6':
                    {

                        reply.Gen = genF;
                        an = 2000 + (10 * request.Cnp[1] + request.Cnp[2] - 11 * zeroInASCII);
                        break;
                    }
                default:
                    {
                        if (((int)request.Cnp[0]) % 2 == 0)
                        {
                            reply.Gen = genM;
                        }
                        else reply.Gen = genF;

                        an = 1800 + (10 * request.Cnp[1] + request.Cnp[2] - 11 * zeroInASCII);
                        break;
                    }
                
            }
            luna = request.Cnp[3] * 10 + request.Cnp[4] - zeroInASCII * 11;
            zi = request.Cnp[5] * 10 + request.Cnp[6] - zeroInASCII * 11;
            if (today.Month >= luna)
            {
                if (today.Day >= zi)
                {
                    reply.Varsta = today.Year - an;
                }
                else reply.Varsta = today.Year - an - 1;
            }
            else reply.Varsta = today.Year - an - 1;

            Console.WriteLine();
            Console.WriteLine("#From Client Sesion:");
            Console.WriteLine($" *User's name:{reply.Nume}\n *User's gender:{reply.Gen}\n *User's age:{reply.Varsta}");
            Console.WriteLine();
            return Task.FromResult(reply);

        }
    
    
    
    }
}
