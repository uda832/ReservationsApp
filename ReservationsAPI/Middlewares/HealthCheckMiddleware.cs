using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ReservationsAPI.Middlewares
{
    public class HealthCheckMiddleware
    {
        private const string HEALTH_CHECK_ROOT_PATH = @"c:\Softwarehouse\Healthcheck"; //Consider making this a parameter
        private readonly string _serverName;
        private readonly string _siteName;

        private readonly RequestDelegate _next;
        private readonly ILogger<HealthCheckMiddleware> _logger;

        public HealthCheckMiddleware(RequestDelegate next,
                                     ILogger<HealthCheckMiddleware> logger,
                                     string serverName,
                                     string siteName)
        {
            _next = next;
            _logger = logger;
            _serverName = serverName;
            _siteName = siteName;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string serverStatusFilePath = $@"{HEALTH_CHECK_ROOT_PATH}\Offline.{_serverName}.txt";
            string siteStatusFilePath = $@"{HEALTH_CHECK_ROOT_PATH}\Offline.{_siteName}.txt";
            string status;


            if (System.IO.File.Exists(serverStatusFilePath))
            { 
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                status = "Server is offline";
            }
            else if (System.IO.File.Exists(siteStatusFilePath))
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                status = "Site is offline";
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                status = "Site is online";
            }

            string responseContent = $"{status} @ {DateTime.UtcNow.ToString()}";
            

            if (!System.IO.Directory.Exists(HEALTH_CHECK_ROOT_PATH))
            {
                string exceptionMsg = $"{_serverName} is missing the {HEALTH_CHECK_ROOT_PATH} folder";

                responseContent += Environment.NewLine + exceptionMsg;


                //TODO: Implement the correctlogic to log exception for the Splunk alert here
                //----------------------------------------------------------------------------
                //throw new ApplicationException(exceptionMsg);
                //----------------------------------------------------------------------------
            }


            //Write the Response body
            await context.Response.WriteAsync(responseContent);


            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }

    public static class HealthCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder builder, string serverName, string siteName)
        {
            return builder.Map("/HealthCheck", b => b.UseMiddleware<HealthCheckMiddleware>(serverName, siteName));
        }
    }
}
