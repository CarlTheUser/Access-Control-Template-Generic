using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerIdentity.Model
{
    public class Name
    {
        public string PartFirst { get; }
        public string PartMiddle { get; }
        public string PartLast { get; }

        public override bool Equals(object obj)
        {
            var name = obj as Name;
            return name != null &&
                   PartFirst == name.PartFirst &&
                   PartMiddle == name.PartMiddle &&
                   PartLast == name.PartLast;
        }

        public override int GetHashCode()
        {
            var hashCode = -398465673;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PartFirst);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PartMiddle);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PartLast);
            return hashCode;
        }
    }
}
