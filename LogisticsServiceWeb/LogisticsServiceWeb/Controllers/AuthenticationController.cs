using LogisticsService.BLL.Interfaces;
using LogisticsServiceWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsServiceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegistrationViewModel user)
        {
            try
            {
                bool IsRegistered = _authenticationService
                    .Registration(
                    user.Email, 
                    user.Password,
                    user.CompanyName, 
                    user.UserType);

                return Ok(IsRegistered);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            try
            {
                LoginResponseViewModel loginResponseViewModel = new LoginResponseViewModel();
                loginResponseViewModel.Token =  _authenticationService.Login(user.Email, user.Password);
                loginResponseViewModel.UserType = _userService.GetUserTypeByEmail(user.Email).ToString();

                return Ok(loginResponseViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
