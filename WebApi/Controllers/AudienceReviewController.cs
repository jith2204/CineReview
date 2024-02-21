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
        IAudienceReviewService _audienceReviewService;

        public AudienceReviewController(
        IAudienceReviewService audienceReviewService)
        {
            _audienceReviewService = audienceReviewService;
        }


        /// <summary>
        /// Creates an Audience Review
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The Successful Output Response</returns>
        ///  <response code ="200">Successfully Inserted the Audience Review</response>
        ///  <response code ="400">BadRequest</response>
        ///  <response code ="401">Unauthorized</response>
        ///  <response code ="403">Forbidden</response>
        ///  <response code ="409">Review Already Exist</response>
        ///  <response code ="500">InternalServerError</response>

        [HttpPost]
        [SwaggerOperation(Summary = "Create an Audience Review")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ResponseModel> CreateAudienceReview([FromBody] AudienceReviewModel model)
        {
            return await _audienceReviewService.CreateAudienceReview(model);
        }


        /// <summary>
        /// Displays all Audience Reviews by Filtering and Sorting with Pagination
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of all Audience Reviews</returns>
        /// <response code ="200">Successfully Displayed All Audience Reviews</response>
        /// <response code ="401">Unauthorized</response>
        /// <response code ="500">InternalServerError</response>

        [HttpGet]
        [SwaggerOperation(Summary = "Displays all Audience Reviews by Filtering and Sorting with Pagination")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [HttpGet]
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
        /// <returns>The Successful Output Response</returns>
        /// <response code ="200">Successfully Updated the Audience Review</response>
        /// <response code ="400">BadRequest</response>
        /// <response code ="401">Unauthorized</response>
        /// <response code ="403">Forbidden</response>
        /// <response code ="404">Review Not Found</response>
        /// <response code ="409">Review Already Exist</response>
        /// <response code ="500">InternalServerError</response>

        [HttpPut]
        [SwaggerOperation(Summary = "Update an Audience Review")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        [HttpPut]
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
        /// <returns>The Successful Output Response</returns>
        /// <response code ="200">Successfully Deleted the Audience Review</response>
        /// <response code ="401">Unauthorized</response>
        /// <response code ="403">Forbidden</response>
        /// <response code ="404">Review Not Found</response>
        /// <response code ="500">InternalServerError</response>

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete an Audience Review")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ResponseModel> DeleteAudienceReview(string filmName, Language language, int year)
        {
            return await _audienceReviewService.DeleteAudienceReview(filmName, language, year);
        }

    }
}
