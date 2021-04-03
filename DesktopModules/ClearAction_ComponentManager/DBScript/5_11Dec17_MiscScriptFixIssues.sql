
/* INSERT SCRIPT */

IF NOT EXISTS (SELECT 1 FROM [dbo].[ClearAction_ComponentMaster] WHERE [ComponentName] like '%Forum%')
BEGIN
	INSERT INTO [dbo].[ClearAction_ComponentMaster] ([ComponentName]) VALUES ('Forum')
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[ClearAction_ComponentMaster] WHERE [ComponentName] like '%Blog%')
BEGIN
	INSERT INTO [dbo].[ClearAction_ComponentMaster] ([ComponentName]) VALUES ('Blog')
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[ClearAction_ComponentMaster] WHERE [ComponentName] like '%SolveSpace%')
BEGIN
	INSERT INTO [dbo].[ClearAction_ComponentMaster] ([ComponentName]) VALUES ('SolveSpace')
END
GO



-- =============================================
-- Author:		Kusum Das
-- Create date: 30-Nov-2017
-- Description:	To add/update ClearAction GlobalCategory
-- =============================================


IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'CA_UpdateCategory' and type = N'P')
BEGIN
    DROP PROCEDURE dbo.CA_UpdateCategory
END
GO

CREATE PROCEDURE [dbo].CA_UpdateCategory 
@CategoryID  int,
@CategoryName nvarchar(500),
@IsActive bit,
@ComponentID int

AS
BEGIN

IF @ComponentID < 1 
BEGIN
	SET @ComponentID = -1
END

IF  @CategoryID = 0  
BEGIN

INSERT INTO [dbo].[ClearAction_GlobalCategory]
           ([CategoryName]
           ,[CreatedBy]
           ,[CreatedOnDate]
           ,[IsActive]
		   ,[ComponentID]
           )
     VALUES
           (@CategoryName
            ,1
			,GETDATE()
			,@IsActive
			,@ComponentID
			)
END
ELSE
BEGIN
		UPDATE 
			[dbo].[ClearAction_GlobalCategory]
		SET 
			[CategoryName] = @CategoryName,
			[IsActive] = @IsActive,
			[ComponentID] = @ComponentID
		WHERE CategoryId=@CategoryID
END

END

GO


