using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Enums;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        IAuthService _authService;

        [HttpPost]
        [Route("login")]
       public  async Task<ActionResult<LoginResponse>> Login([FromBody] AuthInfo authInfo)
        {
            var response = await _authService.Login(authInfo);
            return new JsonResult(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthEnum>> Register([FromBody] AuthInfo authInfo)
        {
            var response = await _authService.Register(authInfo);
            return new JsonResult(response);
        }

    }
}