
CREATE PROCEDURE [dbo].[UserProjectDelete]
(
	@UserProjectID int
)

AS

SET NOCOUNT OFF

DELETE FROM [UserProject]
WHERE [UserProjectID] = @UserProjectID