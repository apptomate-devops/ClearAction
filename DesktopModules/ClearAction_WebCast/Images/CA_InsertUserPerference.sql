USE [ClearActionDBProd]
GO
/****** Object:  StoredProcedure [dbo].[CA_InsertUserPerference]    Script Date: 1/25/2018 2:27:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER PROCEDURE [dbo].[CA_InsertUserPerference] --1    
(@UserId int)     
AS    
BEGIN    

-- Update blog perfrence     
CREATE TABLE #tmp    
(ContentItemid int)    
    
Declare @blogcount int

Select @blogcount = 15 - count(*) From [ClearAction_UserComponents] where [ComponentID] = 2 and userid = @UserID

INSERT INTO #tmp    
(ContentItemid)    
SELECT TOP (@blogcount) ContentItemid FROM    
[dbo].ClearAction_GetTagsBlog(@UserId)    
where ContentItemid not in (select [ItemID] from [ClearAction_UserComponents] where [ComponentID] = 2 and userid = @UserID)
    
Declare @Id int    
    
While (Select Count(*) From #tmp) > 0    
Begin    
    
    Select Top 1 @Id = ContentItemid From #tmp    
        
   EXEC CA_AssignItemToUser 2,@Id,@UserID,@UserID    
    
    
    Delete #tmp Where ContentItemid = @Id    
    
End    
    
-- Update Active Forum    
         
CREATE TABLE #tmpForum    
(TopicId int)    

Declare @forumcount int

Select @forumcount = 15 - count(*) From [ClearAction_UserComponents] where [ComponentID] = 1 and userid = @UserID
    
INSERT INTO #tmpForum    
(TopicId)    
SELECT TOP (@forumcount) ContentId     FROM    
[dbo].ClearAction_GetTagsForum(@UserId)    
where ContentId not in (select [ItemID] from [ClearAction_UserComponents] where [ComponentID] = 1 and userid = @UserID)    
    
    
Declare @TopicId int        
    
While (Select Count(*) From #tmpForum) > 0    
Begin    
    
    Select Top 1 @TopicId = TopicId From #tmpForum    
    
       
   EXEC CA_AssignItemToUser 1,@TopicId,@UserID,@UserID    
    
    
    Delete #tmpForum Where TopicId = @TopicId    
    
End    
    
-- Update solvespace perfrence     
         
CREATE TABLE #tmpSS  
(SolvespaceId int)    

Declare @sscount int

Select @sscount = 15 - count(*) From [ClearAction_UserComponents] where [ComponentID] = 3 and userid = @UserID
    
INSERT INTO #tmpSS
(SolvespaceId)    
SELECT TOP (@sscount) SolveSpaceID     FROM    
[dbo].[ClearAction_GetTagsSolvespace](@UserId)    
where SolveSpaceID not in (select [ItemID] from [ClearAction_UserComponents] where [ComponentID] = 3 and userid = @UserID)    
    
    
Declare @SSId int        
    
While (Select Count(*) From #tmpSS) > 0    
Begin    
    
    Select Top 1 @SSId = SolvespaceId From #tmpSS   
    
       
   EXEC CA_AssignItemToUser 3,@SSId,@UserID,@UserID    
    
    
    Delete #tmpSS Where SolvespaceId = @SSId    
    
End    
End