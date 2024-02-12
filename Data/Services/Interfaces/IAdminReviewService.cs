using Data.Entities;
using Data.Enums;
using Data.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Interfaces
{
    public interface IAdminReviewService
    {
        Task<ResponseModel> CreateAdminReview(AdminReviewModel model);

        Task<SearchResult<AdminReviewPaginationModel>> GetAdminReview(AdminReviewSortFilterModel model);

        Task<ResponseModel> UpdateAdminReview(string filmName, Language language, int year, AdminReviewModel model);

        Task<ResponseModel> DeleteAdminReview(string filmName, Language language, int year);
    }
}
