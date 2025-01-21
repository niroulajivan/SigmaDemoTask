using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SigmaCandidateManagement.Core.Entities.Configuration;
using SigmaCandidateManagement.Core.Interfaces.Services;

namespace SigmaCandidateManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginRequestController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly LoginParam _loginparam;

        public LoginRequestController(
            ITokenService tokenService,
            IOptions<LoginParam> loginparam)
        {
            _tokenService = tokenService;
            _loginparam = loginparam.Value;
        }

        /// <summary>
        /// Login to the system and generate a JWT token if credentials are valid.
        /// </summary>
        /// <param name="request">Login request containing Email and Password.</param>
        /// <returns>A JWT token if login is successful, Unauthorized status otherwise.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginParam request, [FromServices] ITokenService tokenService)
        {
            //Checking whether email and password matched or not.
            if (request.Email == _loginparam.Email && request.Password == _loginparam.Password)
            {
                //token generation
                var token = tokenService.GenerateToken(request.Email, "Admin");
                //success response
                return Ok(new ApiResponse<object>(
                 200,
                 "Success",
                 new { Token = token },
                 string.Empty
             ));
            }
            //unathorized response.
            return Unauthorized(new ApiResponse<object>(
               401,
               "Failed",
               new { },
               "Invalid credentials"
           ));
        }

    }

}
