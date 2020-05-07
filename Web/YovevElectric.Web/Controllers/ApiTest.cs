using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YovevElectric.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YovevElectric.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiTest : ControllerBase
    {

        [HttpPost]
        public void Post(Test input)
        {
            ;
        }
    }
}
