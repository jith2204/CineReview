using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AdminReviewPaginationModel
    {
        public int FilmID { get; set; }

        public string FilmName { get; set; }

        public DateTime Release_Date { get; set; }

        public string Language { get; set; }

        public string Genre { get; set; }

        public float Rating { get; set; }

        public string Review { get; set; }

        public bool Liked { get; set; }
      
        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }
    }
}
