using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebProject.Entities
{
    public class Email
    {
        public readonly string address;
        public readonly string subject;
        public readonly string expectedTestBody;

        public Email(string address, string subject, string expectedTestBody)
        {
            this.address = address;
            this.subject = subject;
            this.expectedTestBody = expectedTestBody;
        }
    }
}
