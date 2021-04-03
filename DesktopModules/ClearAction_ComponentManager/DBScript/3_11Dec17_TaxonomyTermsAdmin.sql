
-- =============================================
-- Author:	Kusum Das
-- Create date: 11-Dec-2017
-- Description:	To get the list of all the taxonomy terms having parentid
-- =============================================


IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'CA_ListTaxonomyTerms' and type = N'P')
BEGIN
    DROP PROCEDURE [dbo].[CA_ListTaxonomyTerms] 
END
GO

CREATE PROCEDURE [dbo].[CA_ListTaxonomyTerms]
@PI int, -- page number
	@PS int -- Page size 
AS
BEGIN
with rs as (
SELECT ROW_NUMBER() OVER (ORDER BY subTbl.[TermID] ) AS Row,* 
FROM (SELECT distinct  t.[TermID]
		, t.[ParentTermID]
		, t.Name
		,m.Name as 'ParentName'
		,t.[Description]
		,t.[VocabularyID]
	    ,t.[Weight]
        ,t.[TermLeft]
         ,t.[TermRight]
      
FROM  [dbo].[Taxonomy_Terms]  t
INNER JOIN  [dbo].[Taxonomy_Terms] m ON m.TermID = t.ParentTermID
/*LEFT JOIN  [dbo].[Taxonomy_Terms] m ON m.TermID = t.ParentTermID WHERE t.ParentTermID is not  null*/
) as subTbl
)
/* For Pagination */  
 SELECT DISTINCT 
  [TermID], [ParentTermID],Name,ParentName,[Description],[VocabularyID],[Weight] ,[TermLeft],[TermRight]
 ,(SELECT COUNT(*) from rs) TotalRecords
 FROM rs 
 WHERE ( (@PI=-1) OR (Row BETWEEN (@PI - 1) * @PS+ 1 AND (@PI*@PS) ))


END

GO


/***************   DELETE CATEGORY   ***************************************/


GO

-- =============================================
-- Author:	Kusum Das
-- Create date: 11-Dec-2017
-- Description:	To delete the taxonomy terms
-- =============================================

IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'CA_DeleteTaxonomyTerm' and type = N'P')
BEGIN
    DROP PROCEDURE [dbo].[CA_DeleteTaxonomyTerm] 
END
GO

CREATE PROCEDURE [dbo].[CA_DeleteTaxonomyTerm] 
 @TermID int
AS
BEGIN

DELETE FROM [dbo].[Taxonomy_Terms] 
	WHERE TermID = @TermID 
	AND  ParentTermID is not null
END

GO

