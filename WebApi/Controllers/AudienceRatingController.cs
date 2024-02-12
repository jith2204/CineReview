using Data.Models;
using Data.Services;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmReview.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AudienceRatingController : ControllerBase
    {
        IAudienceRatingService _audienceRatingService;
       
        public AudienceRatingController(
        IAudienceRatingService audienceRatingService)
        {
            _audienceRatingService = audienceRatingService;
        }

        [HttpGet]
        public async Task<SearchResult<AudienceRatingPaginationModel>> GetAudienceRatingWithPagination([FromQuery] AudienceRatingSortFilterModel model)
        {
            return await _audienceRatingService.GetAudienceRating(model);
        }
    }
}
