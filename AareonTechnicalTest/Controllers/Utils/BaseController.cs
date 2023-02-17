using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace AareonTechnicalTest.Controllers.Utils
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ErrorsController
    {
        public readonly ApplicationContext Context;
        public BaseController(ApplicationContext context)
        {
            Context = context;
        }
    }
}
