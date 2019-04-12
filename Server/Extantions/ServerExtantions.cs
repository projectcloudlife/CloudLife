using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Server.Extantions
{
    public static class ServerExtantions
    {
        public static int UserId(this ControllerBase controller)
        {
            string Id = controller.User.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;
            return int.Parse(Id);
        }
        public static IEnumerable<string> GetRoutedEndpoints(this Type controller)
        {
            var methods = controller.GetRuntimeMethods();
            var controllerRouteAttr = controller.GetCustomAttribute(typeof(RouteAttribute));

            var controllerRoute = "";
            if(controllerRouteAttr != null)
            {
                var controllerName = controller.Name.Replace("Controller", "");
                controllerRoute = ((RouteAttribute)controllerRouteAttr).Template.Replace("[controller]", controllerName);
            }

            var actionMethods = methods.Where(method => method.GetCustomAttribute(typeof(RouteAttribute)) != null);

            return actionMethods.Select(method => {
                var attr = (RouteAttribute)method.GetCustomAttribute(typeof(RouteAttribute));
                return controllerRoute +"/"+attr.Template;
            });
        }

        public static FileCommon ToCommon(this FileDB file)
        {
            return new FileCommon()
            {
                Data = file.Data,
                Id = file.Id,
                InRecycleBin = file.InRecycleBin,
                IsPublic = file.IsPublic,
                Name = file.Name,
                SizeInBytes = file.SizeInBytes,
                UserId = file.UserId
            };
        }

        public static FileDB ToDB(this FileCommon file)
        {
            return new FileDB()
            {
                Data = file.Data,
                Id = file.Id,
                InRecycleBin = file.InRecycleBin,
                IsPublic = file.IsPublic,
                Name = file.Name,
                SizeInBytes = file.SizeInBytes,
                UserId = file.UserId
            };
        }

    }
}
