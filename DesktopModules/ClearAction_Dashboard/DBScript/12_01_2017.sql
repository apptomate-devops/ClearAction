
/****** Object:  StoredProcedure [dbo].[CA_Forum_GetUserForums]    Script Date: 4/12/2017 1:34:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Alter PROCEDURE [dbo].[CA_Forum_GetUserForums]
@UserID int,
@TopN int
AS
BEGIN
SELECT TOP (@TopN)
CASE  
	WHEN (ISNULL(HasSeen,0)=1) THEN 'Completed' 
	WHEN (B.IsAssigned=1 AND ISNULL(HasSeen,0)=0) THEN 'To-Do'
	END AS [Status],
	B.[Subject],B.Body,B.Summary,B.TopicId,B.ContentId,B.AuthorId
FROM
(
	SELECT DISTINCT M.[Subject],M.ContentId,M.TopicId,M.Summary,CAST(M.Body as varchar(1000)) Body, M.AuthorId,
		(CASE WHEN (ISNULL(UC.ItemID,0)>0) THEN 1 ELSE 0 END) IsAssigned,IsNull(HasSeen,0) HasSeen,M.DateCreated
	FROM  (SELECT C.DateCreated,C.AuthorId, C.[Subject],C.ContentID,T.TopicId,T.CategoryID,C.Body,C.Summary FROM dbo.activeforums_Topics T 
			INNER JOIN activeforums_Content C on C.contentID=T.ContentId) AS M 
	INNER JOIN
		(SELECT * FROM dbo.ClearAction_UserComponents WHERE (UserID = @UserID AND ComponentID=1 )) AS UC 
	ON M.ContentId = UC.ItemID
	LEFT OUTER JOIN 
		activeforums_TopicCategoryRelation FC on FC.TopicId=M.TopicId
	GROUP BY 
		M.TopicId,M.ContentId,M.[Subject],cast(M.Body as varchar(1000)),FC.CategoryID, UC.ItemID,M.Summary,HasSeen,M.AuthorId,M.DateCreated
) AS B ORDER BY DateCreated DESC
/* For Pagination   
 SELECT distinct Title,ContentItemId,IsAssigned,(SELECT COUNT(*) from rs) TotalRecords
 FROM rs 
 WHERE ( (@PI=-1) OR (Row BETWEEN (@PI - 1) * @PS+ 1 AND (@PI*@PS) ))
*/
END

GO

/****** Object:  StoredProcedure [dbo].[CA_Blog_GetUserBlogs]    Script Date: 4/12/2017 1:34:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE [dbo].[CA_Blog_GetUserBlogs] 
@UserID int,
@TopN int
AS
BEGIN
SELECT top (@TopN)
CASE  
	WHEN (ISNULL(HasSeen,0)=1) THEN 'Completed' 
	WHEN (B.IsAssigned=1 AND ISNULL(HasSeen,0)=0) THEN 'To-Do'
	END AS [Status],
	B.* 
FROM
(SELECT DISTINCT
	M.Title, 
	M.ContentItemId, 
	M.Summary,
	(CASE WHEN (ISNULL(UC.ItemID,0)>0) THEN 1 ELSE 0 END) IsAssigned,
	IsNull(HasSeen,0) HasSeen,PublishedOnDate
FROM  
	(SELECT * FROM dbo.Blog_Posts WHERE Published=1  ) AS M 
INNER JOIN
        (SELECT *
        FROM dbo.ClearAction_UserComponents
        WHERE (UserID = @UserID AND ComponentID=2
)) AS UC 
ON 
	M.ContentItemId = UC.ItemID
LEFT OUTER JOIN Blog_PostCategoryRelation BC on BC.ContentItemId=M.ContentItemId
GROUP BY 
	M.Title,BC.CategoryID, M.ContentItemId, UC.ItemID,M.Summary,HasSeen,PublishedOnDate
	) as B ORDER BY B.PublishedOnDate DESC
/* For Pagination   
 SELECT distinct Title,ContentItemId,IsAssigned,(SELECT COUNT(*) from rs) TotalRecords
 FROM rs 
 WHERE ( (@PI=-1) OR (Row BETWEEN (@PI - 1) * @PS+ 1 AND (@PI*@PS) ))
*/
END

GO


