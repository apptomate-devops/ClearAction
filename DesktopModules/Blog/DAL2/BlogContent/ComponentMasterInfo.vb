Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace DAL2.BlogContent

    <TableName("ClearAction_ComponentMaster")>
    <PrimaryKey("ComponentID")>
    Public Class ComponentMasterInfo

        Private m_ComponentID As Integer
        Public Property ComponentID() As Integer
            Get
                Return m_ComponentID
            End Get
            Set
                m_ComponentID = Value
            End Set
        End Property
        Private m_ComponentName As String

        Public Property ComponentName() As String
            Get
                Return m_ComponentName
            End Get
            Set
                m_ComponentName = Value
            End Set
        End Property


    End Class
End Namespace

