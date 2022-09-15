using Newtonsoft.Json;

namespace PJ.Example.Domain.Abstractions.Models.ExceptionErrors
{
    [Serializable]
    public class ErrorResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "error")]
        public ExceptionDetails Error { get; set; }
    }
}