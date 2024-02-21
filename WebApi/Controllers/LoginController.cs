using Data.Models;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        ILoginService _loginRepository; 

        public AccountController(
        ILoginService loginRepository)
        {
            _loginRepository = loginRepository;
        }

        /// <summary>
        /// Creates an User Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>one user account is registered</returns>
        /// <response code ="200">Successfully added an User</response>
        /// <response code ="400">BadRequest</response>
        /// <response code ="409">Username or Email Already Exist</response>
        /// <response code ="500">InternalServerError</response>
       
        [HttpPost("Register")]
        [SwaggerOperation(Summary = "Register an User Account")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ResponseModel> UserRegistration(RegisterModel model)
        {

            var result = await _loginRepository.Register(model);
            return result;

        }


        /// <summary>
        /// Login into an User Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns>one user account is logged in</returns>
        /// <response code ="200">Successfully logged in as User</response>
        /// <response code ="400">BadRequest </response>
        /// <response code ="404">Login Not Found </response>
        /// <response code ="500">InternalServerError </response>
       
        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Log In for User Functionalities")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<TokenInfo> UserLogin(LoginModel model)
        {
                var result = await _loginRepository.Login(model);
                return result;
        }
    }
}
