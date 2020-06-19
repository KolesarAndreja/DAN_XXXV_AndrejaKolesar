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
        static Thread t1 = new Thread(UserInput);
        static Task<Thread[]> t2 = new Task<Thread[]>(ThreadGenerator);

        public static void UserInput()
        {
            Console.WriteLine("Unesite broj učesnika: ");
            numOfParticipant = ValidNumber(int.MaxValue);
            Console.WriteLine("Unesite broj između 1 i 100 koji će ostali učesnici pogađati: ");
            number = ValidNumber(100);
            t2.Start();
            Console.WriteLine("Broj učesnika: {0} \nOdabrani broj: {1}", numOfParticipant, number);
        }

        public static Thread[] ThreadGenerator()
        {
            Thread[] participants = new Thread[numOfParticipant];
            for(int i = 0; i < numOfParticipant; i++)
            {
                participants[i] = new Thread(GuessNumber)
                {
                    Name = string.Format("Učesnik_{0}", i + 1)
                };
            }

            return participants;
        }

        public static void GuessNumber()
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
            t1.Start();
            t1.Join();
            t2.Wait();
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
