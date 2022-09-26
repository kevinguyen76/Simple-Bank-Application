using System;
using System.IO;

namespace Bank_Application
{
    class SearchAccount
    {
        public void SearchAcc()
        {
            // New App Menu Name to help Users know where they are
            Console.Title = "Search Account";

            Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║                  Search An Account                ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║═══════════════════════════════════════════════════║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║                 Enter Account Number              ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.Write("\t\t║  Account Number: ");
            int accountNumInptCursorX = Console.CursorTop;
            int accountNumInptCursorY = Console.CursorLeft;

            Console.Write("                                 ║");

            Console.WriteLine("\n\t\t║                                                   ║");
            Console.WriteLine("\t\t╚═══════════════════════════════════════════════════╝");

            Console.SetCursorPosition(accountNumInptCursorY, accountNumInptCursorX);

            Console.ForegroundColor = ConsoleColor.Yellow;
            string accountNumInpt = Convert.ToString(Console.ReadLine()).Trim();
            Console.ForegroundColor = ConsoleColor.Cyan;

            DoesAccountExist(accountNumInpt);
            Console.ForegroundColor = ConsoleColor.Cyan;

            ActionPrompt();
        }

        // Check if Account Exist
        private void DoesAccountExist(string accountNum)
        {
            if (File.Exists($"{accountNum}.txt"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\n\n\t\t Account Found!\n");
                Console.ForegroundColor = ConsoleColor.Cyan;

                // Print each line associated with the account file
                string[] lines = File.ReadAllLines($"{accountNum}.txt");

                Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
                Console.WriteLine("\t\t║                                                   ║");
                Console.WriteLine("\t\t║                  Account Details                  ║");
                Console.WriteLine("\t\t║                                                   ║");
                Console.WriteLine("\t\t║═══════════════════════════════════════════════════║");
                Console.WriteLine("\t\t║                                                   ║");
                foreach (string set in lines)
                {
                    if (set.Contains("Transaction"))
                    {
                        continue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\t\t   {set}                              ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                }
                Console.WriteLine("\t\t║                                                   ║");
                Console.WriteLine("\t\t╚═══════════════════════════════════════════════════╝\n\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\t\t   Account does not exist!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                ActionPrompt();

                SearchAcc();
            }
        }

        // User prompt to search another account or go back to main menu
        private void ActionPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t   Check another account (y/n)? ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case 'y':
                    Console.Clear();
                    SearchAcc();
                    break;

                case 'n':
                    Console.Clear();
                    SelectServiceScreen.GoToMainMenu();
                    break;
                default:
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t   Invalid input! please enter y/n\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    ActionPrompt();
                    break;
            }
        }
    }
}
