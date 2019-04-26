using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveTesting.AspNetCore.LogicObjects;
using LiveTesting.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("tests")]
    [ApiController]
    public class LiveTestingController : ControllerBase
    {
        public LiveTestingController(ITestRunner testRunner)
        {
            _testRunner = testRunner;
        }

        ITestRunner _testRunner;

        [HttpGet]
        public ActionResult Get()
        {
            Response.Headers.Add("Content-Type", "text/html; charset=UTF-8");
            var formatter = new HtmlTestFormatter();

            var result = new ContentResult();
            result.Content = formatter.Format(_testRunner.RunTests());
            return result;
        }

    }
}