using Foodie.Services.AuthAPI.Model.Dto;
using Foodie.Services.AuthAPI.Models.Dto;

namespace Foodie.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registerDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string role);
    }
}
