using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Authentication
{
    public enum AuthenticationStatus : int
    {
        Ok,
        NotFound,
        InvalidCredentials,
        Locked,
        Deactivated
    }
}
