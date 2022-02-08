using System.Threading.Tasks;
using Authentication.Services;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;
using Repository.DAL;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _dbContext;
        public UsersController(IUserService userService, AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> RegisterAsync(RegisterDto dto)
        {
            string result = await _userService.RegisterAsync(dto);
            if (result.Contains("Registered"))
            {
                await _dbContext.SaveChangesAsync();
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetTokenAsync(TokenRequestDto model)
        {
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }
    }
}
