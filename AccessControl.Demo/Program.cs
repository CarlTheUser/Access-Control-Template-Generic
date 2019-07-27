using AccessControl.Authentication;
using AccessControl.Authentication.Security;
using AccessControl.Demo.Authentication;
using AccessControl.Demo.Infrastructure;
using AccessControl.Infrastructure;
using AccessControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Choose activity: \n");
                Console.WriteLine("1 : Create account");
                Console.WriteLine("2 : Authenticate account");
                Console.WriteLine("3 : Exit");

                char option = Console.ReadKey().KeyChar;

                UserOperation operation;

                switch(option)
                {
                    case '1': operation = new AccountCreationOperation();
                        break;
                    case '2': operation = new AuthenticateOperation();
                        break;
                    case '3': operation = new ExitOperation();
                        break;
                    default: operation = new IdleOperation();
                        break;
                }

                operation.Execute();

                Console.WriteLine();
            }
        }
        
        abstract class UserOperation
        {
            public abstract void Execute();
        }

        class ExitOperation : UserOperation
        {
            public override void Execute()
            {
                Console.WriteLine("\n-Exit requested-\n");
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            }
        }

        class AuthenticateOperation : UserOperation
        {
            public override void Execute()
            {
                Console.WriteLine("\n-Authenticate-\n");

                Console.WriteLine("Enter username: ");

                string username = Console.ReadLine();

                Console.WriteLine("Enter password: ");

                string password = GetPasswordInput();

                IAccountSource accountSource = new AccountSource();

                AccountAuthentication authentication = new DefaultAccountAuthentication(accountSource);

                AuthenticationResult result = authentication.Authenticate(username, password);

                switch(result.Status)
                {
                    case AuthenticationStatus.Ok:

                        OkResult okResult = result as OkResult;

                        Account account = okResult.Account;

                        Console.WriteLine($"Account found: {account.Username}: \nPriviledges: {string.Join(", ", account.Priviledges)}.");

                        break;

                    case AuthenticationStatus.InvalidCredentials:

                        InvalidCredentialsResult invalidCredentials = result as InvalidCredentialsResult;

                        Console.WriteLine($"Wrong password for {invalidCredentials.FormalIdentifier}.");

                        break;

                    case AuthenticationStatus.NotFound:

                        Console.WriteLine("Account not found.");

                        break;

                    default: Console.WriteLine("Unexpected code block reached (!!).");

                        break;
                }
            }
        }

        class AccountCreationOperation : UserOperation
        {
            public override void Execute()
            {
                Console.WriteLine("\n-Account Creation-\n");

                Console.WriteLine("Enter username: ");

                string username = Console.ReadLine();

                Console.WriteLine("Enter password: ");

                string password = GetPasswordInput();

                try
                {
                    ISecuredPassword passwordCreator = new EncryptionSecuredPassword();

                    Account newAccount = Account.New(
                        username,
                        passwordCreator.Create(password),
                        new[] { AccessPriviledge.Consumer }
                        );

                    INewAccountWriter accountWriter = new NewAccountWriter();

                    accountWriter.Write(newAccount);

                    Console.WriteLine("New Account Created...");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        class IdleOperation : UserOperation
        {
            public override void Execute()
            {
                Console.WriteLine("Uh-uh");
            }
        }

        static string GetPasswordInput()
        {
            //https://stackoverflow.com/questions/3404421/password-masking-console-application

            string password = string.Empty;

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write('*');
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);

                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();
            return password;
        }
    }
}
