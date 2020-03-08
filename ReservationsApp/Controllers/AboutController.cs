using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReservationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {

        public ActionResult Get()
        {
            return Ok(new { 
                Description = "API is the service for the ReservationApp",
                AssemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(),
            });
        }
    }
}