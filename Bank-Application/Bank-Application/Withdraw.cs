using System;
using System.IO;

namespace Bank_Application
{
    class Withdraw
    {
        long withdrawalAmount;
        long totalAccAmount;

        public void WithdrawAmount()
        {
            // New App Menu Name to help Users know where they are
            Console.Title = "Withdraw";

            Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║                WithDraw From Account              ║");
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
            int withdrawalAmountCursorX = Console.CursorTop;
            int withdrawalAmountCursorY = Console.CursorLeft;
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
                UpdateAccountAmount(accountNumInpt, withdrawalAmountCursorY, withdrawalAmountCursorX);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\n\t\t   Account does not exist!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                ActionPrompt();
            }
        }

        private void UpdateAccountAmount(string accountNum, int withdrawalAmountCursorY, int withdrawalAmountCursorX)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(withdrawalAmountCursorY, withdrawalAmountCursorX);

            // Check is withdrawal amount is in the correct format
            try
            {
                withdrawalAmount = Convert.ToInt64(Console.ReadLine());
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
            bool isWithdrawalValid = CheckIsWithdrawalValid(withdrawalAmount);

            if (isWithdrawalValid)
            {
                string[] lines = File.ReadAllLines($"{accountNum}.txt");
                foreach (string set in lines)
                {
                    string[] splits = set.Split(':');
                    if (splits[0].Contains("Account Balance"))
                    {
                        long currentAmount = Convert.ToInt64(splits[1].Trim().TrimStart('$'));
                        totalAccAmount = currentAmount - withdrawalAmount;

                        // change amount in txt file
                        if (totalAccAmount >= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n\n\n\n\n\t\t   Successfully Withdrew ${0}", withdrawalAmount);
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            StreamWriter file = new StreamWriter(accountNum + ".txt");
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
                            file.WriteLine($"Transaction Number {lines.Length - 6} {DateTime.Now.ToString("MM/dd/yyyy")}: Withdrew ${withdrawalAmount}");
                            file.Close();

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("\n\t\t   Account Balance is: ${0}\n", totalAccAmount);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            ActionPrompt();

                        } else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\n\n\n\n\n\t\t   Insufficient Funds!...");
                            Console.ForegroundColor = ConsoleColor.Cyan;

                            ActionPrompt();
                        }
                    }
                }
            }
            else if (!isWithdrawalValid)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\n\n\t\t   Invalid Amount provided!");
                Console.ForegroundColor = ConsoleColor.Cyan;

                ActionPrompt();
            }
        }

        private bool CheckIsWithdrawalValid(long amount)
        {
            string withdrawalAmount = Convert.ToString(amount);

            return (withdrawalAmount.Length <= 10 && !withdrawalAmount.Equals("0"));
        }

        private bool DoesAccountExist(string accountNum)
        {
            return (File.Exists($"{accountNum}.txt"));
        }

        private void ActionPrompt()
        {
            Console.Write("\n\t\t   Withdraw from another account (y/n)? ");
            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case 'y':
                    Console.Clear();
                    WithdrawAmount();
                    break;

                case 'n':
                    Console.Clear();
                    SelectServiceScreen.GoToMainMenu();
                    break;
                default:
                    int currentLineCursor = Console.CursorTop;
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
