using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AudienceRating
    {
        [Key]
        public int RatingId { get; set; }

        public string FilmName { get; set; }

        public Language Language { get; set; }

        public int Year { get; set; }

        public float TotalRatings { get; set; }

        public int TotalReviewers { get; set; }

        public float Rating {  get; set; }

        public DateTime LastReviewedTime { get; set; }
    }
}
