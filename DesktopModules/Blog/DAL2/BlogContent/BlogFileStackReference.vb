
Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Web.Caching
Imports DotNetNuke.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

<TableName("Blog_FileStackRefernce")>
<PrimaryKey("FileId", AutoIncrement:=True)>
<Scope("FileId")>
<Cacheable("Blog_FileReference", CacheItemPriority.Normal)>
Public Class FileStackRefernceInfo
    Public Property FileId() As Long
        Get
            Return m_FileId
        End Get
        Set
            m_FileId = Value
        End Set
    End Property


    Private m_FileId As Long
    Public Property UploadedBy() As Int32
        Get
            Return m_UploadedBy
        End Get
        Set
            m_UploadedBy = Value
        End Set
    End Property
    Private m_UploadedBy As Int32
    Public Property ContentItemID() As Integer
        Get
            Return m_ContentItemID
        End Get
        Set
            m_ContentItemID = Value
        End Set
    End Property
    Private m_ContentItemID As Integer
    Public Property FileName() As String
        Get
            Return m_FileName
        End Get
        Set
            m_FileName = Value
        End Set
    End Property
    Private m_FileName As String
    Public Property filestackurl() As String
        Get
            Return m_filestackurl
        End Get
        Set
            m_filestackurl = Value
        End Set
    End Property
    Private m_filestackurl As String
End Class