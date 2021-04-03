Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace DAL2.BlogContent


    <TableName("Blog_PostCategoryRelation")>
    <PrimaryKey("PostCategoryId")>
    Public Class PostCategoryRelationInfo

        Private m_PostCategoryId As Integer
        Public Property PostCategoryId() As Integer
            Get
                Return m_PostCategoryId
            End Get
            Set
                m_PostCategoryId = Value
            End Set
        End Property
        Private m_CategoryId As Integer
        Public Property CategoryId() As Integer
            Get
                Return m_CategoryId
            End Get
            Set
                m_CategoryId = Value
            End Set
        End Property

        Private m_ContentItemId As Integer
        Public Property ContentItemId() As Integer
            Get
                Return m_ContentItemId
            End Get
            Set
                m_ContentItemId = Value
            End Set
        End Property

        Private m_IsActive As Boolean
        Public Property IsActive() As Boolean
            Get
                Return m_IsActive
            End Get
            Set
                m_IsActive = Value
            End Set
        End Property


        Private m_BlogGlobalCategory As BlogGlobalCategory
        <ReadOnlyColumn>
        Public ReadOnly Property GetCategoryInfo As BlogGlobalCategory
            Get
                If m_CategoryId > 0 Then
                    m_BlogGlobalCategory = New BlogContentController().GetGlobalCategory(m_CategoryId).Where(Function(x) x.CategoryId = CategoryId).SingleOrDefault
                End If

                Return Nothing
            End Get

        End Property

    End Class
End Namespace