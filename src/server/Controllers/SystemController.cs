﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Server;
using Newtonsoft.Json;
using Swimbait.Server.Controllers.Requests;
using Swimbait.Server.Controllers.Responses;
using Swimbait.Server.Services;

namespace Swimbait.Server.Controllers
{
    [Route("YamahaExtendedControl/v1/system")]
    public class SystemController : BaseController
    {
        private MusicCastHost _musicCastHost;

        public SystemController(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _musicCastHost = new MusicCastHost();
        }

        [HttpGet("getFuncStatus")]
        public IActionResult GetFuncStatus()
        {
            var response = new FuncStatusResponse();
            response.response_code = 0;
            response.auto_power_standby = true;
            return new ObjectResult(response);
        }

        [HttpGet("GetFeatures")]
        public IActionResult GetFeatures()
        {
            var response = new FeaturesResponse();
            return new ObjectResult(response);
        }

        [HttpGet("getLocationInfo")]
        public IActionResult GetLocationInfo()
        {
            var response = new LocationInfoResponse();
            response.id = _musicCastHost.LocationId;
            return new ObjectResult(response);
        }

        [HttpGet("getTag")]
        public IActionResult GetTag()
        {
            var response = new GetTagResponse();
            response.zone_list.Add(new IntegerInputList {id = "main", tag = 5});
            response.input_list.Add("bluetooth", 0);
            response.input_list.Add("server", 0);
            response.input_list.Add("net_radio", 0);
            response.input_list.Add("pandora", 0);
            response.input_list.Add("spotify", 0);
            response.input_list.Add("airplay", 0);
            response.input_list.Add("mc_link", 0);
            return new ObjectResult(response);
        }

        [HttpPost("SetLocationName")]
        public IActionResult SetLocationName()
        {
            var json = Request.Form.Keys.First();
            var request = JsonConvert.DeserializeObject<SetLocationNameRequest>(json);
            Log.LogInformation($"Set name={request.name}");
            return Ok();
        }

        [HttpGet("getNameText")]
        public IActionResult GetNameText()
        {
            var response = new NameTextResponse();
            response.zone_list.Add("main", _musicCastHost.Name);
            response.input_list.Add("bluetooth", "Bluetooth");
            response.input_list.Add("server", "Server");
            response.input_list.Add("net_radio", "Net Radio");
            response.input_list.Add("pandora", "Pandora");
            response.input_list.Add("spotify", "Spotify");
            response.input_list.Add("airplay", "Air Play");
            response.input_list.Add("mc_link", "MC Link");
            return new ObjectResult(response);
        }

        [HttpGet("getNetworkStandby")]
        public IActionResult GetNetworkStandby()
        {
            var response = new NetworkStandbyResponse();
            response.network_standby = "on";
            return new ObjectResult(response);
        }

        [HttpGet("getDeviceInfo")]
        public IActionResult GetDeviceInfo()
        {
            var response = new DeviceInfoResponse();
            response.model_name = "WX-030";
            response.destination = "A";
            response.system_id = _musicCastHost.SerialNumber;
            response.system_version = _musicCastHost.SystemVersion;
            response.api_version = _musicCastHost.ApiVersion;
            response.netmodule_version= "0516 ";
            response.netmodule_checksum = "DF4473CE";
            response.system_version = _musicCastHost.SystemVersion;
            response.operation_mode = "normal";
            response.update_error_code = "00000000";

            return new ObjectResult(response);
        }
    }
}
