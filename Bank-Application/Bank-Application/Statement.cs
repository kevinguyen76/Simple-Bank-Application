using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Bank_Application
{
    class Statement
    {
        private string emailAddress;

        public void AccStatement()
        {
            Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║                  Account Statement                ║");
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
            Console.WriteLine("\t\t╚═══════════════════════════════════════════════════╝\n");

            Console.SetCursorPosition(accountNumInptCursorY, accountNumInptCursorX);

            Console.ForegroundColor = ConsoleColor.Yellow;
            string accountNumInpt = Convert.ToString(Console.ReadLine()).Trim();
            Console.ForegroundColor = ConsoleColor.Cyan;

            DoesAccountExist(accountNumInpt);
            Console.ForegroundColor = ConsoleColor.Cyan;

            EmailStatement(accountNumInpt);
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
                Console.WriteLine("\t\t║═══════════════════════════════════════════════════║");
                Console.WriteLine("\t\t║                                                   ║");
                Console.WriteLine("\t\t║                 Last 5 Transactions               ║");
                Console.WriteLine("\t\t║                                                   ║");
                for (int i = lines.Length - 1; i >= lines.Length - 5; i--)
                {
                    if (lines[i].Contains("Transaction"))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\t\t   {lines[i]}                              ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    } 
                    else
                    {
                        continue;
                    }
                }
                Console.WriteLine("\t\t║                                                   ║");
                Console.WriteLine("\t\t╚═══════════════════════════════════════════════════╝");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\t\t Account does not exist!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                ActionPrompt();
            }
        }

        // Email user bank statment
        private void EmailStatement(string accountNum)
        {
            Console.Write("\n\t\t Email Account Statement(y/n)? ");
            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case 'y':
                    string[] lines = File.ReadAllLines($"{accountNum}.txt");
                    string[] lastFiveTransaction = new string[5];

                    foreach ( string set in lines)
                    {
                        string[] splits = set.Split(':');
                        if (splits[0].Contains("Email Address"))
                        {
                            emailAddress = splits[1].Trim();
                        }
                    }

                    // Get last 5 Transactions 
                    for (int i = 0; i < 5; i++)
                    {
                        int transaction = lines.Length;
                        for (int j = transaction - i; j-- >0;)
                        {
                            if (lines[j].Contains("Transaction"))
                            {
                                lastFiveTransaction[i] = lines[j];
                                break;
                            } 
                        }
                    }

                    hasTransactions(lastFiveTransaction, emailAddress);
                    ActionPrompt();

                    break;
                case 'n':
                    Console.Clear();
                    SelectServiceScreen.GoToMainMenu();

                    break;
                default:
                    int currentLineCursor = Console.CursorTop;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, currentLineCursor);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t Invalid input! please enter y/n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    EmailStatement(accountNum);
                    break;
            }
        }

        private void hasTransactions(string[] transactions, string email)
        {
            if(transactions[0] != null)
            {
                string emailMessage = "";
                foreach (string transaction in transactions)
                {
                    emailMessage += $"</br>{transaction}";
                }

                // Emailing Logic
                try
                {
                    Console.WriteLine("\n\t\t Sending email!...");
                    SmtpClient mySmtpClient = new SmtpClient("smtp-mail.outlook.com");

                    mySmtpClient.Port = 587;
                    mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    mySmtpClient.UseDefaultCredentials = false;
                    NetworkCredential credential = new NetworkCredential("programming-NET@hotmail.com", "#gkH9f58@kk");
                    mySmtpClient.EnableSsl = true;
                    mySmtpClient.Credentials = credential;

                    MailMessage message = new MailMessage("programming-NET@hotmail.com", email);

                    message.Subject = "Bank Statment";
                    message.Body = "<h1>Your Last 5 Transactions are down below!</h1>" + emailMessage + "<p>Kind regards, </br>Simple Banking System</p>";
                    message.IsBodyHtml = true;

                    mySmtpClient.Send(message);
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("\n\t\t Error... Email Could not be sent!...");

                    ActionPrompt();
                    throw new ApplicationException ("SmtpException has occured: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                ActionPrompt();
            } 
            else
            {
                // If there are no transactions associated with account print out error msg
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\t\t There are no transactions associated with this account!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                ActionPrompt();
            }
        }

        // User prompt to search another account or go back to main menu
        private void ActionPrompt()
        {
            Console.Write("\n\t\t Check another Account Statement(y/n)? ");
            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case 'y':
                    Console.Clear();
                    AccStatement();
                    break;

                case 'n':
                    Console.Clear();
                    SelectServiceScreen.GoToMainMenu();
                    break;
                default:
                    int currentLineCursor = Console.CursorTop;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, currentLineCursor);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t Invalid input! please enter y/n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    ActionPrompt();
                    break;
            }
        }
    }
}
