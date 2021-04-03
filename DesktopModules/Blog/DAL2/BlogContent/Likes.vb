
Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Web.Caching
Imports DotNetNuke.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

<TableName("Blog_Likes")>
<PrimaryKey("Id", AutoIncrement:=True)>
<Scope("PostId")>
<Cacheable("Blog_Likes", CacheItemPriority.Normal)>
Public Class Likes
    Public Property Id() As Integer
        Get
            Return m_Id
        End Get
        Set
            m_Id = Value
        End Set
    End Property
    Private m_Id As Integer
    Public Property PostId() As Integer
        Get
            Return m_PostId
        End Get
        Set
            m_PostId = Value
        End Set
    End Property
    Private m_PostId As Integer
    Public Property UserId() As Integer
        Get
            Return m_UserId
        End Get
        Set
            m_UserId = Value
        End Set
    End Property
    Private m_UserId As Integer
    Public Property Checked() As Boolean
        Get
            Return m_Checked
        End Get
        Set
            m_Checked = Value
        End Set
    End Property
    Private m_Checked As Boolean

    Private mItemType As String
    Public Property ItemType() As String
        Get
            Return mItemType
        End Get
        Set(value As String)
            mItemType = value
        End Set
    End Property
End Class