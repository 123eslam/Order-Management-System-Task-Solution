namespace OrderManagementSystemTask.BLL.Dtos.AuthenticationDto
{
    public class UserResultDto
    {
        public UserResultDto(string userName, string email, string token)
        {
            UserName = userName;
            Email = email;
            Token = token;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
