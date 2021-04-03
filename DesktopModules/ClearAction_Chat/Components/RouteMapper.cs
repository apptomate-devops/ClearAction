using System.Web.Routing;
using DotNetNuke.Web.Api;

namespace ClearAction_Chat.Components
{
    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            RouteTable.Routes.MapHubs();
        }
    }
}