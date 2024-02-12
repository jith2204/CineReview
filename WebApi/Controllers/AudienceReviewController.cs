using Data.Enums;
using Data.Models;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmReview.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class AudienceReviewController : ControllerBase
    {
        IAudienceReviewService _audienceReviewService;

        public AudienceReviewController(
        IAudienceReviewService audienceReviewService)
        {
            _audienceReviewService = audienceReviewService;
        }

        [HttpPost]
        public async Task<ResponseModel> CreateAudienceReview([FromBody] AudienceReviewModel model)
        {
            return await _audienceReviewService.CreateAudienceReview(model);
        }

        [HttpGet]
        public async Task<SearchResult<AudienceReviewPaginationModel>> GetAudienceReviewWithPagination([FromQuery] AudienceReviewSortFilterModel model)
        {
            return await _audienceReviewService.GetAudienceReview(model);
        }

        [HttpPut]
        public async Task<ResponseModel> UpdateAudienceReview(string filmName, Language language, int year, [FromBody] AudienceReviewModel model)
        {
            return await _audienceReviewService.UpdateAudienceReview(filmName, language, year, model);
        }

        [HttpDelete]
        public async Task<ResponseModel> DeleteAudienceReview(string filmName, Language language, int year)
        {
            return await _audienceReviewService.DeleteAudienceReview(filmName, language, year);
        }

    }
}
