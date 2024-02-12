using Data.Models;
using Data.Repositories.Interfaces;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost("Register")]
        public async Task<ResponseModel> UserRegistration(RegisterModel model)
        {

            var result = await _loginRepository.Register(model);
            return result;

        }
    
       
        [HttpPost("Login")]
        public async Task<TokenInfo> UserLogin(LoginModel model)
        {
                var result = await _loginRepository.Login(model);
                return result;
        }
    }
}
