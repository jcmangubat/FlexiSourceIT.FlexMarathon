using Microsoft.AspNetCore.Mvc;

namespace FlexiSourceIT.FlexMarathon.API.Controllers
{
    public class APIControllerBase<TApiController>(
        ILogger<TApiController> logger) : ControllerBase
        where TApiController : class
    {
        protected readonly ILogger<TApiController> _logger = logger;
    }
}
