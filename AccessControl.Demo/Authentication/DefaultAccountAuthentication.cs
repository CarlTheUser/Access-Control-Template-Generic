using AccessControl.Authentication;
using AccessControl.Authentication.Security;
using AccessControl.Infrastructure;
using AccessControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Demo.Authentication
{
    public class DefaultAccountAuthentication : AccountAuthentication
    {
        public DefaultAccountAuthentication(IAccountSource accountSource) : base(accountSource)
        {
        }

        public override AuthenticationResult Authenticate(string identifier, string password)
        {
            Account found = accountSource.FromUserIdentifier(identifier);

            if (found != null)
            {
                ISecuredPassword passwordChecker = new EncryptionSecuredPassword();

                if (passwordChecker.Verify(password, found.Password))
                {
                    return new OkResult(found);
                }
                else return new InvalidCredentialsResult(found.Username);
            }
            else return AuthenticationResult.NotFound;
        }
    }
}
