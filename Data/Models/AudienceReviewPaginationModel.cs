using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AudienceReviewPaginationModel
    {
        public int ReviewID { get; set; }

        public string? FilmName { get; set; }

        public int Year { get; set; }

        public string? Language { get; set; }

        public float Rating { get; set; }

        public string? Review { get; set; }

        public bool? Liked { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string? UpdatedBy { get; set; }

        public string? UserName { get; set; }

        public string? UserUserName { get; set; }
    }
}
