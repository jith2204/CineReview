using Data.Models;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FilmReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly ILoginService _loginRepository;

        public AccountController(
        ILoginService loginRepository)
        {
            _loginRepository = loginRepository;
        }


        /// <summary>
        /// Creates an User Account
        /// </summary>
        /// <param name="model"></param>

        [HttpPost("Register")]
        [SwaggerOperation(Summary = "Register an User Account")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseModel> UserRegistration(RegisterModel model)
        {

            var result = await _loginRepository.Register(model);
            return result;

        }


        /// <summary>
        /// Login into an User Account
        /// </summary>
        /// <param name="model"></param>

        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Login for User Functionalities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<TokenInfo> UserLogin(LoginModel model)
        {
            var result = await _loginRepository.Login(model);
            return result;
        }
    }
}
