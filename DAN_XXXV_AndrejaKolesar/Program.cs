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
        //locker
        static object obj = new object();
        static object obj2 = new object();
        static int numOfParticipant;
        static int number;
        static Thread t1 = new Thread(UserInput);
        static Task<Thread[]> t2 = new Task<Thread[]>(ThreadGenerator);
        static bool IsGuessed = false;

        public static void UserInput()
        {
            Console.Write("Unesite broj učesnika: ");
            numOfParticipant = ValidNumber(int.MaxValue);
            Console.Write("Unesite broj između 1 i 100 koji će ostali učesnici pogađati: ");
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
            Random random = new Random();
            int n=0;
            do
            {
                int userNumber = random.Next(0, 101);
                lock (obj)
                {

                    if (!IsGuessed)
                    {
                        if (userNumber == number)
                        {
                            Console.WriteLine("{0} je pobedio, a traženi broj je bio {1}. ", Thread.CurrentThread.Name, number);
                            IsGuessed = true;
                        }
                        else
                        {
                            Console.Write("{0} je pokusao da pogodi skriveni broj, njegov izbor je bio {1}. ", Thread.CurrentThread.Name, userNumber);
                            if (userNumber % 2 == number % 2)
                            {
                                Console.Write("{0} je pogodio parnost broja!", Thread.CurrentThread.Name);
                            }
                            Console.WriteLine();
                        }
                    }
                }
            } while (n != number && !IsGuessed);

        }

        /// <summary>
        /// Return valid positive number in range [1,limit] number
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
            Thread[] generatedThreads = t2.Result;
            foreach(var t in generatedThreads)
            {
                t.Start();
            }

            foreach(var t in generatedThreads)
            {
                t.Join();
            }
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
