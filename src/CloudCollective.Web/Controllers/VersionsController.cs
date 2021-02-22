using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CloudCollective.Web.Controllers
{
    [ApiController]
    [Route("versions")]
    [ApiVersion("1.0", Deprecated = true)]
    public class VersionsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>
            {
                "v1.0.1",
                "v1.0.2",
                "v1.0.3"
            };
        }
    }

    [ApiController]
    [Route("versions")]
    [ApiVersion("2.0")]
    public class Versions2Controller : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>
            {
                "v2.0.34-pre",
                "v2.0.56",
                "v2.0.3"
            };
        }
    }

    [ApiController]
    [Route("versions")]
    [ApiVersion("3.0")]
    public class Versions3Controller : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>
            {
                "v3.0.1-pre",
                "v3.0.3",
                "v3.0.89"
            };
        }
    }
}
