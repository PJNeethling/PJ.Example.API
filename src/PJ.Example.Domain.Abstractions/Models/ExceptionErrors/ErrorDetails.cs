﻿using Newtonsoft.Json;

namespace PJ.Example.Domain.Abstractions.Models.ExceptionErrors
{
    [Serializable]
    public class ErrorDetails
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "message")]
        public string Message { get; set; }
    }
}