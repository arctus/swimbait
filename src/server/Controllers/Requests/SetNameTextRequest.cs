﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swimbait.Server.Controllers.Requests
{
    public class SetNameTextRequest
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}
