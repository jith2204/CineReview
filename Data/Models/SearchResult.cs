using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class SearchResult<T> where T : class
    {
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
