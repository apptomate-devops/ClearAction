
/**************************************************************
Name  : Kusum Das
Date : 30-Nov-17
Purpose: To add new column "ComponentID" in  "ClearAction_GlobalCategory" table

**************************************************************/

IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'ComponentID'
          AND Object_ID = Object_ID(N'dbo.ClearAction_GlobalCategory'))
BEGIN
		ALTER TABLE ClearAction_GlobalCategory  ADD ComponentID int NULL
		ALTER TABLE ClearAction_GlobalCategory 	ADD CONSTRAINT fk_GlobCat_CompID FOREIGN KEY(ComponentID) REFERENCES ClearAction_ComponentMaster(ComponentID)
 END


GO

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

/*********************************************************************************/


-- =============================================
-- Author:	Kusum Das
-- Create date: 9-Dec-2017
-- Description:	To get the list of all the Component Master
-- =============================================


IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'CA_ListComponentMaster' and type = N'P')
BEGIN
    DROP PROCEDURE dbo.CA_ListComponentMaster
END
GO

CREATE PROCEDURE [dbo].[CA_ListComponentMaster] 
AS
BEGIN

SELECT 
	  CM.ComponentID 
	  ,CM.ComponentName
	  
FROM ClearAction_ComponentMaster CM 

END

GO



-- =============================================
-- Author:	Kusum Das
-- Create date: 9-Dec-2017
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

SELECT 
       GC.CategoryId
      ,GC.CategoryName
      ,GC.CreatedBy
      ,GC.CreatedOnDate
      ,GC.IsActive
	  , ISNULL(CM.ComponentID,-1) AS ComponentID
	  ,ISNULL(CM.ComponentName, 'Global Gategory') AS ComponentName
	  
FROM ClearAction_GlobalCategory GC LEFT JOIN ClearAction_ComponentMaster CM ON GC.ComponentID = CM.ComponentID

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

IF @ComponentID < 1 THEN
BEGIN
	@ComponentID = null
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



/*
select * from sys.objects where name like '%component%'

select  * from ClearAction_ComponentMaster

select * from  ClearAction_GlobalCategory


SELECT  [CategoryId]
      ,[CategoryName]
      ,[CreatedBy]
      ,[CreatedOnDate]
      ,[IsActive]
      ,[IsDeleted]
	  , CASE WHEN [GC.ComponentID] = null then -1 ELSE GC.[GC.ComponentID] END AS ComponentID
	  --,[CM.ComponentName]

FROM ClearAction_GlobalCategory GC LEFT OUTER JOIN ClearAction_ComponentMaster CM ON GC.ComponentID = CM.ComponentID

*/