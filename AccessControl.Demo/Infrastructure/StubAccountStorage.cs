using AccessControl.Authentication.Security;
using AccessControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Demo.Infrastructure
{
    internal sealed class StubAccountStorage
    {
        private static readonly List<Account> _Accounts;

        public static IEnumerable<Account> Accounts => _Accounts;

        static StubAccountStorage()
        {
            ISecuredPassword securedPasswordCreator = new EncryptionSecuredPassword();

            _Accounts = new List<Account>()
            {
                Account.Existing(
                    new Identity(1),
                    "user1",
                    securedPasswordCreator.Create("password"),
                    new [] { AccessPriviledge.Consumer }),
                Account.Existing(
                    new Identity(2),
                    "Admin",
                    securedPasswordCreator.Create("Administrator123"),
                    new [] { AccessPriviledge.Administrator }),
                Account.Existing(
                    new Identity(3),
                    "SuperUser",
                    securedPasswordCreator.Create("superPassword"),
                    new [] { AccessPriviledge.Consumer, AccessPriviledge.Administrator }),
                Account.Existing(
                    new Identity(4),
                    "consumer",
                    securedPasswordCreator.Create("password"),
                    new [] { AccessPriviledge.Consumer }),
            };
            
        }
        

        public static void SaveRecord(Account account)
        {
            if (account.Identity == null)
            {
                Account existing = Accounts.FirstOrDefault(a => a.Username == account.Username);

                if (existing == null)
                {
                    Identity identity = CreateIdentity();
                    account.InitializeIdentity(identity);
                    _Accounts.Add(account);
                }
                else throw new Exception("Account with username already exists (!!)");
            }
            else throw new InvalidOperationException("That aint allowed here.");
        }

        public static void RemoveRecord(Account account)
        {
            _Accounts.Remove(account);
        }

        private static Identity CreateIdentity()
        {
            return new Identity(
                (from account in _Accounts
                 where account.Identity != null
                 select account.Identity.Value).Max() + 1);
        }
    }
}
