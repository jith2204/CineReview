using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class TrendingMoviesModel
    {
        public string FilmName { get; set; }

        public Language Language { get; set; }

        public int Year { get; set; }

        public float Rating { get; set; }
    }
}
