
CREATE PROCEDURE [dbo].[UserDelete]
(
	@UserID int
)

AS

SET NOCOUNT OFF

DELETE FROM [User]
WHERE [UserID] = @UserID