/*************
-- Store File STack URL  Aditional column on tables
-- AJIT dated : 06/12/2017

*/

ALTER TABLE dbo.activeforums_Content
Add EditedAuthorId int


GO


ALTER TABLE dbo.activeforums_Content
Add EditedAuthorName nvarchar(200)


GO

/*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Ajit : FileStack url save to table
-- Dated : 07/12/2017
*/ALTER PROCEDURE [dbo].[activeforums_Topics_Save]    
@PortalId int,    
@TopicId int,    
@ViewCount int,    
@ReplyCount int,    
@IsLocked bit,    
@IsPinned bit,    
@TopicIcon nvarchar(25),    
@StatusId int,    
@IsApproved bit,    
@IsDeleted bit,    
@IsAnnounce bit,    
@IsArchived bit,    
@AnnounceStart datetime,    
@AnnounceEnd datetime,    
@Subject nvarchar(255),    
@Body ntext,    
@Summary ntext,    
@DateCreated datetime,    
@DateUpdated datetime,    
@AuthorId int,    
@AuthorName nvarchar(150),    
@IPAddress nvarchar(50),    
@TopicType int,    
@Priority int,    
@URL nvarchar(1000),    
@TopicData nvarchar(max)  ,  
@CategoryID int  
AS    
DECLARE @ContentId int    
DECLARE @ForumId int    
DECLARE @ForumGroupId int    
DECLARE @ModuleId int    
SET @ForumId = -1    
SET @ModuleId = -1    
DECLARE @ApprovedStatus bit    
SET @ApprovedStatus = @IsApproved    
DECLARE @currURL nvarchar(1000)    
IF @URL <> '' AND @TopicId>0    
BEGIN    
 SET @ForumId = (SELECT ForumId FROM dbo.activeforums_ForumTopics WHERE TopicId = @TopicId)    
 SET @ModuleId = (SELECT ModuleId FROM dbo.activeforums_Forums WHERE ForumId= @ForumId)    
 SET @ForumGroupId = (SELECT ForumGroupId FROM dbo.activeforums_Forums WHERE ForumId= @ForumId)    
 SET @currURL = dbo.fn_activeforums_GetURL(@ModuleId,@ForumGroupId,@ForumId,@TopicId,-1,-1)    
 IF @currURL <> ''    
  BEGIN    
   DECLARE @newURL nvarchar(1000)    
   SET @newURL = dbo.fn_activeforums_GetURL(@ModuleID,@ForumGroupId, @ForumId,-1,-1,-1) + @URL + '/'    
   IF LTRIM(RTRIM(LOWER(@newURL))) <> LTRIM(RTRIM(LOWER(@currURL)))     
    BEGIN    
     exec dbo.activeforums_URL_Archive @PortalId,@ForumGroupId, @ForumId, @TopicId, @currURL    
    END    
  END    
END    
IF EXISTS(SELECT ContentId FROM dbo.activeforums_Topics WHERE TopicId = @TopicId)    
 BEGIN    
  SELECT @ApprovedStatus = IsApproved, @ContentId = ContentId FROM dbo.activeforums_Topics WHERE TopicId = @TopicId    
    
  BEGIN    
   UPDATE dbo.activeforums_Content    
    SET Subject = @Subject,    
     Body = @Body,    
     Summary = @Summary,    
     DateCreated = @DateCreated,    
     DateUpdated = @DateUpdated,    
     AuthorId = @AuthorId,    
     AuthorName = @AuthorName,    
     IsDeleted = @IsDeleted  ,
	 EditedAuthorId=@AuthorId,
	  EditedAuthorName=@AuthorName

    WHERE ContentId = @ContentId    
   UPDATE dbo.activeforums_Topics    
    SET ViewCount = @ViewCount,    
     ReplyCount = @ReplyCount,    
     IsLocked = @IsLocked,    
     IsPinned = @IsPinned,    
     TopicIcon = @TopicIcon,    
     StatusId = @StatusId,    
     IsApproved = @IsApproved,    
     IsDeleted = @IsDeleted,    
     IsAnnounce = @IsAnnounce,    
     IsArchived = @IsArchived,    
     AnnounceStart = @AnnounceStart,    
     AnnounceEnd = @AnnounceEnd,    
     TopicType = @TopicType,    
     Priority = @Priority,    
     URL = @URL,    
     TopicData = @TopicData  ,  
  CategoryId=@CategoryID  
    WHERE TopicId = @TopicId     
  END    
 END    
ELSE    
    
BEGIN    
 BEGIN    
  INSERT INTO dbo.activeforums_Content    
   (Subject, Body, Summary, DateCreated, DateUpdated, AuthorId, AuthorName, IsDeleted, IPAddress)    
   VALUES    
   (@Subject, @Body, @Summary, @DateCreated, @DateUpdated, @AuthorId, @AuthorName, @IsDeleted, @IPAddress)    
  SET @ContentId = SCOPE_IDENTITY()    
 END    
 BEGIN    
  INSERT INTO dbo.activeforums_Topics    
   (ContentId, ViewCount, ReplyCount, IsLocked, IsPinned, TopicIcon, StatusId, IsApproved, IsDeleted, IsAnnounce, IsArchived, TopicType, AnnounceStart, AnnounceEnd, Priority, URL, TopicData,CategoryID)    
   VALUES    
   (@ContentId, @ViewCount, @ReplyCount, @IsLocked, @IsPinned, @TopicIcon, @StatusId, @IsApproved, @IsDeleted, @IsAnnounce, @IsArchived, @TopicType, @AnnounceStart, @AnnounceEnd, @Priority, @URL, @TopicData,@CategoryID)    
  SET @TopicId = SCOPE_IDENTITY()    
      
 END    
    
END    
BEGIN    
IF @IsApproved = 1 And @AuthorId > 0     
 BEGIN    
  UPDATE dbo.activeforums_UserProfiles     
   SET DateLastPost = GetDate()    
   WHERE UserId = @AuthorId AND PortalId = @PortalId    
 END    
END    
SELECT @TopicId    
    
    
-- reset thread order    
IF @ForumId > -1    
 EXEC dbo.activeforums_SaveTopicNextPrev @ForumId  
  
 


 --------------------------------------------------------------


 ALTER  TABLE activeforums_TopicCategoryRelation

 ADD IsGlobalCategory bit

 ----------------------------
 
CREATE TABLE [dbo].[ClearAction_GlobalQuestions](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](500) NULL,
	[CreatedBy] [int] NULL,
	[CreatedOnDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ClearAction_GlobalQuestions] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



-------------------------------------------

alter PROCEDURE [dbo].[activeforums_Topics_Delete]  
@ForumId int,  
@TopicId int,  
@DelBehavior int,  
@UpdateStats bit = 1  
AS  
DECLARE @ContentId int  
SELECT @ContentId = ContentId FROM dbo.activeforums_Topics WHERE TopicId = @TopicId  
BEGIN  
IF @DelBehavior = 1  
 BEGIN  
  UPDATE dbo.activeforums_Content SET IsDeleted = 1 WHERE ContentId = @ContentId OR ContentId IN (Select ContentId FROM dbo.activeforums_Replies WHERE TopicId = @TopicId)  
  UPDATE dbo.activeforums_Topics SET IsDeleted = 1 WHERE TopicId = @TopicId  
  UPDATE dbo.activeforums_Replies SET IsDeleted = 1 WHERE TopicId = @TopicId  
 END  
ELSE  
 BEGIN  


 -- DELETE FROM GlobalRelation Category
 DELETE from activeforums_TopicCategoryRelation WHERE  TopicId = @TopicId  
  DELETE FROM dbo.activeforums_ForumTopics WHERE ForumId = @ForumId AND TopicId = @TopicId  
  DELETE FROM dbo.activeforums_Replies WHERE TopicId = @TopicId  
  DELETE FROM dbo.activeforums_Topics WHERE TopicId = @TopicId  
  DELETE FROM dbo.activeforums_Content WHERE ContentId = @ContentId   
  DELETE FROM dbo.activeforums_Content WHERE ContentId IN (Select ContentId FROM dbo.activeforums_Replies WHERE TopicId = @TopicId)    
  DELETE FROM dbo.activeforums_Topics_Tags WHERE TopicId = @TopicId  
  DELETE FROM dbo.activeforums_Topics_Ratings WHERE TopicId = @TopicId  
 END  
END  
exec dbo.activeforums_Forums_LastUpdates @ForumId  
  
  
-- reset thread order  
EXEC dbo.activeforums_SaveTopicNextPrev @ForumId  
  


  GO
  
  -----
  CREATE PROCEDURE [dbo].[activeforums_Topics_Update]      
@TopicId int,      
@Subject nvarchar(255),      
@Body ntext,      
@Summary ntext,      
  
@DateUpdated datetime,      
@AuthorId int,      
@AuthorName nvarchar(150)  
  
  
AS      
DECLARE @ContentId int      
  
SELECT @ContentId = ContentId FROM activeforums_Topics  WHERE  TopicId=TopicId  
  
 UPDATE dbo.activeforums_Content      
    SET Subject = @Subject,      
     Body = @Body,      
     Summary = @Summary,      
    EditedAuthorId=@AuthorId,  
 EditedAuthorName=@AuthorName,  
     DateUpdated = GETDATE()      
WHERE ContentId = @ContentId      
  