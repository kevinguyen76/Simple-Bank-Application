using System;

namespace Bank_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            // New User Login
            Login user = new Login();
            user.LoginScreen();
        }
    }
}
