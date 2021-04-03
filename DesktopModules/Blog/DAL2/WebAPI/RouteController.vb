
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports DotNetNuke.Web.Api
Imports System.Web.Http
Imports System.Net.Http
Imports System.Net
Imports DotNetNuke.Services.Exceptions
Namespace API

    Public Class BlogControllerBase
        Inherits DnnApiController

        <DnnAuthorize>
        <HttpGet>
        Public Function KeepAlive() As HttpResponseMessage
            Try
                Return Request.CreateResponse(HttpStatusCode.OK, "True")
            Catch ex As Exception
                'Log to DotNetNuke and reply with Error
                Exceptions.LogException(ex)
                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

    End Class
End Namespace


