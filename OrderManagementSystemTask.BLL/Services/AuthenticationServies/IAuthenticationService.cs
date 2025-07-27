using OrderManagementSystemTask.BLL.Dtos.AuthenticationDto;

namespace OrderManagementSystemTask.BLL.Services.AuthenticationServies
{
    public interface IAuthenticationService
    {
        public Task<UserResultDto> Login(LoginDto loginDto);
        public Task<UserResultDto> Register(RegisterDto registerDto);
    }
}
