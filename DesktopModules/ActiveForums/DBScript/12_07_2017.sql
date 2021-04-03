/*************
-- Store File STack URL  Aditional column on tables
-- AJIT dated : 06/12/2017

*/

ALTER TABLE dbo.activeforums_Attachments
Add filestackurl nvarchar(500)


GO

/*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Ajit : FileStack url save to table
-- Dated : 07/12/2017
*/
ALTER PROCEDURE [dbo].[activeforums_Attachments_Save]
@ContentId int,
@UserID int,
@FileName nvarchar(255),
@ContentType nvarchar(255),
@FileSize int,
@FileID int = null,
@filestackurl nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @AttachID int

	INSERT INTO dbo.activeforums_Attachments (ContentId, UserID, [Filename], DateAdded, ContentType, FileSize, FileID,filestackurl)
	VALUES (@ContentId, @UserID, @Filename, GetDate(), @ContentType, @FileSize, @FileID,@filestackurl)

	SET @AttachID = SCOPE_IDENTITY()

	SELECT @AttachID
END

--------

/* Script added and modified by Sachin */

/****** Object:  StoredProcedure [dbo].[CA_Forum_GetUserForumsAll]    Script Date: 7/12/2017 4:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CA_Forum_GetUserForumsAll]  -- -1,-1,'All',8,''
@UserID int,
@CatID int,
@OptionID varchar(10),
@LoggedInUser int,
@SearchKey varchar(200)
AS
BEGIN
IF (@UserID>0)
BEGIN
SELECT * FROM (
SELECT 
CASE  
	WHEN (ISNULL(HasSeen,0)=1) THEN 'Completed' 
	WHEN (B.IsAssigned=1 AND ISNULL(HasSeen,0)=0) THEN 'To-Do'
	END AS [Status],
	B.[Subject],B.Body,B.Summary,B.TopicId,B.ContentId,B.AuthorId,B.IsSelfAssigned
FROM
(
	SELECT DISTINCT M.[Subject],M.ContentId,M.TopicId,M.Summary,CAST(M.Body as varchar(1000)) Body, M.AuthorId,
		(CASE WHEN (ISNULL(UC.ItemID,0)>0) THEN 1 ELSE 0 END) IsAssigned,
		CASE WHEN (IsNull(UC.CreatedBy,-1)=@UserID AND IsNull(UC.ItemID,0)>0) THEN 1 ELSE 0 END IsSelfAssigned,
		IsNull(HasSeen,0) HasSeen,M.DateCreated
	FROM  (SELECT C.DateCreated,C.AuthorId, C.[Subject],C.ContentID,T.TopicId,T.CategoryID,C.Body,C.Summary FROM 
	(SELECT * FROM dbo.activeforums_Topics where IsApproved=1) T 
			INNER JOIN activeforums_Content C on C.contentID=T.ContentId) AS M 
	INNER JOIN
		(SELECT * FROM dbo.ClearAction_UserComponents WHERE (((@UserID=-1) OR (UserID = @UserID ))AND ComponentID=1 )) AS UC 
	ON M.ContentId = UC.ItemID
	LEFT OUTER JOIN 
		(SELECT * FROM activeforums_TopicCategoryRelation WHERE IsActive=1 )FC on FC.TopicId=M.TopicId
	GROUP BY 
		M.TopicId,M.ContentId,M.[Subject],cast(M.Body as varchar(1000)),FC.CategoryID, UC.ItemID,M.Summary,HasSeen,M.AuthorId,M.DateCreated,UC.CreatedBy
	HAVING 
	((@CatID=-1) OR (FC.CategoryId=@CatID))
	AND 
	((@SearchKey='') OR (cast(M.Body as varchar(1000)) LIKE '%'+@SearchKey+'%' OR M.Subject LIKE '%'+@SearchKey+'%' OR M.Summary LIKE '%'+@SearchKey+'%'))
) AS B 
) tblFinal
WHERE ((@OptionID='All') or ([Status]=@OptionID) ) 
END
ELSE
BEGIN
SELECT * FROM (
SELECT 
CASE  
	WHEN (ISNULL(HasSeen,0)=1) THEN 'Completed' 
	WHEN (B.IsAssigned=1 AND ISNULL(HasSeen,0)=0) THEN 'To-Do'
	END AS [Status],
	B.[Subject],B.Body,B.Summary,B.TopicId,B.ContentId,B.AuthorId,B.IsSelfAssigned
FROM
(
	SELECT DISTINCT M.[Subject],M.ContentId,M.TopicId,M.Summary,CAST(M.Body as varchar(1000)) Body, M.AuthorId,
		(CASE WHEN (ISNULL(UC.ItemID,0)>0) THEN 1 ELSE 0 END) IsAssigned,
		CASE WHEN (IsNull(UC.CreatedBy,-1)=@LoggedInUser AND IsNull(UC.ItemID,0)>0) THEN 1 ELSE 0 END IsSelfAssigned,
		IsNull(HasSeen,0) HasSeen,M.DateCreated
	FROM  (SELECT C.DateCreated,C.AuthorId, C.[Subject],C.ContentID,T.TopicId,T.CategoryID,C.Body,C.Summary FROM 
	(SELECT * FROM dbo.activeforums_Topics where IsApproved=1) T 
			INNER JOIN activeforums_Content C on C.contentID=T.ContentId) AS M 
	LEFT OUTER JOIN
		(SELECT * FROM dbo.ClearAction_UserComponents WHERE (UserID = @LoggedInUser AND ComponentID=1 )) AS UC 
	ON M.ContentId = UC.ItemID
	LEFT OUTER JOIN 
		(SELECT * FROM activeforums_TopicCategoryRelation where IsActive=1 )FC on FC.TopicId=M.TopicId
	GROUP BY 
		M.TopicId,M.ContentId,M.[Subject],cast(M.Body as varchar(1000)),FC.CategoryID, 
		UC.ItemID,M.Summary,HasSeen,M.AuthorId,M.DateCreated,UC.CreatedBy
	HAVING 
	((@CatID=-1) OR (FC.CategoryId=@CatID))
) AS B 
) tblFinal
WHERE ((@OptionID='All') or ([Status]=@OptionID) )  
end
END



/*
Ajit
Sp to get Active member on forum by disucssion
*/
CREATE PROCEDURE [dbo].[activeforums_GetActiveTopicMember] --19

@TopicID int


AS
SELECT DISTINCT U.UserID as AuthorId, U.Username, U.FirstName, U.LastName, U.Email, U.DisplayName 
FROM	dbo.activeforums_Content AS P INNER JOIN
        dbo.Users AS U ON P.AuthorId = U.UserID
WHERE  

     
   P.ContentId in 
   (
		   (
		   SELECT Distinct ContentId FROM activeforums_Content
		   WHERE
		   (
		  -- Contentid in 
		 --  (
			--	SELECT ContentId FROM activeforums_Topics where TopicId=@TopicID

		--	)
		--   OR
		   Contentid in 
		   (
				SELECT ContentId FROM activeforums_Replies where TopicId=@TopicID

			)		   
			
		   
		  )
		  )

   
   )

   
   
   
