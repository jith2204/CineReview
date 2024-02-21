using Data.Enums;
using Data.Models;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class AdminReviewController : ControllerBase
    {
        IAdminReviewService _adminReviewRepository;

        public AdminReviewController(
        IAdminReviewService adminReviewRepository)
        {
            _adminReviewRepository = adminReviewRepository;
        }

        /// <summary>
        /// Creates an Admin Review
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The Successful Output Response</returns>
        ///  <response code ="200">Successfully Inserted the Admin Review</response>
        ///  <response code ="400">BadRequest</response>
        ///  <response code ="401">Unauthorized</response>
        ///  <response code ="403">Forbidden</response>
        ///  <response code ="409">Review Already Exist</response>
        ///  <response code ="500">InternalServerError</response>

        [HttpPost]
        [SwaggerOperation(Summary = "Create an Admin Review")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ResponseModel> CreateAdminReview([FromBody] AdminReviewModel model)
        {
              return await _adminReviewRepository.CreateAdminReview(model);
        }

       
        /// <summary>
        /// Displays all Admin Reviews by Filtering and Sorting with Pagination
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of all Admin Reviews</returns>
        /// <response code ="200">Successfully Displayed All Admin Reviews</response>
        /// <response code ="401">Unauthorized</response>
        /// <response code ="500">InternalServerError</response>

        [HttpGet]
        [SwaggerOperation(Summary = "Displays all Admin Reviews by Filtering and Sorting with Pagination")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<SearchResult<AdminReviewPaginationModel>> GetAdminReviewWithPagination([FromQuery] AdminReviewSortFilterModel model)
        {
            return await _adminReviewRepository.GetAdminReview(model);
        }


        /// <summary>
        /// Updates an Admin Review
        /// </summary>
        /// <param name="filmName"></param>
        /// <param name="language"></param>
        /// <param name="year"></param>
        /// <param name="model"></param>
        /// <returns>The Successful Output Response</returns>
        /// <response code ="200">Successfully Updated the Admin Review</response>
        /// <response code ="400">BadRequest</response>
        /// <response code ="401">Unauthorized</response>
        /// <response code ="403">Forbidden</response>
        /// <response code ="404">Review Not Found</response>
        /// <response code ="409">Review Already Exist</response>
        /// <response code ="500">InternalServerError</response>

        [HttpPut]
        [SwaggerOperation(Summary = "Update an Admin Review")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ResponseModel> UpdateAdminReview(string filmName, Language language, int year, [FromBody] AdminReviewModel model)
        {
            return await _adminReviewRepository.UpdateAdminReview(filmName, language, year, model);
        }


        /// <summary>
        /// Deletes an Admin Review
        /// </summary>
        /// <param name="filmName"></param>
        /// <param name="language"></param>
        /// <param name="year"></param>
        /// <returns>The Successful Output Response</returns>
        /// <response code ="200">Successfully Deleted the Admin Review</response>
        /// <response code ="401">Unauthorized</response>
        /// <response code ="403">Forbidden</response>
        /// <response code ="404">Review Not Found</response>
        /// <response code ="500">InternalServerError</response>

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete an Admin Review")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ResponseModel> DeleteAdminReview(string filmName, Language language, int year)
        {
            return await _adminReviewRepository.DeleteAdminReview(filmName, language, year);
        }
    }
}
 