IF EXISTS (SELECT * FROM [dbo].[Resource_Localization] WHERE [ResourceId] = 'optAll' AND [ResourceSet] = 'Master/Master.aspx' AND [LocaleId] = '')
BEGIN
    UPDATE [dbo].[Resource_Localization] SET [Value] = 'All' WHERE [ResourceId] = 'optAll' AND [ResourceSet] = 'Master/Master.aspx' AND [LocaleId] = ''
END
ELSE
BEGIN
   INSERT INTO [dbo].[Resource_Localization]
           ([ResourceId]
           ,[Value]
           ,[LocaleId]
           ,[ResourceSet]
           ,[Type]
           ,[BinFile]
           ,[TextFile]
           ,[Filename]
           ,[Comment]
           ,[DateCreated]
           ,[DateModified]
           ,[UserID]
           ,[PageType]
           ,[RequiredContentPlaceholders]
           ,[PreviewUrl]
           ,[IsCultureDependent]
           ,[PreviewUrlDisplay])
     VALUES
           ('optAll'
           ,'All'
           ,''
           ,'Master/Master.aspx'
           ,''
           ,null
           ,null
           ,''
           ,''
           ,getDate()
           ,getDate()
           ,null
           ,'Admin'
           ,null
           ,null
           ,1
           ,null)

END



INSERT INTO [dbo].[Resource_Localization]
           ([ResourceId]
           ,[Value]
           ,[LocaleId]
           ,[ResourceSet]
           ,[Type]
           ,[BinFile]
           ,[TextFile]
           ,[Filename]
           ,[Comment]
           ,[DateCreated]
           ,[DateModified]
           ,[UserID]
           ,[PageType]
           ,[RequiredContentPlaceholders]
           ,[PreviewUrl]
           ,[IsCultureDependent]
           ,[PreviewUrlDisplay])
     VALUES
           ('msgSorryNoResults'
           ,'Sorry, there are no results for your search'
           ,''
           ,'Master/Master.aspx'
           ,''
           ,null
           ,null
           ,''
           ,''
           ,getDate()
           ,getDate()
           ,null
           ,'Admin'
           ,null
           ,null
           ,1
           ,null)
GO


INSERT INTO [dbo].[Resource_Localization]
           ([ResourceId]
           ,[Value]
           ,[LocaleId]
           ,[ResourceSet]
           ,[Type]
           ,[BinFile]
           ,[TextFile]
           ,[Filename]
           ,[Comment]
           ,[DateCreated]
           ,[DateModified]
           ,[UserID]
           ,[PageType]
           ,[RequiredContentPlaceholders]
           ,[PreviewUrl]
           ,[IsCultureDependent]
           ,[PreviewUrlDisplay])
     VALUES
           ('ltSearchResults.Text'
           ,'Search results'
           ,''
           ,'Master/Master.aspx'
           ,''
           ,null
           ,null
           ,''
           ,''
           ,getDate()
           ,getDate()
           ,null
           ,'Admin'
           ,null
           ,null
           ,1
           ,null)
GO


