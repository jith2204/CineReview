using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ResponseModel
    {
           

            public bool Success { get; set; }
            public string Message { get; set; }
            public string DateTime { get; set; }
            public List<string> Errors { get; set; }
        
    }
}
