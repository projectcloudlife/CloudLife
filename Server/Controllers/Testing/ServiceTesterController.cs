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

namespace Server.Controllers.Testing
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
        AuthInfo _Badinfo2;

        public ServiceTesterController(IAuthService authService, IFileService fileService)
        {
            _authService = authService;
            _fileService = fileService;
            _Goodinfo = new AuthInfo { Username = "assaf", Password = "123456" };
            _Badinfo = new AuthInfo { Username = "assaf", Password = "1" };
            _Badinfo2 = new AuthInfo { Username = "assaf1", Password = "1" };
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
           await _authService.Register(info);
            var result = await _authService.Login(info);
            var badUserResult = await _authService.Login(_Badinfo);
            var badPasswordResult = await _authService.Login(_Badinfo2);
            if (result.AuthInfo == AuthEnum.Success && result.Token!="" 
                && badUserResult.AuthInfo==AuthEnum.BadUsername
                && badPasswordResult.AuthInfo==AuthEnum.BadPassword) { return true; }
            else { return false; }
        }

        [HttpGet]
        [Route("filesservices")]
        public async Task<ActionResult<bool>> Files()
        {
            var info = new AuthInfo { Username = "assaf2", Password = "123456" };
            await _authService.Register(info);
            var userId = await _authService.GetId(info);
            var fileList = await _fileService.GetFiles(true);
            var begin = fileList.ToList().Count;
            var file = new FileCommon {
                IsPublic = true,
                InRecycleBin = false,
                Name = "file.exe",
                SizeInBytes = 21323,
                Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                UserId = userId
            };
            await _fileService.UploadFile(file);
            fileList = await _fileService.GetFiles(true);
            var end = fileList.ToList().Count;
            return (end - begin == 1);



        }

    }
}