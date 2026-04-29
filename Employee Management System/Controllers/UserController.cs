using EMS.Application.Common.Responses;
using EMS.Application.DTO.Empolyee;
using EMS.Application.DTO.User;
using EMS.Application.Sevices.Interfaces;
using EMS.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IAuthService _authService;

        public UserController(
            ILogger<UserController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        // Helper to avoid repeating switch everywhere
        private IActionResult MapResponse(ApiResponse result)
        {
            return result.StatusCode switch
            {
                AppStatusCodesEnum.Success => Ok(result),
                AppStatusCodesEnum.InvalidData => BadRequest(result),
                AppStatusCodesEnum.NotFound => NotFound(result),
                _ => StatusCode(500, result)
            };
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _authService.Login(loginDTO);
            return result.StatusCode switch
            {
                AppStatusCodesEnum.Success => Ok(result),
                AppStatusCodesEnum.InvalidData => BadRequest(result),
                AppStatusCodesEnum.NotFound => NotFound(result),
                _ => StatusCode(500, result)
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await _authService.Register(registerDTO);

            return result.StatusCode switch
            {
                AppStatusCodesEnum.Success => Ok(result),
                AppStatusCodesEnum.InvalidData => BadRequest(result),
                AppStatusCodesEnum.NotFound => NotFound(result),
                _ => StatusCode(500, result)
            };
        }
    }
}
      