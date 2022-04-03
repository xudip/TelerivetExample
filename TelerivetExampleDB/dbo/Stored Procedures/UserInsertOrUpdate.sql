
CREATE PROCEDURE [dbo].[UserInsertOrUpdate]
(
	@UserID int,
	@Name nvarchar(50),
	@Phone nvarchar(20),
	@Pin nvarchar(50),
	@Status bigint
)

AS

IF @UserID > 0
BEGIN
	UPDATE [User]SET [Name] = @Name,
	[Phone] = @Phone,
	[Pin] = @Pin,
	[Status] = @Status
WHERE [UserID] = @UserID

SELECT @UserID
END
ELSE
BEGIN
	DECLARE @UserExists INT = (SELECT UserID FROM [User] WHERE Phone = @phone AND Name = @Name);
	IF(@UserExists > 0)
	BEGIN
		SELECT -1;
	END
	ELSE
	BEGIN
		INSERT INTO [User](	[Name],	[Phone],	[Pin],	[Status])VALUES(	@Name,	@Phone,	@Pin,	@Status)
		SELECT SCOPE_IDENTITY()
	END
END