using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CloudCollective.Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>
            {
                "Lollipop",
                "Ice cream",
                "iPad",
                "Samsung S10"
            };
        }
    }
}
