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
    public class AdminReview
    {
        [Key]
        [Required]
        public int FilmID {  get; set; }
       
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

        [Required]
        public string Review { get; set; }

        [Required]
        public bool Liked { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        public DateTime? UpdatedTime { get; set; }
    }
}
