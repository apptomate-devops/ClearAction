Public Class LikesDTO


    Public CommentID As Integer
    Public UserID As Integer
    Public LikesCount As String
    Public UserLiked As String
    Public ImageName As String
    Public UnLikeCount As String


End Class



Public Class FileStackRefernceDTO


    Public FileId As Long
    Public ContentItemID As Integer
    Public filestackurl As String
    'Public UserLiked As Boolean
    Public FileName As String
    Public UploadedBy As Integer

End Class


Public Class DTOResponse

    Public Property UserID As Integer

    Public Property ResponseText As String

    Public Property QuestionID As Integer
End Class
