﻿using System;

namespace Swimbait.Server.Services
{
    public class ResponseLog
    {
        public Uri RequestUri { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }
    }
}