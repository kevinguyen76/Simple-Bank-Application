using System;
using System.IO;

namespace Bank_Application
{
    class Login
    {
        private string userNameInput;
        private string userPasswordInput;

        public void LoginScreen()
        {
            Console.Clear();

            // Console title and UI
            Console.Title = "Simple Banking System Login";

            Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║          Welcome To Simple Banking System         ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║═══════════════════════════════════════════════════║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║                  Login to Proceed!                ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.Write("\t\t║  UserName: ");
            int userNameCursorX = Console.CursorTop;
            int userNameCursorY = Console.CursorLeft;
            Console.Write("                                       ║");

            Console.Write("\n\t\t║  Password: ");
            int userPasswordCursorX = Console.CursorTop;
            int userPasswordCursorY = Console.CursorLeft;
            Console.Write("                                       ║");
            Console.WriteLine("\n\t\t║                                                   ║");
            Console.WriteLine("\t\t╚═══════════════════════════════════════════════════╝");

            Console.SetCursorPosition(userNameCursorY, userNameCursorX);
            Console.ForegroundColor = ConsoleColor.Yellow;
            userNameInput = Console.ReadLine().Trim();

            Console.SetCursorPosition(userPasswordCursorY, userPasswordCursorX);

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    userPasswordInput += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (keyInfo.Key == ConsoleKey.Backspace && userPasswordInput.Length > 0)
                    {
                        userPasswordInput = userPasswordInput.Substring(0, userPasswordInput.Length - 1);
                        Console.Write("\b \b");
                    } else if ( keyInfo.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            Console.ForegroundColor = ConsoleColor.Cyan;

            IsLoginValid(userNameInput, userPasswordInput);
        }

        private void IsLoginValid(string userName, string password)
        {
            try
            {
                // Accress login.txt and read each line in the file
                string[] lines = File.ReadAllLines("login.txt");
                foreach (string set in lines)
                {
                    //if password and username input match those in file return true
                    string[] splits = set.Split('|');
                    if (splits[0] == userName && splits[1] == password)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\n\n\n\t\t\tValid Credentials!... Please Enter");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.ReadKey();

                        Console.Clear();
                        SelectServiceScreen serviceScreen = new SelectServiceScreen();
                        serviceScreen.MainMenu();
                        return;
                    }
                }

                // If user login credentials are not found then refresh Login screen
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n\n\n\t\t\tIncorrect credentials... \n\t\t\tPlease press any key to try again!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                // Reset input values
                userNameInput = null;
                userPasswordInput = null;

                Console.ReadKey();
                LoginScreen();

            }
            catch (FileNotFoundException)
            {
                Console.Clear();
                Console.WriteLine("file not found");
                Console.ReadKey();
            }
        }
    }
}
