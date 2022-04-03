CREATE PROCEDURE [dbo].[ApproveProject] 
	@RequestID int,
	@ProjectCode nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @UserProjectID INT=(
							SELECT UserProjectID 
							FROM UserProject 
							WHERE ProjectCode = @ProjectCode 
							AND UserProjectID = @RequestID
						);
	IF(ISNULL(@UserProjectID, 0) = 0)
	BEGIN
		--RETURN -1 FOR NO RECORD FOUND
		SELECT -1;
	END
	ELSE
	BEGIN
		UPDATE UserProject SET Status = 1 WHERE ProjectCode = @ProjectCode AND UserProjectID = @RequestID
		SELECT @RequestID;
	END
END