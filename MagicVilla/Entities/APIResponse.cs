using System.Net;

namespace MagicVilla.Entities
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessage { get; set;}
        public bool IsSuccess { get; set; }
        public object Result { get; set; }
    }
}
