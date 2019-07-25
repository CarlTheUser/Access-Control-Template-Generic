using AccessControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Authentication.Security
{
    public interface ISecuredPassword
    {
        HashedPassword Create(string password);

        bool Verify(string test, HashedPassword password);
    }
}
