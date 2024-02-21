using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(String.Format(message))
        {
        }
    }
}
