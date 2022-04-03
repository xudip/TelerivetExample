
CREATE PROCEDURE [dbo].[UserProjectInsertOrUpdate]
(
	@UserProjectID int,
	@ProjectCode nvarchar(50),
	@Amount decimal(9, 2),
	@Pin nvarchar(50),
	@Status bit,
	@FromNumber nvarchar(20)
)

AS

IF @UserProjectID > 0
BEGIN
	UPDATE [UserProject]SET 
	[ProjectCode] = @ProjectCode,
	[Amount] = @Amount,
	[Pin] = @Pin,
	[Status] = @Status
WHERE [UserProjectID] = @UserProjectID

SELECT @UserProjectID
END
ELSE
BEGIN
	DECLARE @UserID INT = (SELECT UserID from [User] WHERE Phone = @FromNumber);
	DECLARE @UserProjectExists INT = (SELECT UserProjectID FROM UserProject WHERE ProjectCode = @ProjectCode
	AND UserID = @UserID);
	IF(@UserProjectExists > 0)
	BEGIN
		SELECT -1;
	END
	ELSE
	BEGIN
	INSERT INTO [UserProject](	[UserID],	[ProjectCode],	[Amount],	[Pin],	[Status])VALUES(	@UserID,	@ProjectCode,	@Amount,	@Pin,	@Status)
		SELECT SCOPE_IDENTITY()
	END
END