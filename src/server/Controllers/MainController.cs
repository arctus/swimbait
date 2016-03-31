﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Swimbait.Server.Controllers.Responses;
using Swimbait.Server.Services;

namespace Swimbait.Server.Controllers
{
    [Route("YamahaExtendedControl/v1/main")]
    public class MainController : BaseController
    {
        private MusicCastHost _musicCastHost;

        public MainController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _musicCastHost = new MusicCastHost();
        }



        [HttpGet("getStatus")]
        public IActionResult GetStatus()
        {
            var response = new StatusResponse();
            response.response_code = 0;
            response.power = "on";
            response.sleep = 0;
            response.volume = 30;
            response.mute = false;
            response.max_volume = 60;
            response.input = "mc_link";
            response.distribution_enable = false;
            response.equalizer = new Equalizer {low =0, mid=0, high =0};
            response.link_control = "standard";
            response.disable_flags = 0;
            return new ObjectResult(response);
        }
    }
}
