using Data.Models;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace FilmReview.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class GetListController : ControllerBase
    {
        private readonly IListofValuesService _listofValuesService;

        public GetListController(IListofValuesService listofValuesService)
        {
           _listofValuesService = listofValuesService;
        }


        /// <summary>
        /// Gets the value corresponding to each genre types
        /// </summary>
        /// <returns>List of Genre types with values</returns>

        [HttpGet("GenreValues")]
        [SwaggerOperation(Summary = "Display Values for each type of Genres")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<string> GetValuesforGenres()
        {
            return await _listofValuesService.GetGenreList();
        }


        /// <summary>
        /// Gets the value corresponding to each language types
        /// </summary>
        /// <returns>List of Language types with values</returns>

        [HttpGet("LanguageValues")]
        [SwaggerOperation(Summary = "Display Values for each type of Languages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<string> GetValuesforLanguages()
        {
            return await _listofValuesService.GetLanguageList();
        }
    }
}

