


IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'InsertTermId' and type = N'P')
BEGIN
    DROP PROCEDURE [dbo].[InsertTermId] 
END
GO

CREATE procedure [dbo].[InsertTermId] (@ParentTermId int, @Term nvarchar(250),@TermId int OUTPUT )
AS
  BEGIN
  DECLARE @OutputTbl TABLE (ID INT)

  INSERT INTO [dbo].[Taxonomy_Terms]
           ([VocabularyID]
           ,[ParentTermID]
           ,[Name]
           ,[Description]
           ,[Weight]
           ,[TermLeft]
           ,[TermRight]
           ,[CreatedByUserID]
           ,[CreatedOnDate]
           ,[LastModifiedByUserID]
           ,[LastModifiedOnDate])
     OUTPUT Inserted.TermID into @OutputTbl(ID)
     VALUES
           (1,@ParentTermId,@Term,'',0,0,0,1,GETDATE(),1,GETDATE())
	select @TermId = ID from @OutputTbl
END
GO



IF EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'InsertTaxmonyTerms' and type = N'P')
BEGIN
    DROP PROCEDURE [dbo].[InsertTaxmonyTerms] 
END
GO

CREATE procedure [dbo].[InsertTaxmonyTerms] (@ParentTerm nvarchar(250), @Term nvarchar(250))
AS
  BEGIN
  Declare @NewParentID int
  Declare @NewID int
   if isnull(@ParentTerm,'') =  ''
   BEGIN
		EXEC InsertTermId @ParentTermId = NULL,@Term = @Term,@TermId = @NewID OUTPUT
	END
  else
  BEGIN
		select @NewParentID = TermId from [Taxonomy_Terms] where [Name] = @ParentTerm
		if isnull(@NewParentID,'') = ''
		BEGIN
			EXEC InsertTermId @ParentTermId = NULL,@Term = @ParentTerm,@TermId = @NewParentID OUTPUT
		END
		EXEC InsertTermId @ParentTermId = @NewParentID,@Term = @Term, @TermId = @NewID OUTPUT
	END
END
GO

exec [InsertTaxmonyTerms] @ParentTerm='Audience',@Term='Executive'
exec [InsertTaxmonyTerms] @ParentTerm='Audience',@Term='Functional Ldr'
exec [InsertTaxmonyTerms] @ParentTerm='Audience',@Term='Virtual Leader'
exec [InsertTaxmonyTerms] @ParentTerm='Key Message',@Term='Driving Accountability and Commitment'
exec [InsertTaxmonyTerms] @ParentTerm='Key Message',@Term='Creativity Within Boundaries'
exec [InsertTaxmonyTerms] @ParentTerm='Key Message',@Term='Getting and Keeping Customers'
exec [InsertTaxmonyTerms] @ParentTerm='Key Message',@Term='Getting Everyone On The Same Page'
exec [InsertTaxmonyTerms] @ParentTerm='Key Message',@Term='Driving Growth and Innovation'
exec [InsertTaxmonyTerms] @ParentTerm='Key Message',@Term='Data and Metrics That Work'
exec [InsertTaxmonyTerms] @ParentTerm='Experience level',@Term='Novice'
exec [InsertTaxmonyTerms] @ParentTerm='Experience level',@Term='Experienced'
exec [InsertTaxmonyTerms] @ParentTerm='Experience level',@Term='Expert'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Function',@Term='Marketing Ops'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Function',@Term='Corporate Mktg'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Function',@Term='Product'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Function',@Term='Digital'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Function',@Term='CX'
exec [InsertTaxmonyTerms] @ParentTerm='SIG',@Term='Customer Engagement'
exec [InsertTaxmonyTerms] @ParentTerm='SIG',@Term='Demand Generation'
exec [InsertTaxmonyTerms] @ParentTerm='SIG',@Term='Stakeholder Alignment'
exec [InsertTaxmonyTerms] @ParentTerm='SIG',@Term='Capability'
exec [InsertTaxmonyTerms] @ParentTerm='SIG',@Term='Metrics'
exec [InsertTaxmonyTerms] @ParentTerm='SIG',@Term='Automation (Tech)'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Inc Followthrough'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Inc Ability to prioritize'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Inc Productivity of resources'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Inc Employee engagement'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Inc Job Satisfaction'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Inc Strategic vs. tactical work'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='inc Right the First Time'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Max Stakeholder Satisfaction'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Max Stakeholder Cooperation'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Max Collaboration'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Max Resource Acquisition'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Max Customer Acq (revenue)'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Max Customer Retention (Revenue)'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Max Share of Wallet (Growth)'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Maximize Product Adoption (Growth)'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Dec Customer Churn'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Decrease Employee Turnover'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Dec Project Stalls'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Dec Duplicated Resources'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Dec Redundant Efforts'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Dec Rework'
exec [InsertTaxmonyTerms] @ParentTerm='Biz Impact',@Term='Dec Alliance Workload'
