using Cors.CorsComponent;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cors.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors(PolicyName = Constants.DefaultCorsPolicy)]
    public class ProductController : Controller
    {
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
