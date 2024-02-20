using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AdminReviewModel
    {
        public int FilmID { get; set; }

        [Required]
        public string FilmName { get; set; }
        
        [Required]
        public DateTime Release_Date { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public float Rating { get; set; }

        public string Review { get; set; }

        [Required]
        public bool Liked { get; set; }
    }
}
