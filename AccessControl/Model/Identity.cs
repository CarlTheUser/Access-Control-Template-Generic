using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Model
{
    public sealed class Identity
    {
        public int Value { get; }

        public Identity(int value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var identity = obj as Identity;
            return identity != null &&
                   Value == identity.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }
    }
}
