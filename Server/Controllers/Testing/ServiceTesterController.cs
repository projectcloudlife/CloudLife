using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Enums;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Attributes;
using Server.DAL.Interfaces;
using Server.Extantions;
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
        ITokenGeneratorService _tokenGeneratorService;
        IUserRepository _userRepository;
        AuthInfo _Goodinfo;
        AuthInfo _UsernameTakenRegister;
        AuthInfo _BadUsernameLogin;
        AuthInfo _BadPasswordRegister;
        AuthInfo _BadPasswordLogin;

        public ServiceTesterController(IAuthService authService, IUserRepository userRepository, IFileService fileService, ITokenGeneratorService tokenService)
        {
            _authService = authService;
            _fileService = fileService;
            _userRepository = userRepository;
            _tokenGeneratorService = tokenService;
            _Goodinfo = new AuthInfo { Username = "assaf", Password = "123456" };
            _UsernameTakenRegister = new AuthInfo { Username = "assaf", Password = "1" };
            _BadUsernameLogin = new AuthInfo { Username = "assafdasas", Password = "1" };
            _BadPasswordRegister = new AuthInfo { Username = "assaf1", Password = "1" };
            _BadPasswordLogin = new AuthInfo { Username = "assaf", Password = "1" };
        }

        [HttpGet]
        [Route("Register")]
        public async Task<ActionResult<bool>> Register()
        {
            var goodResult = await _authService.Register(_Goodinfo);

            var badUserResult = await _authService.Register(_Goodinfo);

            var BadPasswordResult = await _authService.Register(_BadPasswordRegister);

            if (goodResult == AuthEnum.Success &&
                BadPasswordResult == AuthEnum.BadPassword &&
                badUserResult == AuthEnum.BadUsername)
            {
                return true;
            }

            return false;

        }

        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult<bool>> Login()
        {
            var info = new AuthInfo { Username = "assaf1", Password = "123456" };

            await _authService.Register(info);

            var result = await _authService.Login(info);

            var badUserResult = await _authService.Login(_BadUsernameLogin);

            var badPasswordResult = await _authService.Login(_BadPasswordLogin);

            if (result.AuthResponse == AuthEnum.Success &&
                result.Token != "" &&
                badUserResult.AuthResponse == AuthEnum.BadUsername &&
                badPasswordResult.AuthResponse == AuthEnum.BadPassword)
            {
                return true;
            }

            return false;
        }

        [HttpGet]
        [Route("getById")]
        public async Task<ActionResult<bool>> GetById()
        {
            var info = new AuthInfo { Username = "assaf12", Password = "123456" };
            var info2 = new AuthInfo { Username = "assa", Password = "123456" };

            await _authService.Register(info);

            var id = await _userRepository.GetId(info);
            var id2 = await _userRepository.GetId(info);
            var id3 = await _userRepository.GetId(info2);

            return true;
        }

        [HttpGet]
        [Route("filesservices")]
        public async Task<ActionResult<bool>> Files()
        {
            var info = new AuthInfo { Username = "assaf2", Password = "123456" };

            await _authService.Register(info);

            var userId = await _userRepository.GetId(info);

            var fileList = await _fileService.GetFiles(userId, true);

            var begin = fileList.ToList().Count;

            var file = new FileCommon
            {
                IsPublic = true,
                InRecycleBin = false,
                Name = "file.exe",
                SizeInBytes = 21323,
                Data = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                UserId = userId
            };

            await _fileService.UploadFile(file);

            fileList = await _fileService.GetFiles(userId, true);

            var end = fileList.ToList().Count;

            return (end - begin == 1);
        }

    }
}