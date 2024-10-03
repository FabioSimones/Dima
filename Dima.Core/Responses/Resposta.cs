using Dima.Core;
using System.Text.Json.Serialization;

namespace Dima.Api.Response
{
    public class Resposta<TData>
    {
        
        private readonly int _code;

        public TData? Data { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess => 
            _code is >= 200 and <= 299;

        [JsonConstructor]
        public Resposta() 
            => _code = Configuration.DefaultStatusCode;
        

        public Resposta(TData? data, int code = Configuration.DefaultStatusCode, string? message = null)
        {
            Data = data;
            Message = message;
            _code = code;
        }        
    }
}
