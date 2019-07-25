using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Model
{
    public class HashedPassword
    {
        public byte[] Salt { get; }
        public byte[] Value { get; }

        public HashedPassword(byte[] salt, byte[] value)
        {
            //Salt = salt.Length > 0 ? salt : throw new AccessDomainViolationException("Salt has no value.");
            //Value = value.Length > 0 ? value : throw new AccessDomainViolationException("Password has no value.");
            Salt = salt;
            Value = value;
        }
    }
}
