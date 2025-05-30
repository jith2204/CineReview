using Data.Models;
using Data.Services;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FilmReview.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AudienceRatingController : ControllerBase
    {
        private readonly IAudienceRatingService _audienceRatingService;
       
        public AudienceRatingController(
        IAudienceRatingService audienceRatingService)
        {
            _audienceRatingService = audienceRatingService;
        }


        /// <summary>
        /// Displays all Audience Ratings by Filtering and Sorting with Pagination
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of all Audience Ratings</returns>
        
        [HttpGet]
        [SwaggerOperation(Summary = "Display all Audience Ratings by Filtering and Sorting with Pagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<SearchResult<AudienceRatingPaginationModel>> GetAudienceRatingWithPagination([FromQuery] AudienceRatingSortFilterModel model)
        {
            return await _audienceRatingService.GetAudienceRating(model);
        }


        /// <summary>
        /// Displays all Trending Movies of last 14 days based on Audience Ratings  
        /// </summary>
        /// <returns>List of Trending Movies</returns>
      
        [HttpGet("TrendingMovies")]
        [SwaggerOperation(Summary = "Display all Trending Movies of last 14 days based on Audience Ratings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<TrendingMoviesModel>> GetTrendingMovies()
        {
            return await _audienceRatingService.GetTrendingMovies();
        }
    }
}
