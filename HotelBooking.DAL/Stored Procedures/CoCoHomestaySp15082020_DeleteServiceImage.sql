/****** Object:  StoredProcedure [dbo].[ServiceImage_Delete]    Script Date: 15/08/2020 6:01:02 CH ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Trung
-- Create date: 7/8/2020
-- Description:
-- =============================================
CREATE PROCEDURE [dbo].[ServiceImage_Delete]
	@ServiceImageId INT
AS
BEGIN 
DECLARE @Message NVARCHAR(200) = 'Something went wrong, please try again'
	BEGIN TRY
		DELETE FROM [dbo].SERVICEIMAGES
		WHERE ServiceImageId = @ServiceImageId
		SET @Message = 'Image has been deleted successfully!'
	END TRY
	BEGIN CATCH
	END CATCH
	SELECT @ServiceImageId AS Id, @Message AS [Message]
END  

GO

