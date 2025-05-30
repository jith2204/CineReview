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

    public class AdminReviewController : ControllerBase
    {
        private readonly IAdminReviewService _adminReviewService;

        public AdminReviewController(
        IAdminReviewService adminReviewService)
        {
            _adminReviewService = adminReviewService;
        }

        /// <summary>
        /// Creates an Admin Review
        /// </summary>
        /// <param name="model"></param>

        [HttpPost]
        [SwaggerOperation(Summary = "Create an Admin Review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel> CreateAdminReview([FromBody] AdminReviewModel model)
        {
            return await _adminReviewService.CreateAdminReview(model);
        }


        /// <summary>
        /// Displays all Admin Reviews by Filtering and Sorting with Pagination
        /// </summary>
        /// <param name="model"></param>

        [HttpGet]
        [SwaggerOperation(Summary = "Displays all Admin Reviews by Filtering and Sorting with Pagination")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<SearchResult<AdminReviewPaginationModel>> GetAdminReviewWithPagination([FromQuery] AdminReviewSortFilterModel model)
        {
            return await _adminReviewService.GetAdminReview(model);
        }


        /// <summary>
        /// Updates an Admin Review
        /// </summary>
        /// <param name="filmName"></param>
        /// <param name="language"></param>
        /// <param name="year"></param>
        /// <param name="model"></param>

        [HttpPut]
        [SwaggerOperation(Summary = "Update an Admin Review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel> UpdateAdminReview(string filmName, Language language, int year, [FromBody] AdminReviewModel model)
        {
            return await _adminReviewService.UpdateAdminReview(filmName, language, year, model);
        }


        /// <summary>
        /// Deletes an Admin Review
        /// </summary>
        /// <param name="filmName"></param>
        /// <param name="language"></param>
        /// <param name="year"></param>

        [HttpDelete]
        [SwaggerOperation(Summary = "Delete an Admin Review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel> DeleteAdminReview(string filmName, Language language, int year)
        {
            return await _adminReviewService.DeleteAdminReview(filmName, language, year);
        }
    }
}
