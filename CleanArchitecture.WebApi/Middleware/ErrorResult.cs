using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArchitecture.WebApi.Middleware
{
    public sealed class ErrorResult : ErrorStatusCode
    {
        public string Message { get; set; } = null!;
    }
    public class ErrorStatusCode
    {
        public int StatusCode { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public sealed class ValidationErrorResult : ErrorStatusCode
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
