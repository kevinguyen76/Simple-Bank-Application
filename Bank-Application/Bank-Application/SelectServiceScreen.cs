using System;

namespace Bank_Application
{
    class SelectServiceScreen
    {
        public void MainMenu()
        {
            // Console Title and UI
            Console.Title = "Main Menu";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║          Welcome To Simple Banking System         ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║═══════════════════════════════════════════════════║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║   1. Create a new account                         ║");
            Console.WriteLine("\t\t║   2. Search for an account                        ║");
            Console.WriteLine("\t\t║   3. Deposit                                      ║");
            Console.WriteLine("\t\t║   4. Withdraw                                     ║");
            Console.WriteLine("\t\t║   5. A/C Statement                                ║");
            Console.WriteLine("\t\t║   6. Delete Account                               ║");
            Console.WriteLine("\t\t║   7. Exit                                         ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║═══════════════════════════════════════════════════║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.Write("\t\t║  Select a service through 1-7: ");
            int mainMenuCursorX = Console.CursorTop;
            int mainMenuCursorY = Console.CursorLeft;

            Console.Write("                   ║");
            int userPasswordCursorX = Console.CursorTop;
            int userPasswordCursorY = Console.CursorLeft;

            Console.WriteLine("\n\t\t║                                                   ║");
            Console.WriteLine("\t\t╚═══════════════════════════════════════════════════╝");

            Console.SetCursorPosition(mainMenuCursorY, mainMenuCursorX);

            //int menuSelection = Convert.ToInt32(new string(Console.ReadKey().KeyChar, 1));
            ConsoleKeyInfo menuSelection = Console.ReadKey();
            switch (menuSelection.KeyChar)
            {
                case '1':
                    Console.Clear();
                    CreateAccount newAccount = new CreateAccount();
                    newAccount.CreateAcc();

                    break;
                case '2':
                    Console.Clear();
                    SearchAccount searchAccount = new SearchAccount();
                    searchAccount.SearchAcc();

                    break;
                case '3':
                    Console.Clear();
                    Deposit deposit = new Deposit();
                    deposit.DepositAmount();

                    break;
                case '4':
                    Console.Clear();
                    Withdraw withdraw = new Withdraw();
                    withdraw.WithdrawAmount();

                    break;
                case '5':
                    Console.Title = "Account Statement";
                    Console.Clear();
                    Statement accStatement = new Statement();
                    accStatement.AccStatement();

                    break;
                case '6':
                    Console.Clear();
                    DeleteAccount deleteAcc = new DeleteAccount();
                    deleteAcc.DeleteAcc();

                    break;
                case '7':
                    // Should just close window 
                    Console.Clear();
                    Console.WriteLine("Thankyou for using The Simple Banking System, Bye!");
                    Environment.Exit(0);

                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t   Invalid input! please enter 1-7\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    MainMenu();
                    break;

            }
        }

        // Go back to main menu
        public static void GoToMainMenu()
        {
            SelectServiceScreen home = new SelectServiceScreen();
            home.MainMenu();
        }
    }
}
