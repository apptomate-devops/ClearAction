Imports System.Xml.Serialization

Namespace BlogML.Xml
    <Serializable>
    Public NotInheritable Class BlogMLAttachements
        'Inherits BlogMLAttachement

        <XmlAttribute("url")>
        Public Property url As String

        <XmlAttribute("embedded")>
        Public Property embedded As Boolean = False


        <XmlAttribute("mime-type")>
        Public Property mimetype As String

        <XmlAttribute("Size")>
        Public Property Size As Integer



        Private m_ContentType As ContentTypes = ContentTypes.Base64
        Private m_Text As Byte
        ' Encoded Text
        <XmlElement>
        Public Property Attachement As Byte
            Get
                Return m_Text
            End Get
            Set(value As Byte)
                m_Text = value
            End Set
        End Property
    End Class
End Namespace
