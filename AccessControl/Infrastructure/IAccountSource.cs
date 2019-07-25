using AccessControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Infrastructure
{
    public interface IAccountSource
    {
        Account FromUserIdentifier(string identifier);

        Account FromTechnicalIdentifier(Identity identity);
    }
}
