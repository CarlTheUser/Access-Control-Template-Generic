using AccessControl.Infrastructure;
using AccessControl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Demo.Infrastructure
{
    public class NewAccountWriter : INewAccountWriter
    {
        public void Write(Account record)
        {
            StubAccountStorage.SaveRecord(record);
        }
    }
}
