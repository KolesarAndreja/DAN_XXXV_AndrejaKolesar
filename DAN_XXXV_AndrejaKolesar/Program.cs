using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XXXV_AndrejaKolesar
{
    class Program
    {
        static int numOfParticipant;
        static int number;

        public static void UserInput()
        {
            Console.WriteLine("Unesite broj učesnika: ");
            numOfParticipant = ValidNumber(int.MaxValue);

            Console.WriteLine("Unesite broj koji će ostali učesnici pogađati: ");
            number = ValidNumber(100);

        }

        public static void ThreadGenerator()
        {

        }

        /// <summary>
        /// Return valid positive number in range (0,limit] number
        /// </summary>
        public static int ValidNumber(int limit)
        {
            string s = Console.ReadLine();
            int Num;
            bool b = Int32.TryParse(s, out Num);
            while (!b || Num < 0 || Num > limit)
            {
                Console.Write("Pogrešan unos. Ponovite: ");
                s = Console.ReadLine();
                b = Int32.TryParse(s, out Num);
            }
            return Num;
        }

        public static void NumberGuessingSimulator()
        {
            Thread t1 = new Thread(UserInput);
            Thread t2 = new Thread(ThreadGenerator)
            {
                Name = "Thread_Generator"
            };

        }

        static void Main(string[] args)
        {
            string s = "";
            Console.Write("1.Simulator pogađanja broja \n2.Izlaz iz aplikacije \nVaš izbor: ");
            do
            {
                s = Console.ReadLine();
                switch (s)
                {
                    case "1":
                        NumberGuessingSimulator();
                        Console.Write("1.Simulator pogađanja broja \n2.Izlaz iz aplikacije \nVaš izbor: "); ;
                        break;
                    case "2":
                        break;
                    default:
                        Console.Write("Pogrešan unos. Ponovite: ");
                        break;
                }
            } while (s != "2");
        }
    }
}
