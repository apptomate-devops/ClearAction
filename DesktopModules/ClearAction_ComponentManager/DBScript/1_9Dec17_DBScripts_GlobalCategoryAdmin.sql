



-- =============================================
-- Author:	Kusum Das
-- Create date: 8-Dec-2017
-- Description:	To alter the column size of 
-- =============================================

ALTER TABLE [dbo].[ClearAction_GlobalCategory]  ALTER COLUMN [CategoryName] NVARCHAR(500)


GO


-- =============================================
-- Author:	Kusum Das
-- Create date: 8-Dec-2017
-- Description:	To get the list of all the Global Categories
-- =============================================


IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'CA_ListAllCategories' and type = N'P')
BEGIN
    DROP PROCEDURE dbo.CA_ListAllCategories
END
GO

CREATE PROCEDURE [dbo].[CA_ListAllCategories] 
AS
BEGIN
SELECT * FROM ClearAction_GlobalCategory 
END

GO



/***************   DELETE CATEGORY   ***************************************/


GO


-- =============================================
-- Author:	Kusum Das
-- Create date: 8-Dec-2017
-- Description:	To delete the category from the Global Categories
-- =============================================


IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'CA_DeleteCategory' and type = N'P')
BEGIN
    DROP PROCEDURE dbo.CA_DeleteCategory
END
GO

CREATE PROCEDURE [dbo].CA_DeleteCategory 
 @CategoryID int
AS
BEGIN
--DELETE FROM ClearAction_GlobalCategory  WHERE CategoryId = @CategoryID

update ClearAction_GlobalCategory
set IsActive = 0
 WHERE CategoryId = @CategoryID


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
@IsDeleted bit

AS
BEGIN

IF  @CategoryID = 0  
BEGIN

INSERT INTO [dbo].[ClearAction_GlobalCategory]
           ([CategoryName]
           ,[CreatedBy]
           ,[CreatedOnDate]
           ,[IsActive]
           )
     VALUES
           (@CategoryName
            ,1
			,GETDATE()
			,@IsActive
			)
END
ELSE
BEGIN
		UPDATE 
			[dbo].[ClearAction_GlobalCategory]
		SET 
			[CategoryName] = @CategoryName,
			[IsActive] = @IsActive
			--,[IsDeleted] = @IsDeleted
		WHERE CategoryId=@CategoryID
END

END

GO
