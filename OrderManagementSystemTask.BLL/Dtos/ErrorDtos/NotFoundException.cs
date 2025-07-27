namespace OrderManagementSystemTask.BLL.Dtos.ErrorDtos
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg)
        {

        }
    }
}
