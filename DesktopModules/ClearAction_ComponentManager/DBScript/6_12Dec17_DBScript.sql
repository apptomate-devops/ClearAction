


-- =============================================
-- Author:	Kusum Das
-- Create date: 9-Dec-2017
-- Description:	To get the list of all the Global Categories
-- Updated date: 12-Dec-2017 - for paging
-- =============================================

--[dbo].[CA_ListAllCategories]  1 , 15


IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'CA_ListAllCategories' and type = N'P')
BEGIN
    DROP PROCEDURE dbo.CA_ListAllCategories
END
GO

CREATE PROCEDURE [dbo].[CA_ListAllCategories] 
@PI int, -- page number
@PS int -- Page size 
AS
BEGIN

with rs as (
SELECT ROW_NUMBER() OVER (ORDER BY subTbl.[CategoryId] ) AS Row,* 
FROM (

SELECT 
       GC.CategoryId
      ,GC.CategoryName
      ,GC.CreatedBy
      ,GC.CreatedOnDate
      ,GC.IsActive
	  , ISNULL(CM.ComponentID,-1) AS ComponentID
	  ,ISNULL(CM.ComponentName, 'Global Gategory') AS ComponentName
	  
FROM ClearAction_GlobalCategory GC LEFT JOIN ClearAction_ComponentMaster CM ON GC.ComponentID = CM.ComponentID
) as subTbl
)


/* For Pagination */  
 SELECT DISTINCT 
  *
 ,(SELECT COUNT(*) from rs) TotalRecords
 FROM rs 
 WHERE ( (@PI=-1) OR (Row BETWEEN (@PI - 1) * @PS+ 1 AND (@PI*@PS) ))


END

GO



