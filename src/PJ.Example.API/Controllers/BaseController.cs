using Microsoft.AspNetCore.Mvc;
using PJ.Example.Domain.Abstractions.SharedKeys;

namespace PJ.Example.API.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// The correlation id correlating the exchange account to exchange transactions
        /// </summary>
        [FromHeader(Name = HeaderKeys.Header_AuthToken)]
        public string AuthToken { get; set; }
    }
}