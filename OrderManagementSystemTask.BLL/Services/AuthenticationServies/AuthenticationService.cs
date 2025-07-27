using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderManagementSystemTask.BLL.Dtos.AuthenticationDto;
using OrderManagementSystemTask.BLL.Dtos.ErrorDtos;
using OrderManagementSystemTask.DAL.Entities;
using OrderManagementSystemTask.DAL.Presistance.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderManagementSystemTask.BLL.Services.AuthenticationServies
{
    public class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> options,IUnitOfWork _unitOfWork) : IAuthenticationService
    {
        public async Task<UserResultDto> Login(LoginDto loginDto)
        {
            //Check email exists
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthorizedException();
            //Check password
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new UnAuthorizedException();
            return new UserResultDto(userName: user.UserName,token: await CreateTokenAsync(user),email: user.Email!);
        }

        public async Task<UserResultDto> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.Email.Split('@')[0],
                PhoneNumber = registerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }
            await _userManager.AddToRoleAsync(user, "Customer");
            var customer = new Customer
            {
                UserId = user.Id,
                Name = user.UserName,
                Email = user.Email
            };
            _unitOfWork.CustomerRepostory.Add(customer);
            await _unitOfWork.CompleteAsync();
            return new UserResultDto(userName: user.UserName, token: await CreateTokenAsync(user), email: user.Email!);
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = options.Value;
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            var siginCareds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDays),
                signingCredentials: siginCareds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
