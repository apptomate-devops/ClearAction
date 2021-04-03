using DotNetNuke.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction.Modules.WebCast
{
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("ClearAction_WebCast", "default", "{controller}/{action}", new[] { "ClearAction.Modules.WebCast" });
        }
    }
}