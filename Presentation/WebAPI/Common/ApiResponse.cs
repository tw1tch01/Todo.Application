using System.Collections.Generic;
using Newtonsoft.Json;

namespace Todo.WebAPI.Common
{
    public class ApiResponse
    {
        public string Endpoint { get; set; }

        public string Method { get; set; }

        public string Message { get; set; }

        public IDictionary<string, object> Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}