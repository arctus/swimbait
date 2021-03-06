﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Swimbait.Server.Services;
using Swimbait.Common.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using System.IO;

namespace Swimbait.Server
{
    /// <summary>
    /// Fluent extension
    /// </summary>
    public static class ActivityLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseActivityLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ActivityLogMiddleware>();
        }
    }

    public class ActivityLogMiddleware
    {
        private readonly RequestDelegate next;
        static object _lockObject = new object();
        
        public ActivityLogMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var environmentService = (EnvironmentService) context.RequestServices.GetService(typeof (EnvironmentService));

            var request = context.Request;
            var uri = request.GetUri();
            var path = uri.PathAndQuery;
            
            Directory.CreateDirectory(environmentService.ReplayLogFolder);
            
            var thisPort = uri.Port;
            var yamahaPort = MapPortToReal(uri);

            var body = "<not decoded>";
            if (!uri.ToString().Contains("secure") && request.Method.ToLower() == "post")
            {
                body = StreamService.ReadText(request.Body);
            }

            var lineContent = $"{DateTime.Now.ToString("hh:mm:ss.ff")},{thisPort},{yamahaPort},{request.Method},{path},{body},{Environment.NewLine}";

            lock (_lockObject)
            {
                File.AppendAllText(environmentService.ActivityLogFilename, lineContent);
            }

            return next(context);
        }


        public static int MapPortToReal(Uri thisRequest)
        {
            // remap the port since windows is using the port Yamaha uses
            var relayPort = thisRequest.Port == EnvironmentService.SwimbaitDlnaPort ? EnvironmentService.YamahaDlnaPort : thisRequest.Port;
            return relayPort;
        }
    }
}
