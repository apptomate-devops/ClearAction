Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace DAL2.BlogContent

    <TableName("ClearAction_UserComponents")>
    <PrimaryKey("ID")>
    Public Class UserComponentsInfo

        Private m_ID As Integer
        Public Property ID() As Integer
            Get
                Return m_ID
            End Get
            Set
                m_ID = Value
            End Set
        End Property

        Private m_ComponentID As Integer
        Public Property ComponentID() As Integer
            Get
                Return m_ComponentID
            End Get
            Set
                m_ComponentID = Value
            End Set
        End Property


        Private m_UserID As Integer

        Public Property UserID() As Integer

            Get
                Return m_UserID
            End Get
            Set
                m_UserID = Value
            End Set
        End Property

        Private m_ItemID As Integer

        Public Property ItemID() As Integer
            Get
                Return m_ItemID
            End Get
            Set
                m_ItemID = Value
            End Set
        End Property

        Private m_CreatedOn As DateTime

        Public Property CreatedOn() As DateTime
            Get
                Return m_CreatedOn
            End Get
            Set
                m_CreatedOn = Value
            End Set
        End Property

        Private m_HasSeen As Boolean

        Public Property HasSeen() As Boolean
            Get
                Return m_HasSeen
            End Get
            Set
                m_HasSeen = Value
            End Set
        End Property







    End Class
End Namespace
