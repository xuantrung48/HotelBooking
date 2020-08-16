/****** Object:  StoredProcedure [dbo].[ServiceImage_GetByServiceId]    Script Date: 15/08/2020 6:02:03 CH ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Trung
-- Create date: 7/8/2020
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ServiceImage_GetByServiceId]
	@ServiceId INT
AS
BEGIN
	SELECT * FROM [dbo].[SERVICEIMAGES] WHERE ServiceId = @ServiceId
END
GO

