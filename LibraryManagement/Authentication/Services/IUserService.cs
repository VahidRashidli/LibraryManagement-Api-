using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.Dtos;

namespace Authentication.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestDto dto);
    }
}
