using AccessControl.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Authentication
{
    public abstract class AccountAuthentication
    {
        protected readonly IAccountSource accountSource;

        protected AccountAuthentication(IAccountSource accountSource)
        {
            this.accountSource = accountSource;
        }

        public abstract AuthenticationResult Authenticate(string identifier, string password);
    }
}
