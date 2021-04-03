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

Imports System.Runtime.Serialization
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization

Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Tokens

Imports DotNetNuke.Modules.Blog.Common.Globals

Namespace Entities.Blogs

 <Serializable(), XmlRoot("Blog"), DataContract()>
 Partial Public Class BlogInfo
  Implements IHydratable
  Implements IPropertyAccess
  Implements IXmlSerializable

#Region " ML Properties "
  Public Property ParentTabID As Integer = -1
#End Region

#Region " IHydratable Implementation "
  ''' -----------------------------------------------------------------------------
  ''' <summary>
  ''' Fill hydrates the object from a Datareader
  ''' </summary>
  ''' <remarks>The Fill method is used by the CBO method to hydrtae the object
  ''' rather than using the more expensive Refection  methods.</remarks>
  ''' <history>
  ''' 	[pdonker]	02/16/2013  Created
  ''' </history>
  ''' -----------------------------------------------------------------------------
  Public Sub Fill(dr As IDataReader) Implements IHydratable.Fill
   BlogID = Convert.ToInt32(Null.SetNull(dr.Item("BlogID"), BlogID))
   ModuleID = Convert.ToInt32(Null.SetNull(dr.Item("ModuleID"), ModuleID))
   Title = Convert.ToString(Null.SetNull(dr.Item("Title"), Title))
   Description = Convert.ToString(Null.SetNull(dr.Item("Description"), Description))
   Image = Convert.ToString(Null.SetNull(dr.Item("Image"), Image))
   Locale = Convert.ToString(Null.SetNull(dr.Item("Locale"), Locale))
   FullLocalization = Convert.ToBoolean(Null.SetNull(dr.Item("FullLocalization"), FullLocalization))
   Published = Convert.ToBoolean(Null.SetNull(dr.Item("Published"), Published))
   IncludeImagesInFeed = Convert.ToBoolean(Null.SetNull(dr.Item("IncludeImagesInFeed"), IncludeImagesInFeed))
   IncludeAuthorInFeed = Convert.ToBoolean(Null.SetNull(dr.Item("IncludeAuthorInFeed"), IncludeAuthorInFeed))
   Syndicated = Convert.ToBoolean(Null.SetNull(dr.Item("Syndicated"), Syndicated))
   SyndicationEmail = Convert.ToString(Null.SetNull(dr.Item("SyndicationEmail"), SyndicationEmail))
   Copyright = Convert.ToString(Null.SetNull(dr.Item("Copyright"), Copyright))
   MustApproveGhostPosts = Convert.ToBoolean(Null.SetNull(dr.Item("MustApproveGhostPosts"), MustApproveGhostPosts))
   PublishAsOwner = Convert.ToBoolean(Null.SetNull(dr.Item("PublishAsOwner"), PublishAsOwner))
   EnablePingBackSend = Convert.ToBoolean(Null.SetNull(dr.Item("EnablePingBackSend"), EnablePingBackSend))
   EnablePingBackReceive = Convert.ToBoolean(Null.SetNull(dr.Item("EnablePingBackReceive"), EnablePingBackReceive))
   AutoApprovePingBack = Convert.ToBoolean(Null.SetNull(dr.Item("AutoApprovePingBack"), AutoApprovePingBack))
   EnableTrackBackSend = Convert.ToBoolean(Null.SetNull(dr.Item("EnableTrackBackSend"), EnableTrackBackSend))
   EnableTrackBackReceive = Convert.ToBoolean(Null.SetNull(dr.Item("EnableTrackBackReceive"), EnableTrackBackReceive))
   AutoApproveTrackBack = Convert.ToBoolean(Null.SetNull(dr.Item("AutoApproveTrackBack"), AutoApproveTrackBack))
   OwnerUserId = Convert.ToInt32(Null.SetNull(dr.Item("OwnerUserId"), OwnerUserId))
   CreatedByUserID = Convert.ToInt32(Null.SetNull(dr.Item("CreatedByUserID"), CreatedByUserID))
   CreatedOnDate = CDate(Null.SetNull(dr.Item("CreatedOnDate"), CreatedOnDate))
   LastModifiedByUserID = Convert.ToInt32(Null.SetNull(dr.Item("LastModifiedByUserID"), LastModifiedByUserID))
   LastModifiedOnDate = CDate(Null.SetNull(dr.Item("LastModifiedOnDate"), LastModifiedOnDate))
   DisplayName = Convert.ToString(Null.SetNull(dr.Item("DisplayName"), DisplayName))
   Email = Convert.ToString(Null.SetNull(dr.Item("Email"), Email))
   Username = Convert.ToString(Null.SetNull(dr.Item("Username"), Username))
   NrPosts = Convert.ToInt32(Null.SetNull(dr.Item("NrPosts"), NrPosts))
   LastPublishDate = CDate(Null.SetNull(dr.Item("LastPublishDate"), LastPublishDate))
   NrViews = Convert.ToInt32(Null.SetNull(dr.Item("NrViews"), NrViews))
   FirstPublishDate = CDate(Null.SetNull(dr.Item("FirstPublishDate"), FirstPublishDate))
   AltLocale = Convert.ToString(Null.SetNull(dr.Item("AltLocale"), AltLocale))
   AltTitle = Convert.ToString(Null.SetNull(dr.Item("AltTitle"), AltTitle))
   AltDescription = Convert.ToString(Null.SetNull(dr.Item("AltDescription"), AltDescription))
   CanEdit = Convert.ToBoolean(Null.SetNull(dr.Item("CanEdit"), CanEdit))
   CanAdd = Convert.ToBoolean(Null.SetNull(dr.Item("CanAdd"), CanAdd))
   CanApprove = Convert.ToBoolean(Null.SetNull(dr.Item("CanApprove"), CanApprove))

  End Sub
  ''' -----------------------------------------------------------------------------
  ''' <summary>
  ''' Gets and sets the Key ID
  ''' </summary>
  ''' <remarks>The KeyID property is part of the IHydratble interface.  It is used
  ''' as the key property when creating a Dictionary</remarks>
  ''' <history>
  ''' 	[pdonker]	02/16/2013  Created
  ''' </history>
  ''' -----------------------------------------------------------------------------
  Public Property KeyID() As Integer Implements IHydratable.KeyID
   Get
    Return BlogID
   End Get
   Set(value As Integer)
    BlogID = value
   End Set
  End Property
#End Region

#Region " IPropertyAccess Implementation "
  Public Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As System.Globalization.CultureInfo, AccessingUser As DotNetNuke.Entities.Users.UserInfo, AccessLevel As DotNetNuke.Services.Tokens.Scope, ByRef PropertyNotFound As Boolean) As String Implements DotNetNuke.Services.Tokens.IPropertyAccess.GetProperty
   Dim OutputFormat As String = String.Empty
   Dim portalSettings As DotNetNuke.Entities.Portals.PortalSettings = DotNetNuke.Entities.Portals.PortalController.Instance.GetCurrentPortalSettings
   If strFormat = String.Empty Then
    OutputFormat = "D"
   Else
    OutputFormat = strFormat
   End If
   Select Case strPropertyName.ToLower
    Case "blogid"
     Return (BlogID.ToString(OutputFormat, formatProvider))
    Case "moduleid"
     Return (ModuleID.ToString(OutputFormat, formatProvider))
    Case "title"
     Return PropertyAccess.FormatString(Title, strFormat)
    Case "description"
     Return PropertyAccess.FormatString(Description, strFormat)
    Case "image"
     Return PropertyAccess.FormatString(Image, strFormat)
    Case "hasimage"
     Return CBool(Image <> "").ToString(formatProvider)
    Case "locale"
     Return PropertyAccess.FormatString(Locale, strFormat)
    Case "fulllocalization"
     Return FullLocalization.ToString
    Case "fulllocalizationyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(FullLocalization, formatProvider)
    Case "published"
     Return Published.ToString
    Case "publishedyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(Published, formatProvider)
    Case "includeimagesinfeed"
     Return IncludeImagesInFeed.ToString
    Case "includeimagesinfeedyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(IncludeImagesInFeed, formatProvider)
    Case "includeauthorinfeed"
     Return IncludeAuthorInFeed.ToString
    Case "includeauthorinfeedyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(IncludeAuthorInFeed, formatProvider)
    Case "syndicated"
     Return Syndicated.ToString
    Case "syndicatedyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(Syndicated, formatProvider)
    Case "syndicationemail"
     Return PropertyAccess.FormatString(SyndicationEmail, strFormat)
    Case "copyright"
     Return PropertyAccess.FormatString(Copyright, strFormat)
    Case "mustapproveghostposts"
     Return MustApproveGhostPosts.ToString
    Case "mustapproveghostpostsyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(MustApproveGhostPosts, formatProvider)
    Case "publishasowner"
     Return PublishAsOwner.ToString
    Case "publishasowneryesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(PublishAsOwner, formatProvider)
    Case "enablepingbacksend"
     Return EnablePingBackSend.ToString
    Case "enablepingbacksendyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(EnablePingBackSend, formatProvider)
    Case "enablepingbackreceive"
     Return EnablePingBackReceive.ToString
    Case "enablepingbackreceiveyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(EnablePingBackReceive, formatProvider)
    Case "autoapprovepingback"
     Return AutoApprovePingBack.ToString
    Case "autoapprovepingbackyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(AutoApprovePingBack, formatProvider)
    Case "enabletrackbacksend"
     Return EnableTrackBackSend.ToString
    Case "enabletrackbacksendyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(EnableTrackBackSend, formatProvider)
    Case "enabletrackbackreceive"
     Return EnableTrackBackReceive.ToString
    Case "enabletrackbackreceiveyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(EnableTrackBackReceive, formatProvider)
    Case "autoapprovetrackback"
     Return AutoApproveTrackBack.ToString
    Case "autoapprovetrackbackyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(AutoApproveTrackBack, formatProvider)
    Case "owneruserid"
     Return (OwnerUserId.ToString(OutputFormat, formatProvider))
    Case "createdbyuserid"
     Return (CreatedByUserID.ToString(OutputFormat, formatProvider))
    Case "createdondate"
     Return (CreatedOnDate.ToString(OutputFormat, formatProvider))
    Case "lastmodifiedbyuserid"
     Return (LastModifiedByUserID.ToString(OutputFormat, formatProvider))
    Case "lastmodifiedondate"
     Return (LastModifiedOnDate.ToString(OutputFormat, formatProvider))
    Case "displayname"
     Return PropertyAccess.FormatString(DisplayName, strFormat)
    Case "email"
     Return PropertyAccess.FormatString(Email, strFormat)
    Case "username"
     Return PropertyAccess.FormatString(Username, strFormat)
    Case "nrposts"
     Return (NrPosts.ToString(OutputFormat, formatProvider))
    Case "lastpublishdate"
     Return (LastPublishDate.ToString(OutputFormat, formatProvider))
    Case "nrviews"
     Return (NrViews.ToString(OutputFormat, formatProvider))
    Case "firstpublishdate"
     Return (FirstPublishDate.ToString(OutputFormat, formatProvider))
    Case "altlocale"
     Return PropertyAccess.FormatString(AltLocale, strFormat)
    Case "alttitle"
     Return PropertyAccess.FormatString(AltTitle, strFormat)
    Case "altdescription"
     Return PropertyAccess.FormatString(AltDescription, strFormat)
    Case "localizedtitle"
     Return PropertyAccess.FormatString(LocalizedTitle, strFormat)
    Case "localizeddescription"
     Return PropertyAccess.FormatString(LocalizedDescription, strFormat)
    Case "link", "permalink"
     Return PermaLink(DotNetNuke.Entities.Portals.PortalSettings.Current)
    Case "parenturl"
     Return PermaLink(ParentTabID)
    Case Else
     PropertyNotFound = True
   End Select

   Return Null.NullString
  End Function

  Public ReadOnly Property Cacheability() As DotNetNuke.Services.Tokens.CacheLevel Implements DotNetNuke.Services.Tokens.IPropertyAccess.Cacheability
   Get
    Return CacheLevel.fullyCacheable
   End Get
  End Property
#End Region

#Region " IXmlSerializable Implementation "
  ''' -----------------------------------------------------------------------------
  ''' <summary>
  ''' GetSchema returns the XmlSchema for this class
  ''' </summary>
  ''' <remarks>GetSchema is implemented as a stub method as it is not required</remarks>
  ''' <history>
  ''' 	[pdonker]	02/16/2013  Created
  ''' </history>
  ''' -----------------------------------------------------------------------------
  Public Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
   Return Nothing
  End Function

  Private Function readElement(reader As XmlReader, ElementName As String) As String
   If (Not reader.NodeType = XmlNodeType.Element) OrElse reader.Name <> ElementName Then
    reader.ReadToFollowing(ElementName)
   End If
   If reader.NodeType = XmlNodeType.Element Then
    Return reader.ReadElementContentAsString
   Else
    Return ""
   End If
  End Function

  ''' -----------------------------------------------------------------------------
  ''' <summary>
  ''' ReadXml fills the object (de-serializes it) from the XmlReader passed
  ''' </summary>
  ''' <remarks></remarks>
  ''' <param name="reader">The XmlReader that contains the xml for the object</param>
  ''' <history>
  ''' 	[pdonker]	02/16/2013  Created
  ''' </history>
  ''' -----------------------------------------------------------------------------
  Public Sub ReadXml(reader As XmlReader) Implements IXmlSerializable.ReadXml
   ' not implemented
  End Sub

  Friend Property ImportedBlogId As Integer = -1
  Friend Property ImportedPosts As List(Of Posts.PostInfo)
  Friend Property ImportedFiles As List(Of String)

  Public Sub FromXml(xml As XmlNode)
   If xml Is Nothing Then Exit Sub

   xml.ReadValue("BlogId", ImportedBlogId)
   xml.ReadValue("Title", Title)
   xml.ReadValue("TitleLocalizations", TitleLocalizations)
   xml.ReadValue("Description", Description)
   xml.ReadValue("DescriptionLocalizations", DescriptionLocalizations)
   xml.ReadValue("Image", Image)
   xml.ReadValue("Locale", Locale)
   xml.ReadValue("FullLocalization", FullLocalization)
   xml.ReadValue("Published", Published)
   xml.ReadValue("IncludeImagesInFeed", IncludeImagesInFeed)
   xml.ReadValue("IncludeAuthorInFeed", IncludeAuthorInFeed)
   xml.ReadValue("Syndicated", Syndicated)
   xml.ReadValue("SyndicationEmail", SyndicationEmail)
   xml.ReadValue("Copyright", Copyright)
   xml.ReadValue("MustApproveGhostPosts", MustApproveGhostPosts)
   xml.ReadValue("PublishAsOwner", PublishAsOwner)
   xml.ReadValue("EnablePingBackSend", EnablePingBackSend)
   xml.ReadValue("EnablePingBackReceive", EnablePingBackReceive)
   xml.ReadValue("AutoApprovePingBack", AutoApprovePingBack)
   xml.ReadValue("EnableTrackBackSend", EnableTrackBackSend)
   xml.ReadValue("EnableTrackBackReceive", EnableTrackBackReceive)
   xml.ReadValue("AutoApproveTrackBack", AutoApproveTrackBack)
   xml.ReadValue("Username", Username)
   xml.ReadValue("Email", Email)

   ImportedPosts = New List(Of Posts.PostInfo)
   For Each xPost As XmlNode In xml.SelectNodes("Posts/Post")
    Dim post As New Posts.PostInfo
    post.fromXml(xPost)
    ImportedPosts.Add(post)
   Next

   ImportedFiles = New List(Of String)
   For Each xFile As XmlNode In xml.SelectNodes("Files/File")
    ImportedFiles.Add(xFile.InnerText)
   Next

  End Sub

  ''' -----------------------------------------------------------------------------
  ''' <summary>
  ''' WriteXml converts the object to Xml (serializes it) and writes it using the XmlWriter passed
  ''' </summary>
  ''' <remarks></remarks>
  ''' <param name="writer">The XmlWriter that contains the xml for the object</param>
  ''' <history>
  ''' 	[pdonker]	02/16/2013  Created
  ''' </history>
  ''' -----------------------------------------------------------------------------
  Public Sub WriteXml(writer As XmlWriter) Implements IXmlSerializable.WriteXml
   writer.WriteStartElement("Blog")
   writer.WriteElementString("BlogId", BlogID.ToString)
   writer.WriteElementString("Title", Title)
   writer.WriteElementString("TitleLocalizations", TitleLocalizations.ToString)
   writer.WriteElementString("Description", Description)
   writer.WriteElementString("DescriptionLocalizations", DescriptionLocalizations.ToString)
   writer.WriteElementString("Image", Image)
   writer.WriteElementString("Locale", Locale)
   writer.WriteElementString("FullLocalization", FullLocalization.ToString())
   writer.WriteElementString("Published", Published.ToString())
   writer.WriteElementString("IncludeImagesInFeed", IncludeImagesInFeed.ToString())
   writer.WriteElementString("IncludeAuthorInFeed", IncludeAuthorInFeed.ToString())
   writer.WriteElementString("Syndicated", Syndicated.ToString())
   writer.WriteElementString("SyndicationEmail", SyndicationEmail)
   writer.WriteElementString("Copyright", Copyright)
   writer.WriteElementString("MustApproveGhostPosts", MustApproveGhostPosts.ToString())
   writer.WriteElementString("PublishAsOwner", PublishAsOwner.ToString())
   writer.WriteElementString("EnablePingBackSend", EnablePingBackSend.ToString())
   writer.WriteElementString("EnablePingBackReceive", EnablePingBackReceive.ToString())
   writer.WriteElementString("AutoApprovePingBack", AutoApprovePingBack.ToString())
   writer.WriteElementString("EnableTrackBackSend", EnableTrackBackSend.ToString())
   writer.WriteElementString("EnableTrackBackReceive", EnableTrackBackReceive.ToString())
   writer.WriteElementString("AutoApproveTrackBack", AutoApproveTrackBack.ToString())
   writer.WriteElementString("Username", Username)
   writer.WriteElementString("Email", Email)
   writer.WriteStartElement("Posts")
   Dim page As Integer = 0
   Dim totalRecords As Integer = 1
   Do While page * 10 < totalRecords
    For Each p As Posts.PostInfo In Posts.PostsController.GetPostsByBlog(ModuleID, BlogID, "", -1, page, 20, "PUBLISHEDONDATE DESC", totalRecords).Values
     p.WriteXml(writer)
    Next
    page += 1
   Loop
   writer.WriteEndElement() ' Posts
   writer.WriteStartElement("Files")
   ' pack files
   Dim postDir As String = GetBlogDirectoryMapPath(BlogID)
   If IO.Directory.Exists(postDir) Then
    For Each f As String In IO.Directory.GetFiles(postDir)
     Dim fileName As String = IO.Path.GetFileName(f)
     writer.WriteElementString("File", fileName)
    Next
   End If
   writer.WriteEndElement() ' Files
   writer.WriteEndElement() ' Blog
  End Sub
#End Region

 End Class
End Namespace


