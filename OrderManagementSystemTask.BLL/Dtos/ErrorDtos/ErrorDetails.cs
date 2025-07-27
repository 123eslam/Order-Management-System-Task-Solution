using System.Text.Json;

namespace OrderManagementSystemTask.BLL.Dtos.ErrorDtos
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public IEnumerable<string>? Errors { get; set; }
        public override string ToString()
           => JsonSerializer.Serialize(this);
    }
}
