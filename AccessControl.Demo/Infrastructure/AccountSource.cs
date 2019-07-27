using AccessControl.Infrastructure;
using AccessControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Demo.Infrastructure
{
    public class AccountSource : IAccountSource
    {
        public Account FromTechnicalIdentifier(Identity identity)
        {
            return StubAccountStorage.Accounts.FirstOrDefault(account => account.Identity.Value == identity.Value);
        }

        public Account FromUserIdentifier(string identifier)
        {
            return StubAccountStorage.Accounts.FirstOrDefault(account => account.Username == identifier);
        }
    }
}
