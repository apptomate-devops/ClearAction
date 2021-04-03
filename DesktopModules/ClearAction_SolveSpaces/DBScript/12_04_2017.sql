/* Added by Sachin */

/****** Object:  StoredProcedure [dbo].[CA_ListSolveSpacesTopN]    Script Date: 4/12/2017 1:35:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sachin Srivastava
-- Create date: 13-Nov-2017
-- Description:	To Get the list of Solve-Spaces on various parameters
--  [dbo].[CA_ListSolveSpacesTopN] 1
-- =============================================
ALTER PROCEDURE [dbo].[CA_ListSolveSpacesTopN]  
@UserID int,
@TopN int
AS
BEGIN
SELECT DISTINCT TOP (@TopN) tblFinal.* FROM (
SELECT 
	CASE WHEN ((ISNULL(StepCount, 0)) > 0 AND (ISNULL(StepCount, 0))<TotalSteps) THEN 'In-Progress' 
	WHEN (TabLink = '') THEN 'Soon' 
	WHEN (TotalSteps = StepCount) THEN 'Completed' 
	When (B.IsAssigned=1 AND StepCount=0) then 'To-Do'
	END AS [Status],
	B.* 
FROM (
SELECT 
	USS.UserID,Max(USS.LastUpdatedOn) LastUpdatedOn, UC.CreatedOn,A.SolveSpaceID,
	CASE WHEN (IsNull(UC.CreatedBy,-1)=@UserID AND IsNull(UC.ItemID,0)>0) THEN 1 ELSE 0 END IsSelfAssigned,
	CASE WHEN ISNULL(UC.ItemID,0)>0 THEN 1 ELSE 0 END IsAssigned,
	ISNULL( USS.StepCount,0) StepCount, 
	 A.DurationInMin, A.ShortDescription,
	A.TabLink, A.Title, A.TotalSteps
FROM 
	ClearAction_SolveSpaceMaster A
LEFT OUTER JOIN 
(
	SELECT DISTINCT USERID,COUNT(StepID) StepCount,SolveSpaceID ,MAX(LastUpdatedOn) LastUpdatedOn
	FROM ClearAction_UserSolveSpaces 
	GROUP BY SolveSpaceID,UserID
	HAVING UserID=@UserID
) USS 
ON USS.SolveSpaceID=A.SolveSpaceID 
LEFT OUTER JOIN 
(SELECT DISTINCT * FROM ClearAction_UserComponents WHERE UseriD=@UserID and ComponentID=3) UC  
ON A.SolveSpaceID=UC.ItemID
GROUP BY USS.UserID,UC.UserID,USS.LastUpdatedOn,UC.CreatedOn,UC.ItemID,USS.StepCount,A.Title,A.SolveSpaceID,
UC.CreatedOn,UC.CreatedBy,a.DurationInMin,a.ShortDescription,a.TabLink,a.TotalSteps
HAVING 
(UC.UserID=@UserID OR USS.UserID=@UserID ) 
) AS B) tblFinal
LEFT OUTER JOIN ClearAction_SolveSpaceCategories SSC on SSC.SolveSpaceID=tblFinal.SolveSpaceID
ORDER BY LastUpdatedOn DESC
END