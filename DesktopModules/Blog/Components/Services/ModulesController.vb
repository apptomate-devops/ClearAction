'
' DNN Connect - http://dnn-connect.org
' Copyright (c) 2015
' by DNN Connect
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Option Strict On
Option Explicit On
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports DotNetNuke.Web.Api
Imports DotNetNuke.Entities.Modules
Imports System.Xml

Namespace Services

 Public Class ModulesController
  Inherits DnnApiController

  Public Class AddModuleDTO
   Public Property paneName As String
   Public Property position As Integer
   Public Property title As String
   Public Property template As String
  End Class

#Region " Private Members "
#End Region

#Region " Service Methods "
  <HttpPost()>
  <DnnModuleAuthorize(accesslevel:=DotNetNuke.Security.SecurityAccessLevel.Edit)>
  <ValidateAntiForgeryToken()>
  <ActionName("Add")>
  Public Function AddModule(postData As AddModuleDTO) As HttpResponseMessage

   Dim newSettings As New Common.ViewSettings(-1)
   newSettings.BlogModuleId = ActiveModule.ModuleID
   newSettings.Template = postData.template

   Dim objModule As New ModuleInfo
   objModule.Initialize(PortalSettings.PortalId)
   objModule.PortalID = PortalSettings.PortalId
   objModule.TabID = PortalSettings.ActiveTab.TabID
   objModule.ModuleOrder = postData.position
   If postData.title = "" Then
    objModule.ModuleTitle = "Blog"
   Else
    objModule.ModuleTitle = postData.title
   End If
   objModule.PaneName = postData.paneName
   objModule.ModuleDefID = ActiveModule.ModuleDefID
   objModule.InheritViewPermissions = True
   objModule.AllTabs = False
   objModule.Alignment = ""

   Dim objModules As New ModuleController
   objModule.ModuleID = objModules.AddModule(objModule)
   newSettings.UpdateSettings(objModule.TabModuleID)

   Return Request.CreateResponse(HttpStatusCode.OK, New With {.Result = "success"})

  End Function

  <HttpGet()>
  <AllowAnonymous()>
  <ActionName("Manifest")>
  Public Function GetManifest() As HttpResponseMessage

   Dim res As New HttpResponseMessage(HttpStatusCode.OK)

   Using out As New StringWriterWithEncoding(Encoding.UTF8)
    Using output As New XmlTextWriter(out)
     Dim bs As ModuleSettings = ModuleSettings.GetModuleSettings(ActiveModule.ModuleID)

     output.Formatting = Formatting.Indented
     output.WriteStartDocument()
     output.WriteStartElement("manifest")
     output.WriteAttributeString("xmlns", "http://schemas.microsoft.com/wlw/manifest/weblog")
     output.WriteStartElement("options")

     output.WriteElementString("clientType", "Metaweblog")
     output.WriteElementString("supportsMultipleCategories", bs.AllowMultipleCategories.ToYesNo)
     output.WriteElementString("supportsCategories", CBool(bs.VocabularyId <> -1).ToYesNo)
     output.WriteElementString("supportsCustomDate", "Yes")
     output.WriteElementString("supportsKeywords", "Yes")
     output.WriteElementString("supportsTrackbacks", "Yes")
     output.WriteElementString("supportsEmbeds", "No")
     output.WriteElementString("supportsAuthor", "No")
     output.WriteElementString("supportsExcerpt", (CBool(bs.SummaryModel = Common.Globals.SummaryType.PlainTextIndependent)).ToYesNo)
     output.WriteElementString("supportsPassword", "No")
     output.WriteElementString("supportsPages", "No")
     output.WriteElementString("supportsPageParent", "No")
     output.WriteElementString("supportsPageOrder", "No")
     output.WriteElementString("supportsEmptyTitles", "No")
     output.WriteElementString("supportsExtendedEntries", (CBool(bs.SummaryModel = Common.Globals.SummaryType.HtmlPrecedesPost)).ToYesNo)
     output.WriteElementString("supportsCommentPolicy", "No")
     output.WriteElementString("supportsPingPolicy", "No")
     output.WriteElementString("supportsPostAsDraft", "Yes")
     output.WriteElementString("supportsFileUpload", "Yes")
     output.WriteElementString("supportsSlug", "No")
     output.WriteElementString("supportsHierarchicalCategories", "Yes")
     output.WriteElementString("supportsCategoriesInline", "Yes")
     output.WriteElementString("supportsNewCategories", "No")
     output.WriteElementString("supportsNewCategoriesInline", "No")
     output.WriteElementString("requiresXHTML", "Yes")

     output.WriteEndElement() ' options
     output.WriteEndElement() ' manifest
     output.Flush()

    End Using

    res.Content = New StringContent(out.ToString, System.Text.Encoding.UTF8, "application/xml")

   End Using

   Return res

  End Function
#End Region

#Region " Private Methods "
#End Region

 End Class

#Region " Helper Class for Manifest Writing "
 Public Class StringWriterWithEncoding
  Inherits IO.StringWriter

  Private Property _Encoding As Encoding

  Public Sub New(enc As Encoding)
   MyBase.New()
   _Encoding = enc
  End Sub

  Public Overrides ReadOnly Property Encoding As System.Text.Encoding
   Get
    Return _Encoding
   End Get
  End Property

 End Class
#End Region

End Namespace