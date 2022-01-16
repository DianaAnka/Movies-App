
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieInfo.Api.Controllers.Auth.Dto;
using MovieInfo.EntityFramework;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost(nameof(Register))]
        public async Task<UserManagerResponse> Register(RegisterDto input)
        {
            if (input == null)
                throw new NullReferenceException("Reigster Model is null");

            if (input.Password != input.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var identityUser = new ApplicationUser
            {
                Email = input.Email,
                UserName = input.Email,
                Age = input.Age
            };

            var result = await _userManager.CreateAsync(identityUser, input.Password);
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        [HttpPost(nameof(Login))]
        public async Task<UserManagerResponse> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, input.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Invalid Email or Password",
                    IsSuccess = false,
                };

            var claims = new[]
            {
                new Claim("Email", input.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = "Login Success ",
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                Token = $"Bearer {tokenAsString}"
            };
        }
    }
}
