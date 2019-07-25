using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl
{
    public class AccessDomainViolationException : Exception
    {
        public AccessDomainViolationException(string message) : base(message) { }
    }
}
