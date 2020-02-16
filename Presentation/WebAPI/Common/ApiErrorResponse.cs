using Newtonsoft.Json;

namespace Todo.WebAPI.Common
{
    public class ApiErrorResponse
    {
        public string Endpoint { get; set; }

        public string Method { get; set; }

        public string Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}