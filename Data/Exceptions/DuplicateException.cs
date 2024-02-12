using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string message) : base(String.Format(message))
        {
        }
    }
}
