using IdentityService.Business.Abstract;
using IdentityService.Entities.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService appUserService)
        {
            _userService = appUserService;
        }

        [HttpPost("Login")]
        public IActionResult UserLogin([FromBody] UserLoginDTO userLogin)
        {
            var result = _userService.Login(userLogin);
            return Ok(result);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDTO userRegister)
        {
            
            var result = await _userService.Register(userRegister);
            return Ok(result);
        }

        // sitename/api/user?email=ehmed@comapar.edu.az&token=skjdf-sdfa-sdfsd-fsdf
        [HttpGet("VerifyEmail")]
        public IActionResult VerifyEmail([FromQuery] string email, [FromQuery] string token)
        {
            var result = _userService.VerifyEmail(email, token);
            return Ok(result);
        }




    }
}
