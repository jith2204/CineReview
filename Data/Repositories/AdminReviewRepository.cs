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
    public class AdminReviewRepository : GenericRepository<AdminReview>, IAdminReviewRepository
    {
        IDataContext _context;
        public AdminReviewRepository(IDataContext context) : base(context)
        {
            _context = context;
        }
    }
}
