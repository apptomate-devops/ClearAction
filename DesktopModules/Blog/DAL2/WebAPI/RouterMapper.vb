Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports DotNetNuke.Web.Api

Public Class RouterMapper
    Implements IServiceRouteMapper
    'Public Sub RegisterRoutes(mapRouteManager As IMapRoute)
    '    mapRouteManager.MapHttpRoute("ConsultKaro", "default", "{controller}/{action}", New String() {"ConsultKaro.CKAPI"})
    'End Sub

    Private Sub IServiceRouteMapper_RegisterRoutes(mapRouteManager As IMapRoute) Implements IServiceRouteMapper.RegisterRoutes
        ' mapRouteManager.MapHttpRoute("Blog", "ClearAction", "{controller}/{action}", New String() {"DotNetNuke.Modules.Blog.API"})
    End Sub
End Class
