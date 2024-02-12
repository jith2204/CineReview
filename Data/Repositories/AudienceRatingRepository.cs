using Data.Entities;
using Data.Interfaces;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AudienceRatingRepository : GenericRepository<AudienceRating>, IAudienceRatingRepository
    {
        IDataContext _context;
        public AudienceRatingRepository(IDataContext context) : base(context)
        {
            _context = context;
        }
    }
}

