using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Bank_Application
{
    class CreateAccount
    {
        // Account constructor
        int totalAmount = 0;
        string newAccFirstName;
        string newAccLastName;
        string newAccAddress;
        string newAccPhoneNumber;
        string newAccEmail;

        public void CreateAcc()
        {
            // New App Menu Name to help Users know where they are
            Console.Title = "Create Account";

            Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║                Create A New Account               ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║═══════════════════════════════════════════════════║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.WriteLine("\t\t║              Enter The Details Below              ║");
            Console.WriteLine("\t\t║                                                   ║");
            Console.Write("\t\t║  First Name: ");
            int firstNameCursorX = Console.CursorTop;
            int firstNameCursorY = Console.CursorLeft;
            Console.Write("                                     ║");

            Console.Write("\n\t\t║  Last Name: ");
            int lastNameCursorX = Console.CursorTop;
            int lastNameCursorY = Console.CursorLeft;
            Console.Write("                                      ║");

            Console.Write("\n\t\t║  Address: ");
            int addressCursorX = Console.CursorTop;
            int addressCursorY = Console.CursorLeft;
            Console.Write("                                        ║");

            Console.Write("\n\t\t║  Email: ");
            int emailCursorX = Console.CursorTop;
            int emailCursorY = Console.CursorLeft;
            Console.Write("                                          ║");

            Console.Write("\n\t\t║  Phone Number: ");
            int phoneCursorX = Console.CursorTop;
            int phoneCursorY = Console.CursorLeft;
            Console.Write("                                   ║");
            Console.WriteLine("\n\t\t╚═══════════════════════════════════════════════════╝");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(firstNameCursorY, firstNameCursorX);
            newAccFirstName = Console.ReadLine();

            Console.SetCursorPosition(lastNameCursorY, lastNameCursorX);
            newAccLastName = Console.ReadLine();

            Console.SetCursorPosition(addressCursorY, addressCursorX);
            newAccAddress = Console.ReadLine();

            Console.SetCursorPosition(emailCursorY, emailCursorX);
            newAccEmail = Console.ReadLine();

            Console.SetCursorPosition(phoneCursorY, phoneCursorX);
            newAccPhoneNumber = Console.ReadLine();

            IsFormCreationValid(newAccFirstName, newAccLastName, newAccAddress, newAccPhoneNumber, newAccEmail);
        }

        private void IsFormCreationValid(string firstName, string lastName, string address, string phoneNum, string email)
        {
            bool isValidNumber = true;
            bool isEmailValid = true;

            if ( String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName) || String.IsNullOrEmpty(address))
            {
                Console.WriteLine("\n\t\t    No First Name / Last Name / Address Inputed!");
            }

            if (email != null)
            {
                isEmailValid = CheckEmailAddInpt(email);
                if (!isEmailValid)
                {
                    Console.WriteLine("\n\t\t    Email must contain @gmail.com or @outlook.com");
                }
            }

            if (phoneNum != null)
            {
                isValidNumber = CheckPhoneNumInpt(newAccPhoneNumber);
                if (!isValidNumber)
                {
                    Console.WriteLine("\n\t\t    Invalid Phone Number");
                }
            }

            if (isValidNumber && isEmailValid && !String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName) && !String.IsNullOrEmpty(address))
            {
                ActionPrompt(newAccFirstName, newAccLastName, newAccAddress, newAccPhoneNumber, newAccEmail);
            } 
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\t\t    Invalid Credentials!... Create New Account (y/n)? ");
                ErrorActionPrompt();
            }
        }

        // Phone number Validation
        private bool CheckPhoneNumInpt(string phoneNumInpt)
        {
            return phoneNumInpt.ToString().Length == 10 && int.TryParse(phoneNumInpt, out _);

        }

        // Email Address Validation
        private bool CheckEmailAddInpt(string emailInpt)
        {
            var trimmedEmail = emailInpt.Trim();

            //email must contain '@' and either 'gmail.com' or 'outlook.com' or 'uts.edu.au'
            if (trimmedEmail.Contains("@") && (trimmedEmail.Contains("gmail.com") || trimmedEmail.Contains("outlook.com")))
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void CreateNewAccountDb(int amount, string firstName, string lastName, string address, string phoneNum, string email)
        {
            // Generate unique 6-8 digit number
            Random randomNum= new Random();
            int uniqueAccountNum = randomNum.Next(100000, 99999999);

            // "Backend" store users credentials in <account_number>.txt
            StreamWriter newAccountFile = new StreamWriter(uniqueAccountNum + ".txt");

            Login userName = new Login();

            // newAccountFile.WriteLine("UserName: {0}", userName.userNameInpt);
            newAccountFile.WriteLine("Account Number: {0}", uniqueAccountNum);
            newAccountFile.WriteLine("Account Balance: ${0}", amount);
            newAccountFile.WriteLine("First Name: {0}", firstName);
            newAccountFile.WriteLine("Last Name: {0}", lastName);
            newAccountFile.WriteLine("Address: {0}", address);
            newAccountFile.WriteLine("Phone Number: {0}", phoneNum);
            newAccountFile.WriteLine("Email Address: {0}", email);

            newAccountFile.Close();

            // Confirmation prompt
            Console.WriteLine("\n\n\t\t  Congratulation! Your account has been created!");
            Console.WriteLine("\n\t\t  The Account details have been sent to the email provided.");

            // Email Account credentials to users email
            EmailAccCredentials(uniqueAccountNum, totalAmount, newAccFirstName, newAccLastName, newAccAddress, newAccPhoneNumber, newAccEmail);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\t\t  Your unique Account Number is: {0}", uniqueAccountNum);
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("\n\t\t  Press any to go back to the Main Menu!: ");
            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                default:
                    Console.Clear();
                    SelectServiceScreen.GoToMainMenu();
                    break;
            }
        }

        // Email Credentials to User
        private void EmailAccCredentials(int uniqueAccountNum, int amount, string firstName, string lastName, string address, string phoneNum, string email)
        {
            string emailMessage =
                $"Account Number: {uniqueAccountNum}" +
                $"</br>Account Balance: ${amount}" +
                $"</br>First Name: {firstName}" +
                $"</br>Last Name: {lastName}" +
                $"</br>Address: {address}" +
                $"</br>Phone Number: {phoneNum}" +
                $"</br>Email Address: {email}";

            try
            {
                // Emailing Logic
                Console.WriteLine("\n\t\t  Sending Email Confirmation!...");
                SmtpClient mySmtpClient = new SmtpClient("smtp-mail.outlook.com");

                mySmtpClient.Port = 587;
                mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mySmtpClient.UseDefaultCredentials = false;
                NetworkCredential credential = new NetworkCredential("programming-NET@hotmail.com", "#gkH9f58@kk");
                mySmtpClient.EnableSsl = true;
                mySmtpClient.Credentials = credential;

                MailMessage message = new MailMessage("programming-NET@hotmail.com", email);

                message.Subject = "Created New Account";
                message.Body = "<h1>You have created a new Account!</h1>" + emailMessage + "<p>Kind regards, </br>Simple Banking System</p>";
                message.IsBodyHtml = true;
                mySmtpClient.Send(message);

            }
            catch (SmtpException ex)
            {
                Console.WriteLine("\n\t\t  Error... Email Could not be sent!...");
                ActionPrompt(firstName, lastName, address, phoneNum, email);

                throw new ApplicationException ("SmtpException has occured: " + ex.Message);
            }
        }

        private void ActionPrompt(string firstName, string lastName, string address, string phoneNum, string email)
        {
            // User choice
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\t\t  Press 'y' to create new account");
            Console.WriteLine("\t\t  Press 'n' to re-fill the form");
            Console.WriteLine("\t\t  Press 'e' to exit without saving");
            Console.Write("\n\t\t  Is this Information Correct? ");
            Console.ForegroundColor = ConsoleColor.Cyan;

            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case 'y':
                    CreateNewAccountDb(totalAmount, newAccFirstName, newAccLastName, newAccAddress, newAccPhoneNumber, newAccEmail);
                    break;

                case 'n':
                    Console.Clear();
                    CreateAcc();
                    break;
                case 'e':
                    Console.Clear();
                    SelectServiceScreen.GoToMainMenu();
                    break;
                default:
                    Console.Clear();

                    Console.WriteLine("\t\t╔═══════════════════════════════════════════════════╗");
                    Console.WriteLine("\t\t║                                                   ║");
                    Console.WriteLine("\t\t║                Create A New Account               ║");
                    Console.WriteLine("\t\t║                                                   ║");
                    Console.WriteLine("\t\t║═══════════════════════════════════════════════════║");
                    Console.WriteLine("\t\t║                                                   ║");
                    Console.WriteLine("\t\t║              Enter The Details Below              ║");
                    Console.WriteLine("\t\t║                                                   ║");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t\t   First Name: {0}                                ", firstName);
                    Console.WriteLine("\t\t   Last Name:  {0}                               ", lastName);
                    Console.WriteLine("\t\t   Address: {0}                         ", address);
                    Console.WriteLine("\t\t   Phone Number: {0}                         ", phoneNum);
                    Console.WriteLine("\t\t   Email: {0}                      ", email);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\t\t║                                                   ║");
                    Console.WriteLine("\t\t╚═══════════════════════════════════════════════════╝\n");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t Invalid input! please enter y/n/e");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    ActionPrompt(newAccFirstName, newAccLastName, newAccAddress, newAccPhoneNumber, newAccEmail);
                    break;
            }
        }

        private void ErrorActionPrompt()
        {
            ConsoleKeyInfo userChoice = Console.ReadKey();

            switch (userChoice.KeyChar)
            {
                case 'y':
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    CreateAcc();
                    break;

                case 'n':
                    Console.Clear();
                    SelectServiceScreen.GoToMainMenu();
                    break;
                default:
                    int currentLineCursor = Console.CursorTop;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t\t    Invalid input! please enter y/n");
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.Write("\n\t\t    Invalid Credentials!... Create New Account (y/n)? ");

                    ErrorActionPrompt();
                    break;
            }
        }
    }
}
