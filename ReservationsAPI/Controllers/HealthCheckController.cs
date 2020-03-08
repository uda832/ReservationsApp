using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationsAPI.Utilities;

namespace ReservationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            string status = HealthCheckUtility.GetStatus();

            return (status == "offline")
                ? StatusCode(StatusCodes.Status503ServiceUnavailable, new 
                    {
                        Status = StatusCodes.Status503ServiceUnavailable,
                        Message = "service is offline",
                    })
                : Ok(status);
        }
    }
}