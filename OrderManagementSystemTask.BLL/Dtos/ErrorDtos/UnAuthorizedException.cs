namespace OrderManagementSystemTask.BLL.Dtos.ErrorDtos
{
    public sealed class UnAuthorizedException(string msg = "Invalid email or password") : Exception(msg)
    {
    }
}
