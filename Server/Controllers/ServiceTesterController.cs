using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Enums;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Attributes;
using Server.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [TestController]
    [ApiController]
    public class ServiceTesterController : ControllerBase
    {
        IAuthService _authService;
        IFileService _fileService;

        public ServiceTesterController(IAuthService authService, IFileService fileService)
        {
            _authService = authService;
            _fileService = fileService;
        }

        [HttpGet]
        [Route("authService")]
        public async Task<ActionResult<bool>> Register()
        {
            var info = new AuthInfo { Username = "assaf", Password = "123456" };
            var result = await _authService.Register(info);
            if (result == AuthEnum.Success) { return true; }
            else { return false; }
        }

    }
}