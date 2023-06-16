using Foodie.Services.AuthAPI.Model.Dto;
using Foodie.Services.AuthAPI.Models.Dto;
using Foodie.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Foodie.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto register)
        {
            var error = await _authService.Register(register);
            if (error != null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = error;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
        {
            var user = await _authService.Login(login);
            if (user == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "UserName & Password is incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result= user;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto requestDto)
        {
            var assigned = await _authService.AssignRole(requestDto.Email, requestDto.Role.ToUpper());
            if (!assigned)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Encountered!";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}
