
using DotNetNuke.Web.Api;
using System.Web.Http;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Services
{

    /// <summary>
    /// The ServiceRouteMapper tells the DNN Web API Framework what routes this module uses
    /// </summary>
    public class ServiceRouteMapper : IServiceRouteMapper
    {
        /// <summary>
        /// RegisterRoutes is used to register the module's routes
        /// </summary>
        /// <param name="mapRouteManager"></param>
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            
            mapRouteManager.MapHttpRoute(
                moduleFolderName: "ClearAction_ProfileSetup",
                routeName: "default",
                url: "{controller}/{action}",
                defaults: new { itemId = RouteParameter.Optional },
                namespaces: new[] { "ClearAction.Modules.ProfileClearAction_ProfileSetup.Services" });

            mapRouteManager.MapHttpRoute(
              moduleFolderName: "ClearAction_ProfileSetup",
              routeName: "Profile",
              url: "{controller}/{action}",
              defaults: new { itemId = RouteParameter.Optional },
              namespaces: new[] { "ClearAction.Modules.ProfileClearAction_ProfileSetup.Services" });


        }
    }

}