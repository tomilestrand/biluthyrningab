using System;
using System.Collections.Generic;
using System.Text;
using BiluthyrningABdel1;

namespace BiluthyrningABUI
{
    public static class ConsoleUI
    {
        public static void StartScreen()
        {
            char input = GetUserKeyInput("Tryck 1 för att hyra bil, tryck 2 för att lämna tillbaka en hyrd bil", new List<ConsoleKey> { ConsoleKey.D1, ConsoleKey.D2 });
            switch (input)
            {
                case '1':
                    RentCar();
                    break;
                case '2':
                    ReturnCar();
                    break;
                default:
                    break;
            }
        }

        private static char GetUserKeyInput(string query, List<ConsoleKey> validInputs)
        {
            ConsoleKeyInfo input;
            Console.WriteLine(query);
            do
            {
                input = Console.ReadKey(true);
            } while (!validInputs.Contains(input.Key));
            return input.KeyChar;
        }

        private static void ReturnCar()
        {
            int bookingId;
            int newMilage;
            string msg = "";
            do
            {
                if (msg != "")
                    Console.WriteLine(msg);
                bookingId = GetUserNumberInput("Skriv in ditt bokningsnummer: ");
                newMilage = GetUserNumberInput("Skriv in bilens nuvarande km-antal: ");
            } while (!Biluthyrning.ReturnCar(bookingId, newMilage, out msg));
            Console.WriteLine(msg);
        }

        private static int GetUserNumberInput(string query)
        {
            string input;
            int output;
            do
            {
                Console.Write(query);
                input = Console.ReadLine();
            } while (!int.TryParse(input, out output));
            return output;
        }

        private static void RentCar()
        {
            char carType = GetUserKeyInput("Vilken typ av bil vill du hyra?\nTryck 1 för liten bil, tryck 2 för van, tryck 3 för minibuss", new List<ConsoleKey> { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 });
            string SSN = GetUserSSN("Skriv in ditt personnummer (YYYYMMDD-XXXX): ");
            Biluthyrning.RentCar(int.Parse(carType.ToString()), SSN);
        }

        private static string GetUserSSN(string query)
        {
            string input;
            do
            {
                Console.Write(query);
                input = Console.ReadLine();
            } while (!Biluthyrning.ValidSSN(input));
            return input;
        }

        private static string GetUserLineInput(string query)
        {
            string input;
            do
            {
                Console.Write(query);
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }
    }
}
