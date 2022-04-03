
CREATE PROCEDURE [dbo].[UserProjectSelectAllByUserID]
(
	@UserID int
)

AS

SET NOCOUNT ON

SELECT [UserProjectID],
	[UserID],
	[ProjectCode],
	[Amount],
	[Pin],
	[Status]
FROM [UserProject]
WHERE [UserID] = @UserID