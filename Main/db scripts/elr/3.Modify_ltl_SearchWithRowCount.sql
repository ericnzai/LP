
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ltl_SearchWithRowCount')
DROP PROCEDURE [dbo].[ltl_SearchWithRowCount]
GO


/****** Object:  StoredProcedure [dbo].[ltl_SearchWithRowCount]    Script Date: 20/06/2016 15:25:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ltl_SearchWithRowCount]
    @searchstring  nvarchar(500) = 'z1p988zxxxa',
    @culture  nvarchar(15) = 'en',
    --@UserRoles TVP_UserRoles READONLY,
	@UserRoles nvarchar(500),
	@FromGroupID int,
	@NotFromGroupID int,
	@GroupID nvarchar(500),
	@TopicID nvarchar(500)
as
if @searchstring is null
begin
	select @searchstring = 'z1p988zxxxa'
end

if @culture is null
begin
	select @culture = (select SettingValue from ltl_SiteSettings where SettingName = 'Culture')
end



SET NOCOUNT ON;
	DECLARE @v_UserRoles TABLE  (RoleID int)	

	INSERT INTO @v_UserRoles
		SELECT CONVERT (INT, Value)  FROM dbo.SplitList(@UserRoles, ',');

	DECLARE @v_Groups TABLE  (GroupID int)	

	INSERT INTO @v_Groups
		SELECT CONVERT (INT, Value)  FROM dbo.SplitList(@GroupID, ',');

	DECLARE @v_Topics TABLE  (TopicID int)	

	INSERT INTO @v_Topics
		SELECT CONVERT (INT, Value)  FROM dbo.SplitList(@TopicID, ',');


	SELECT DISTINCT FT_TBL.PostID, KEY_TBL.RANK, FT_TBL.Subject, FT_TBL.Body, FT_TBL.LastUpdated, p.SortOrder, st.Name, s.FriendlyUrl as SectionFriendlyName, pst.Name as ParentSectionName, g.GroupID, g.Name as GroupName, g.FriendlyUrl as GroupFriendlyName, ta.FriendlyUrl as TrainingAreaFriendlyName
	FROM ltl_PostTranslations AS FT_TBL 
		 INNER JOIN
		 FREETEXTTABLE(ltl_PostTranslations, (Subject,Body, FormattedBody, PostName),
						@searchstring) AS KEY_TBL
		 ON FT_TBL.PostTranslationID = KEY_TBL.[KEY]
		 INNER JOIN ltl_Posts p ON p.PostID = FT_TBL.PostID
		 LEFT JOIN PostTopics pt ON pt.PostID = p.PostID 
		 INNER JOIN ltl_Sections s ON s.SectionID = p.SectionID
		 Inner JOIN ltl_SectionTranslations st ON st.SectionID = s.SectionID
		 INNER JOIN ltl_Sections ps ON s.ParentID = ps.SectionID
		 Inner JOIN ltl_SectionTranslations pst ON pst.SectionID = ps.SectionID
		 INNER JOIN ltl_Groups g on s.GroupID = g.GroupID
		 INNER JOIN ltl_TrainingArea ta on ta.TrainingAreaID = g.TrainingAreaID
		 
		 
	WHERE
	Exists(select count(1)
			from @v_UserRoles ur
				INNER JOIN ltl_SectionPermissions sp ON sp.RoleID = ur.RoleID
			WHERE sp.SectionID = ps.SectionID) AND
	p.PostStatus = 2 AND
	s.Status = 2 AND
	g.StatusBankID = 2 AND
	ta.StatusBankID = 2 AND
	ps.Status = 2 AND
	st.Culture = @culture AND
	pst.Culture = @culture AND
	FT_TBL.Culture = @culture AND
	g.Culture = @culture 
	AND (@FromGroupID is null OR s.GroupID = @FromGroupID or @GroupID = '0' or @GroupID = '' or g.GroupID in (select * from @v_Groups))
	AND (@NotFromGroupID is null OR s.GroupID != @NotFromGroupID)
	AND (@TopicID = '0' or @TopicID = '' or pt.TopicID in (select * from @v_Topics))
	

	 
	 
	

SELECT COUNT(DISTINCT 1) 
FROM ltl_PostTranslations AS FT_TBL 
		 INNER JOIN
		 FREETEXTTABLE(ltl_PostTranslations, (Subject,Body, FormattedBody, PostName),
						@searchstring) AS KEY_TBL
		 ON FT_TBL.PostTranslationID = KEY_TBL.[KEY]
		 INNER JOIN ltl_Posts p ON p.PostID = FT_TBL.PostID
		 LEFT JOIN PostTopics pt ON pt.PostID = p.PostID
		 INNER JOIN ltl_Sections s ON s.SectionID = p.SectionID
		 INNER JOIN ltl_Sections ps ON s.ParentID = ps.SectionID
		 INNER JOIN ltl_Groups g on s.GroupID = g.GroupID
		 INNER JOIN ltl_TrainingArea ta on ta.TrainingAreaID = g.TrainingAreaID

	WHERE
	Exists(select count(DISTINCT 1)
			from @v_UserRoles ur
				INNER JOIN ltl_SectionPermissions sp ON sp.RoleID = ur.RoleID
			WHERE sp.SectionID = ps.SectionID) AND
	p.PostStatus = 2 AND
	s.Status = 2 AND
	g.StatusBankID = 2 AND
	ta.StatusBankID = 2 AND
	ps.Status = 2 AND
	FT_TBL.Culture = @culture AND
	g.Culture = @culture
	AND (@FromGroupID is null OR s.GroupID = @FromGroupID or @GroupID = '0' or @GroupID = '' or g.GroupID in (select * from @v_Groups))
	AND (@NotFromGroupID is null OR s.GroupID != @NotFromGroupID)
	AND (@TopicID = '0' or @TopicID = '' or pt.TopicID in (select * from @v_Topics))
	
