/****** Object:  StoredProcedure [dbo].[CA_Forum_GetUserForumsAll]    Script Date: 6/12/2017 4:33:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CA_Forum_GetUserForumsAll]  -- -1,-1,'All',8,1,5
@UserID int,
@CatID int,
@OptionID varchar(10),
@LoggedInUser int,
@PI int,
@PS int
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
	B.[Subject],B.Body,B.Summary,B.TopicId,B.ContentId,B.AuthorId
FROM
(
	SELECT DISTINCT M.[Subject],M.ContentId,M.TopicId,M.Summary,CAST(M.Body as varchar(1000)) Body, M.AuthorId,
		(CASE WHEN (ISNULL(UC.ItemID,0)>0) THEN 1 ELSE 0 END) IsAssigned,IsNull(HasSeen,0) HasSeen,M.DateCreated
	FROM  (SELECT C.DateCreated,C.AuthorId, C.[Subject],C.ContentID,T.TopicId,T.CategoryID,C.Body,C.Summary FROM 
	(SELECT * FROM dbo.activeforums_Topics where IsApproved=1) T 
			INNER JOIN activeforums_Content C on C.contentID=T.ContentId) AS M 
	INNER JOIN
		(SELECT * FROM dbo.ClearAction_UserComponents WHERE (((@UserID=-1) OR (UserID = @UserID ))AND ComponentID=1 )) AS UC 
	ON M.ContentId = UC.ItemID
	LEFT OUTER JOIN 
		(select * from activeforums_TopicCategoryRelation where IsActive=1 )FC on FC.TopicId=M.TopicId
	GROUP BY 
		M.TopicId,M.ContentId,M.[Subject],cast(M.Body as varchar(1000)),FC.CategoryID, UC.ItemID,M.Summary,HasSeen,M.AuthorId,M.DateCreated
	HAVING 
	((@CatID=-1) OR (FC.CategoryId=@CatID))
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
	B.[Subject],B.Body,B.Summary,B.TopicId,B.ContentId,B.AuthorId
FROM
(
	SELECT DISTINCT M.[Subject],M.ContentId,M.TopicId,M.Summary,CAST(M.Body as varchar(1000)) Body, M.AuthorId,
		(CASE WHEN (ISNULL(UC.ItemID,0)>0) THEN 1 ELSE 0 END) IsAssigned,IsNull(HasSeen,0) HasSeen,M.DateCreated
	FROM  (SELECT C.DateCreated,C.AuthorId, C.[Subject],C.ContentID,T.TopicId,T.CategoryID,C.Body,C.Summary FROM 
	(SELECT * FROM dbo.activeforums_Topics where IsApproved=1) T 
			INNER JOIN activeforums_Content C on C.contentID=T.ContentId) AS M 
	LEFT OUTER JOIN
		(SELECT * FROM dbo.ClearAction_UserComponents WHERE (UserID = @LoggedInUser AND ComponentID=1 )) AS UC 
	ON M.ContentId = UC.ItemID
	LEFT OUTER JOIN 
		(SELECT * FROM activeforums_TopicCategoryRelation where IsActive=1 )FC on FC.TopicId=M.TopicId
	GROUP BY 
		M.TopicId,M.ContentId,M.[Subject],cast(M.Body as varchar(1000)),FC.CategoryID, UC.ItemID,M.Summary,HasSeen,M.AuthorId,M.DateCreated
	HAVING 
	((@CatID=-1) OR (FC.CategoryId=@CatID))
) AS B 
) tblFinal
WHERE ((@OptionID='All') or ([Status]=@OptionID) )  
end
END
