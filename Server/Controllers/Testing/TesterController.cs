using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Attributes;
using Server.Extantions;

namespace Server.Controllers.Testing
{
    [Route("tests")]
    public class TesterController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.endpoints = GetAllTestEndpoints();
            return View();
        }

        IEnumerable<string> GetAllTestEndpoints()
        {
            var assembly = GetType().Assembly;
            var endpoints = new List<string>();

            var testControllers = assembly.GetTypes().Where(type => {
                if (type.GetCustomAttribute(typeof(TestControllerAttribute)) != null)
                {
                    return true;
                } 
                return false;    
            });

            foreach (var controller in testControllers)
            {
                endpoints.AddRange(controller.GetRoutedEndpoints());
            }

            return endpoints;
        }

    }
}