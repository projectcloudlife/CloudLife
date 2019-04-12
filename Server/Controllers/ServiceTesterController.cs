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
        AuthInfo _Goodinfo;
        AuthInfo _Badinfo;

        public ServiceTesterController(IAuthService authService, IFileService fileService)
        {
            _authService = authService;
            _fileService = fileService;
            _Goodinfo = new AuthInfo { Username = "assaf", Password = "123456" };
            _Badinfo = new AuthInfo { Username = "assaf", Password = "1" };
        }

        [HttpGet]
        [Route("register")]
        public async Task<ActionResult<bool>> Register()
        {           
            var goodResult = await _authService.Register(_Goodinfo);            
            var badUserResult = await _authService.Register(_Goodinfo);            
            var BadPasswordResult = await _authService.Register(_Badinfo);            
            if (goodResult == AuthEnum.Success && BadPasswordResult == AuthEnum.BadPassword
                && badUserResult == AuthEnum.BadUsername) { return true; }
            else { return false; }
        }

        [HttpGet]
        [Route("login")]
        public async Task<ActionResult<bool>> Login()
        {
            var info = new AuthInfo { Username = "assaf1", Password = "123456" };
            var res = await _authService.Register(info);
            var result = await _authService.Login(info);
            var badResult = await _authService.Login(_Badinfo);
            if (result.AuthInfo == AuthEnum.Success && result.Token!="") { return true; }
            else { return false; }
        }

    }
}