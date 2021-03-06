﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Swimbait.Server.Controllers
{
    [Route("error")]
    public class ErrorController : BaseController
    {
        public ErrorController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            
        }

        /// <summary>
        /// "StatusCode" is already in a base class
        /// </summary>
        [Route("Status/{statusCode}")]
        public IActionResult MusicCastStatus(int statusCode)
        {
            Log.LogInformation($"404: {Request.Path}");
            //logic to generate status code response

            return new NotFoundResult();
        }
    }
}
