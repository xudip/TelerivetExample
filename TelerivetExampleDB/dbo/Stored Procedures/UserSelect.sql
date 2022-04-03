
CREATE PROCEDURE [dbo].[UserSelect]
(
	@UserID int
)

AS

SET NOCOUNT ON

SELECT [UserID],
	[Name],
	[Phone],
	[Pin],
	[Status]
FROM [User]
WHERE [UserID] = @UserID