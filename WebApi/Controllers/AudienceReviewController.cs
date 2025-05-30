using Data.Enums;
using Data.Models;
using Data.Repositories;
using Data.Repositories.Interfaces;
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

    public class AudienceReviewController : ControllerBase
    {
        private readonly IAudienceReviewService _audienceReviewService;

        public AudienceReviewController(
        IAudienceReviewService audienceReviewService)
        {
            _audienceReviewService = audienceReviewService;
        }


        /// <summary>
        /// Creates an Audience Review
        /// </summary>
        /// <param name="model"></param>

        [HttpPost]
        [SwaggerOperation(Summary = "Create an Audience Review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel> CreateAudienceReview([FromBody] AudienceReviewModel model)
        {
            return await _audienceReviewService.CreateAudienceReview(model);
        }


        /// <summary>
        /// Displays all Audience Reviews by Filtering and Sorting with Pagination
        /// </summary>
        /// <param name="model"></param>

        [HttpGet]
        [SwaggerOperation(Summary = "Displays all Audience Reviews by Filtering and Sorting with Pagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<SearchResult<AudienceReviewPaginationModel>> GetAudienceReviewWithPagination([FromQuery] AudienceReviewSortFilterModel model)
        {
            return await _audienceReviewService.GetAudienceReview(model);
        }


        /// <summary>
        /// Updates an Audience Review
        /// </summary>
        /// <param name="filmName"></param>
        /// <param name="language"></param>
        /// <param name="year"></param>
        /// <param name="model"></param>

        [HttpPut]
        [SwaggerOperation(Summary = "Update an Audience Review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel> UpdateAudienceReview(string filmName, Language language, int year, [FromBody] AudienceReviewModel model)
        {
            return await _audienceReviewService.UpdateAudienceReview(filmName, language, year, model);
        }


        /// <summary>
        /// Deletes an Audience Review
        /// </summary>
        /// <param name="filmName"></param>
        /// <param name="language"></param>
        /// <param name="year"></param>

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete an Audience Review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel> DeleteAudienceReview(string filmName, Language language, int year)
        {
            return await _audienceReviewService.DeleteAudienceReview(filmName, language, year);
        }
    }
}
