using Data.Enums;
using Data.Models;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ResponseModel> CreateAdminReview([FromBody] AdminReviewModel model)
        {
              return await _adminReviewRepository.CreateAdminReview(model);
        }

        [HttpGet]
        public async Task<SearchResult<AdminReviewPaginationModel>> GetAdminReviewWithPagination([FromQuery] AdminReviewSortFilterModel model)
        {
            return await _adminReviewRepository.GetAdminReview(model);
        }

        [HttpPut]
        public async Task<ResponseModel> UpdateAdminReview(string filmName, Language language, int year, [FromBody] AdminReviewModel model)
        {
            return await _adminReviewRepository.UpdateAdminReview(filmName, language, year, model);
        }

        [HttpDelete]
        public async Task<ResponseModel> DeleteAdminReview(string filmName, Language language, int year)
        {
            return await _adminReviewRepository.DeleteAdminReview(filmName, language, year);
        }
    }
}
 