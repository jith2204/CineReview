using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AudienceRatingSortFilterModel
    {
        public string? FilmName { get; set; }

        public Language? Language { get; set; }

        public int? Year { get; set; }

        public float? Rating { get; set; }
       
        public float? RatingGreaterthan { get; set; }
       
        public float? RatingLessthan { get; set; }

        public string? SortOrder { get; set; }
       
        public string? SortingElement { get; set; }

        public int PageNumber { get; set; }
        
        public int PageSize { get; set; }
    }
}
