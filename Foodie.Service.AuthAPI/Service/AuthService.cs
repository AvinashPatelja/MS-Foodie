using Foodie.Services.AuthAPI.Data;
using Foodie.Services.AuthAPI.Model.Dto;
using Foodie.Services.AuthAPI.Models;
using Foodie.Services.AuthAPI.Models.Dto;
using Foodie.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Foodie.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string role)
        {
            var user = _context.ApplicationUsers.First(x => x.UserName.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, role);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.UserName);
            if (user != null)
            {
                bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (isValid)
                {
                    UserDto userDto = new()
                    {
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        ID = user.Id
                    };

                    var token = _jwtTokenGenerator.GenerateToken(user);

                    LoginResponseDto responseDto = new()
                    {
                        User = userDto,
                        Token = token
                    };
                    return responseDto;
                }
            }
            return null;
        }

        public async Task<string> Register(RegistrationRequestDto registerDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                NormalizedEmail = registerDto.Email.ToUpper(),
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    var response = _context.ApplicationUsers.First(x => x.UserName == registerDto.Email);

                    UserDto userDto = new()
                    {
                        Email = response.Email,
                        Name = response.Name,
                        PhoneNumber = response.PhoneNumber,
                        ID = response.Id
                    };
                    return null;
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
