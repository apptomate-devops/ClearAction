
Imports System.Linq
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Text
Imports DotNetNuke.Modules.Blog.Security

Namespace DAL2.BlogContent
    <TableName("Blog_Blogs")>
    <PrimaryKey("BlogID")>
    Public Class PostContentInfo

        Private m_BlogID As Integer
        Public Property BlogID() As Integer
            Get
                Return m_BlogID
            End Get
            Set
                m_BlogID = Value
            End Set
        End Property

        Private m_ModuleID As Integer
        Public Property ModuleID() As Integer
            Get
                Return m_ModuleID
            End Get
            Set
                m_ModuleID = Value
            End Set
        End Property

        Private m_Title As String
        Public Property Title() As String

            Get
                Return m_Title
            End Get
            Set
                m_Title = Value
            End Set
        End Property

        Private m_Description As String
        Public Property Description() As String

            Get
                Return m_Description
            End Get
            Set
                m_Description = Value
            End Set
        End Property

        Private m_filestackurl As String
        <ReadOnlyColumn>
        Public Property filestackurl() As String

            Get
                Return m_filestackurl
            End Get
            Set
                m_filestackurl = Value
            End Set
        End Property

        Private m_Image As String
        Public Property Image() As String

            Get
                Return m_Image
            End Get
            Set
                m_Image = Value
            End Set
        End Property

        Private m_Locale As String
        Public Property Locale() As String

            Get
                Return m_Locale
            End Get
            Set
                m_Locale = Value
            End Set
        End Property

        Private m_FullLocalization As Boolean
        Public Property FullLocalization() As Boolean

            Get
                Return m_FullLocalization
            End Get
            Set
                m_FullLocalization = Value
            End Set
        End Property

        Private m_Published As Boolean
        Public Property Published() As Boolean

            Get
                Return m_Published
            End Get
            Set
                m_Published = Value
            End Set
        End Property

        Private m_IncludeImagesInFeed As Boolean
        Public Property IncludeImagesInFeed() As Boolean

            Get
                Return m_IncludeImagesInFeed
            End Get
            Set
                m_IncludeImagesInFeed = Value
            End Set
        End Property

        Private m_IncludeAuthorInFeed As Boolean
        Public Property IncludeAuthorInFeed() As Boolean

            Get
                Return m_IncludeAuthorInFeed
            End Get
            Set
                m_IncludeAuthorInFeed = Value
            End Set
        End Property

        Private m_Syndicated As Boolean
        Public Property Syndicated() As Boolean

            Get
                Return m_Syndicated
            End Get
            Set
                m_Syndicated = Value
            End Set
        End Property

        Private m_SyndicationEmail As String
        Public Property SyndicationEmail() As String

            Get
                Return m_SyndicationEmail
            End Get
            Set
                m_SyndicationEmail = Value
            End Set
        End Property

        Private m_Copyright As String
        Public Property Copyright() As String

            Get
                Return m_Copyright
            End Get
            Set
                m_Copyright = Value
            End Set
        End Property

        Private m_MustApproveGhostPosts As Boolean
        Public Property MustApproveGhostPosts() As Boolean

            Get
                Return m_MustApproveGhostPosts
            End Get
            Set
                m_MustApproveGhostPosts = Value
            End Set
        End Property

        Private m_PublishAsOwner As Boolean
        Public Property PublishAsOwner() As Boolean

            Get
                Return m_PublishAsOwner
            End Get
            Set
                m_PublishAsOwner = Value
            End Set
        End Property

        Private m_EnablePingBackSend As Boolean
        Public Property EnablePingBackSend() As Boolean

            Get
                Return m_EnablePingBackSend
            End Get
            Set
                m_EnablePingBackSend = Value
            End Set
        End Property

        Private m_EnablePingBackReceive As Boolean
        Public Property EnablePingBackReceive() As Boolean

            Get
                Return m_EnablePingBackReceive
            End Get
            Set
                m_EnablePingBackReceive = Value
            End Set
        End Property

        Private m_AutoApprovePingBack As Boolean
        Public Property AutoApprovePingBack() As Boolean

            Get
                Return m_AutoApprovePingBack
            End Get
            Set
                m_AutoApprovePingBack = Value
            End Set
        End Property

        Private m_EnableTrackBackSend As Boolean
        Public Property EnableTrackBackSend() As Boolean

            Get
                Return m_EnableTrackBackSend
            End Get
            Set
                m_EnableTrackBackSend = Value
            End Set
        End Property

        Private m_EnableTrackBackReceive As Boolean
        Public Property EnableTrackBackReceive() As Boolean

            Get
                Return m_EnableTrackBackReceive
            End Get
            Set
                m_EnableTrackBackReceive = Value
            End Set
        End Property

        Private m_AutoApproveTrackBack As Boolean
        Public Property AutoApproveTrackBack() As Boolean

            Get
                Return m_AutoApproveTrackBack
            End Get
            Set
                m_AutoApproveTrackBack = Value
            End Set
        End Property

        Private m_OwnerUserId As Integer
        Public Property OwnerUserId() As Integer
            Get
                Return m_OwnerUserId
            End Get
            Set
                m_OwnerUserId = Value
            End Set
        End Property

        Private m_CreatedByUserID As Integer
        Public Property CreatedByUserID() As Integer
            Get
                Return m_CreatedByUserID
            End Get
            Set
                m_CreatedByUserID = Value
            End Set
        End Property

        Private m_CreatedOnDate As DateTime
        Public Property CreatedOnDate() As DateTime
            Get
                Return m_CreatedOnDate
            End Get
            Set
                m_CreatedOnDate = Value
            End Set
        End Property

        Private m_LastModifiedByUserID As Integer
        Public Property LastModifiedByUserID() As Integer
            Get
                Return m_LastModifiedByUserID
            End Get
            Set
                m_LastModifiedByUserID = Value
            End Set
        End Property

        Private m_LastModifiedOnDate As DateTime
        Public Property LastModifiedOnDate() As DateTime
            Get
                Return m_LastModifiedOnDate
            End Get
            Set
                m_LastModifiedOnDate = Value
            End Set
        End Property



        Private m_AuthorInfo As BlogUser
        <ReadOnlyColumn>
        Public ReadOnly Property AuthorUser() As BlogUser
            Get
                Return BlogUser.GetUser(0, CreatedByUserID)
            End Get

        End Property





    End Class
End Namespace

