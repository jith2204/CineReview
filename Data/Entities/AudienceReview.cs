using Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AudienceReview
    {
        [Key]
        [Required]
        public int ReviewID { get; set; }

        [Required]
        public string FilmName { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public float Rating { get; set; }

        public string? Review { get; set; }

        [Required]
        public bool Liked { get; set; }
        
        public DateTime UpdatedTime { get; set; }

        
        [ForeignKey("User")]
        public string UpdatedBy { get; set; }
        public User User { get; set; }
    }
}
