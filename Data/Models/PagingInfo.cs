using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PagingInfo
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public bool HasNextPage { get; set; }

        public PagingInfo()
        {

        }

        public PagingInfo(int page, int size, int totalItems)
        {
            Page = page;
            Size = size;
            TotalItems = totalItems;
        }
    }
}
