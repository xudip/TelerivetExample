
CREATE PROCEDURE [dbo].[UserProjectDeleteAllByUserID]
(
	@UserID int
)

AS

SET NOCOUNT ON

DELETE FROM [UserProject]
WHERE [UserID] = @UserID