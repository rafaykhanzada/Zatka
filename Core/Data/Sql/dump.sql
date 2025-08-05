USE [DAW_CDP] GO
INSERT INTO [dbo].[tblDealer] ([DealerCode] , [DealerName] , [IsParent] , [ParentCode] , [CreatedOn] , [UpdatedOn] , [CreatedBy] , [UpdatedBy] , [IsActive])
SELECT DealerCode,
       DealerName,
       IsParent,
       ParentCode,
       createdat,
       updatedat,
       createdby,
       updatedby,
       1
FROM DAW_SM.dbo.dealer_update GO USE [DAW_CDP] GO
INSERT INTO [dbo].[tblCategory] ([Name] , [CatLevel] , [CatParentId] , [CatImg] , [CreatedOn] , [UpdatedOn] , [CreatedBy] , [UpdatedBy] , [IsActive])
SELECT [Name] ,
       [CatLevel] ,
       [CatParentId] ,
       [CatImg],
       createdat,
       updatedat,
       createdby,
       updatedby,
       1
FROM DAW_SM.dbo.category GO USE [DAW_CDP] GO
INSERT INTO [dbo].[tblProduct] ([Name] , [Model] , [MaterialCode] , [Description] , [CatId] , [CreatedOn] , [UpdatedOn] , [CreatedBy] , [UpdatedBy] , [IsActive])
SELECT [Name] ,
       [Model] ,
       [MaterialCode] ,
       [Description] ,
       [CatId],
       createdat,
       updatedat,
       createdby,
       updatedby,
       1
FROM DAW_SM.dbo.product GO