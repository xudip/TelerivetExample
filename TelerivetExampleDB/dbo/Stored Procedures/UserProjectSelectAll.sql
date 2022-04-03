
CREATE PROCEDURE [dbo].[UserProjectSelectAll]

AS

SET NOCOUNT ON

SELECT [UserProjectID],
	up.[UserID],
	[ProjectCode],
	[Amount],
	up.[Pin],
	up.[Status],
	u.Name as UserName,
	up.CreatedDate
FROM [UserProject] up
LEFT JOIN [User] u on u.UserID = up.UserID
ORDER BY up.CreatedDate DESC