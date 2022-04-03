CREATE PROCEDURE [dbo].[ChangeProjectPin] 
(
	@RequestID INT,
	@ProjectCode nvarchar(20),
	@Pin nvarchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @UserProjectID INT = (SELECT UserProjectID FROM UserProject WHERE ProjectCode = @ProjectCode
	AND UserProjectID = @RequestID);
	IF(ISNULL(@UserProjectID, 0) =0)
	BEGIN
		SELECT -1;
	END
	ELSE
	BEGIN
		UPDATE UserProject SET Pin = @Pin WHERE UserProjectID = @UserProjectID
		SELECT @UserProjectID;
	END
END