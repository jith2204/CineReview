using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Interfaces
{
    public interface ILoginService
    {
        Task<ResponseModel> Register(RegisterModel model);

        Task<TokenInfo> Login(LoginModel model);
    }
}
