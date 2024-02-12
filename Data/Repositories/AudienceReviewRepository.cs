using Data.Entities;
using Data.Interfaces;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AudienceReviewRepository : GenericRepository<AudienceReview>, IAudienceReviewRepository
    {
        IDataContext _context;
        public AudienceReviewRepository(IDataContext context) : base(context)
        {
            _context = context;
        }
    }
}
