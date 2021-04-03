using DotNetNuke.Web.Api;

namespace ClearAction.Modules.Connections
{
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("ClearAction_Connections", "default", "{controller}/{action}", new[] { "ClearAction.Modules.Connections" });
            mapRouteManager.MapHttpRoute("ClearAction_Connections", "Alert", "{controller}/{action}", new[] { "ClearAction.Modules.Connections" });
        }
    }
}