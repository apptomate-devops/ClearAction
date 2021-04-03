Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace DAL2.BlogContent


    <TableName("ClearAction_GlobalCategory")>
    <PrimaryKey("CategoryId")>
    Public Class BlogGlobalCategory


        Public Property CategoryId() As Integer
            Get
                Return m_CategoryId
            End Get
            Set
                m_CategoryId = Value
            End Set
        End Property
        Private m_CategoryId As Integer

        Public Property CategoryName() As String
            Get
                Return m_CategoryName
            End Get
            Set
                m_CategoryName = Value
            End Set
        End Property
        Private m_CategoryName As String

        Public Property CreatedBy() As Integer
            Get
                Return m_CreatedBy
            End Get
            Set
                m_CreatedBy = Value
            End Set
        End Property
        Private m_CreatedBy As Integer

        Public Property CreatedOnDate() As DateTime
            Get
                Return m_CreatedOnDate
            End Get
            Set
                m_CreatedOnDate = Value
            End Set
        End Property
        Private m_CreatedOnDate As DateTime

        Public Property IsActive() As Boolean
            Get
                Return m_IsActive
            End Get
            Set
                m_IsActive = Value
            End Set
        End Property
        Private m_IsActive As Boolean



        Public Property ComponentId() As Integer
            Get
                Return m_ComponentID
            End Get
            Set
                m_ComponentID = Value
            End Set
        End Property
        Private m_ComponentID As Integer


        Public Property OptionOrder() As Integer
            Get
                Return m_OptionOrder
            End Get
            Set
                m_OptionOrder = Value
            End Set
        End Property
        Private m_OptionOrder As Integer



        Public Property DisplayName() As String
            Get
                Return m_DisplayName
            End Get
            Set
                m_DisplayName = Value
            End Set
        End Property
        Private m_DisplayName As String


    End Class
End Namespace