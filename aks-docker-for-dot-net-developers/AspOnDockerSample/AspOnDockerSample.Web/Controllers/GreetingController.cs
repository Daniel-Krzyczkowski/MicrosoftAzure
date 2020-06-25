using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspOnDockerSample.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "HELLO FROM APPLICATION RUNNING IN DOCKER CONTAINER ORCHESTRATED BY KUBERNETES!";
        }
    }
}
