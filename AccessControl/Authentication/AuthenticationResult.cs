using AccessControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Authentication
{
    public abstract class AuthenticationResult
    {
        public static readonly AuthenticationResult NotFound = new NotFoundResult();

        public AuthenticationStatus Status { get; }

        protected AuthenticationResult(AuthenticationStatus status)
        {
            Status = status;
        }
    }

    public class OkResult : AuthenticationResult
    {
        public Account Account { get; }

        public OkResult(Account account) : base(AuthenticationStatus.Ok) { Account = account; }
    }

    public class NotFoundResult : AuthenticationResult
    {
        public NotFoundResult() : base(AuthenticationStatus.NotFound) { }
    }

    public class LockedResult : AuthenticationResult
    {
        public DateTime Lift { get; }

        public LockedResult(DateTime lift) : base(AuthenticationStatus.Locked) { Lift = lift; }
    }

    public class DeactivatedResult : AuthenticationResult
    {
        public DateTime Since { get; }

        public DeactivatedResult(DateTime since) : base(AuthenticationStatus.Deactivated) { Since = since; }
    }

    public class InvalidCredentialsResult : AuthenticationResult
    {
        // User might sign in using phone number, email, etc. Formal identifier may be username
        public string FormalIdentifier { get; }

        public InvalidCredentialsResult(string identifier) : base(AuthenticationStatus.InvalidCredentials) { FormalIdentifier = identifier; }
    }
}
