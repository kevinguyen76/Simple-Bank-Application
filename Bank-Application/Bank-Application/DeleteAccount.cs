using System;
using System.IO;

namespace Bank_Application
{
    class DeleteAccount
    {
        private string accountNumInpt;

        public void DeleteAcc()
        {
            // New App Menu Name to help Users know where they are
            Console.Title = "Delete Account";

            Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║                  Delete An Account                ║");
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
            accountNumInpt = Convert.ToString(Console.ReadLine()).Trim();
            Console.ForegroundColor = ConsoleColor.Cyan;

            DoesAccountExist(accountNumInpt, accountNumInptCursorY, accountNumInptCursorX);
            Console.ForegroundColor = ConsoleColor.Cyan;

            ActionPrompt(accountNumInpt);
        }

        private void DoesAccountExist(string accountNum, int accountNumInptCursorY, int accountNumInptCursorX)
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
                Console.WriteLine("\t\t║                   Account Details                 ║");
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
                        Console.WriteLine($"\t\t  {set}                              ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                }
                Console.WriteLine("\t\t║                                                   ║");
                Console.WriteLine("\t\t╚═══════════════════════════════════════════════════╝\n");

                ActionPrompt(accountNum);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\t         Account does not exist!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                ErrorActionPrompt();
            }
        }

        private void DeleteSelectedAcc(string accountNum)
        {
            Console.WriteLine("\n\t\t Account {0} Deleted!...", accountNum);
            File.Delete($"{accountNum}.txt");

            Console.Write("\n\t\t Press any to go back to the Main Menu!: ");
            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                default:
                    Console.Clear();
                    SelectServiceScreen.GoToMainMenu();
                    break;
            }
        }

        // User prompt to search another account or go back to main menu
        private void ActionPrompt(string accountNum)
        {
            Console.Write("\n\t\t Delete Account (y/n)? ");
            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case 'y':
                    DeleteSelectedAcc(accountNumInpt);
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
                    Console.WriteLine("\t\t Invalid input! please enter y/n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    ActionPrompt(accountNum);
                    break;
            }
        }

        private void ErrorActionPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n\t\t Check another account (y/n)? ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case 'y':
                    Console.Clear();
                    DeleteAcc();
                    break;

                case 'n':
                    Console.Clear();
                    SelectServiceScreen.GoToMainMenu();
                    break;
                default:
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 3);
                    Console.Write(new string(' ', Console.WindowWidth));


                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\t\t Invalid input! please enter y/n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    ErrorActionPrompt();
                    break;
            }
        }
    }
}
