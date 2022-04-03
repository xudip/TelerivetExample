
CREATE PROCEDURE [dbo].[UserProjectSelect]
(
	@UserProjectID int
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
WHERE [UserProjectID] = @UserProjectID