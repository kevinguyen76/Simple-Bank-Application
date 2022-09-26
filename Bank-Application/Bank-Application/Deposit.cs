using System;
using System.IO;

namespace Bank_Application

{
    class Deposit
    {
        long depositAmount;
        long totalAccAmount;

        public void DepositAmount()
        {
            // New App Menu Name to help Users know where they are
            Console.Title = "Deposit into an Account";

            Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║                 Deposit Into Account              ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║═══════════════════════════════════════════════════║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║                 Enter Account Number              ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.Write("\t\t║  Account Number: ");
            int accountNumInptCursorX = Console.CursorTop;
            int accountNumInptCursorY = Console.CursorLeft;
            Console.Write("                                 ║");

            Console.Write("\n\t\t║  Amount: $ ");
            int depositAmountCursorX = Console.CursorTop;
            int depositAmountCursorY = Console.CursorLeft;
            Console.Write("                                       ║");

            Console.WriteLine("\n\t\t║                                                   ║");
            Console.WriteLine("\t\t╚═══════════════════════════════════════════════════╝");
            
            Console.SetCursorPosition(accountNumInptCursorY, accountNumInptCursorX);
            Console.ForegroundColor = ConsoleColor.Yellow;
            string accountNumInpt = Convert.ToString(Console.ReadLine()).Trim();

            bool accountExist = DoesAccountExist(accountNumInpt);

            if (accountExist)
            {
                Console.WriteLine("\n\n\n\n\t\t   Account Found!");
                UpdateAccountAmount(accountNumInpt, depositAmountCursorY, depositAmountCursorX);
            } 
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\n\t\t   Account does not exist!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                ActionPrompt();
            }

        }

        private bool DoesAccountExist(string accountNum)
        {
            return (File.Exists($"{accountNum}.txt"));
        }

        private void UpdateAccountAmount(string accountNum, int depositAmountCursorY, int depositAmountCursorX)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(depositAmountCursorY, depositAmountCursorX);

            // Check is deposit amount is in the correct format
            try
            {
                depositAmount = Convert.ToInt64(Console.ReadLine());
            } 
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\n\n\t\t   Invalid Amount provided!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                ActionPrompt();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;

            // check deposit amount
            bool isDepositValid = CheckIsDepositValid(depositAmount);

            if (isDepositValid)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\n\n\n\n\t\t   Successfully Deposited ${0}", depositAmount);

                string[] lines = File.ReadAllLines($"{accountNum}.txt");

                StreamWriter file = new StreamWriter(accountNum + ".txt");
                foreach (string set in lines)
                {
                    string[] splits = set.Split(':');
                    if (splits[0].Contains("Account Balance"))
                    {
                        long currentAmount = Convert.ToInt64(splits[1].Trim().TrimStart('$'));
                        totalAccAmount = currentAmount + depositAmount;

                        // change amount in txt file
                        for (int i = 0; i < lines.Length; i++)
                        {
                            //if there is amount then change the total
                            if (lines[i].Contains("Account Balance"))
                            {
                                file.WriteLine(splits[1].Replace(splits[1], $"Account Balance: $" + Convert.ToString(totalAccAmount)));
                            }
                            else
                            {
                                file.WriteLine(lines[i]);
                            }
                        }

                        // Add Transaction statement:
                        file.WriteLine($"Transaction Number {lines.Length - 6} {DateTime.Now.ToString("MM/dd/yyyy")}: Deposited ${depositAmount}");
                        file.Close();

                        Console.Write("\n\t\t   Account Balance is: ${0}\n", totalAccAmount);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        ActionPrompt();
                    }
                }
            } 
            else if (!isDepositValid)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\n\n\t\t   Invalid Amount provided!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                ActionPrompt();
            }

        }

        private bool CheckIsDepositValid(long amount)
        {
            string depositAmount = Convert.ToString(amount);

            return (depositAmount.Length <= 10 && !depositAmount.Equals("0"));
        }

        private void ActionPrompt()
        {
            Console.Write("\n\t\t   Deposit into another account (y/n)? ");
            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case 'y':
                    Console.Clear();
                    DepositAmount();
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
                    Console.WriteLine("\n\t\t   Invalid input! please enter y/n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    ActionPrompt();
                    break;
            }
        }
    }
}
