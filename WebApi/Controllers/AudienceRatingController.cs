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
        IAudienceRatingService _audienceRatingService;
       
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
        /// <response code ="200">Successfully Displayed All Audience Ratings</response>
        /// <response code ="401">Unauthorized</response>
        /// <response code ="500">InternalServerError</response>
        
        [HttpGet]
        [SwaggerOperation(Summary = "Display all Audience Ratings by Filtering and Sorting with Pagination")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<SearchResult<AudienceRatingPaginationModel>> GetAudienceRatingWithPagination([FromQuery] AudienceRatingSortFilterModel model)
        {
            return await _audienceRatingService.GetAudienceRating(model);
        }


        /// <summary>
        /// Displays all Trending Movies of last 14 days based on Audience Ratings  
        /// </summary>
        /// <returns>List of Trending Movies</returns>
        /// <response code ="200">Successfully Displayed All Trending Movies</response>
        /// <response code ="401">Unauthorized</response>
        /// <response code ="500">InternalServerError</response>
      
        [HttpGet("TrendingMovies")]
        [SwaggerOperation(Summary = "Display all Trending Movies of last 14 days based on Audience Ratings")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IEnumerable<TrendingMoviesModel>> GetTrendingMovies()
        {
            return await _audienceRatingService.GetTrendingMovies();
        }
    }
}
