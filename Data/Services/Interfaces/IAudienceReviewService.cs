using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Interfaces
{
    public interface IAudienceReviewService
    {
        Task<ResponseModel> CreateAudienceReview(AudienceReviewModel model);
        Task<SearchResult<AudienceReviewPaginationModel>> GetAudienceReview(AudienceReviewSortFilterModel model);
        Task<ResponseModel> UpdateAudienceReview(string filmName, Language language, int year, AudienceReviewModel model);
        Task<ResponseModel> DeleteAudienceReview(string filmName, Language language, int year);

    }
}
    
