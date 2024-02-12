using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AudienceRatingPaginationModel
    {
        public string FilmName { get; set; }

        public string Language { get; set; }

        public int Year { get; set; }

        public float TotalRatings { get; set; }

        public int TotalReviewers { get; set; }

        public float Rating { get; set; }

        public DateTime LastReviewedTime { get; set; }
    }
}
