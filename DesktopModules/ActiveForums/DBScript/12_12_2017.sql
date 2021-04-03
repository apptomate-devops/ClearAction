
/*
Anuj , Count and show response
*/

ALTER PROCEDURE [dbo].[activeforums_GetActiveTopicMember]   

@TopicID int


AS
SELECT  COUNT(*) as ResponseCount, U.UserID as AuthorId, U.Username, U.FirstName, U.LastName, U.Email, U.DisplayName 
FROM dbo.activeforums_Content AS P INNER JOIN
        dbo.Users AS U ON P.AuthorId = U.UserID
WHERE  

     
   P.ContentId in 
   (
     (
     SELECT  ContentId FROM activeforums_Content
     WHERE
     (
     Contentid in 
      (
    SELECT ContentId FROM activeforums_Topics where TopicId=@TopicID

    )
      OR
     Contentid in 
     (
    SELECT ContentId FROM activeforums_Replies where TopicId=@TopicID

   )     
   
     
    )
    )

   
   )
   group by 
   u.Userid,U.Username, U.FirstName, U.LastName, U.Email, U.DisplayName