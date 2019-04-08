using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Enums;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        [Route("login")]
        Task<ActionResult<LoginResponse>> Login([FromBody] AuthInfo authInfo)
        {
            return null;
        }

        [HttpPost]
        [Route("register")]
        Task<ActionResult<AuthEnum>> Register([FromBody] AuthInfo authInfo)
        {
            return null;
        }

    }
}