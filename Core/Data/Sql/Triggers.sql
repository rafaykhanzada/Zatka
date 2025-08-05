CREATE TRIGGER TR_Product_AfterInsert
ON Product
AFTER INSERT
AS
BEGIN
    SET IDENTITY_INSERT [DAW_CDP].[dbo].[tblProduct] ON;
    INSERT INTO [DAW_CDP].[dbo].[tblProduct] ([Id],
           [Name],
           [Model],
           [MaterialCode],
           [Description],
           [CatId],
           [CreatedOn],
           [UpdatedOn],
           [DeletedOn],
           [CreatedBy],
           [UpdatedBy],
           [DeletedBy],
           [IsActive]
    )
    SELECT inserted.id,
        INSERTED.[Name],
        INSERTED.[Model],
        INSERTED.[MaterialCode],
        INSERTED.[Description],
        INSERTED.[CatId],
        INSERTED.[createdat],
        INSERTED.[updatedat],
        NULL,                       -- Assuming DeletedOn is NULL on insert
        INSERTED.[createdby],
        INSERTED.[updatedby],
        NULL,                       -- Assuming DeletedBy is NULL on insert
        1
    FROM INSERTED;
    SET IDENTITY_INSERT [DAW_CDP].[dbo].[tblProduct] OFF;
END;
GO
CREATE TRIGGER TR_Product_AfterUpdate
ON Product
AFTER UPDATE
AS
BEGIN
    UPDATE c
    SET 
        c.[Name] = i.[Name],
        c.[Model] = i.[Model],
        c.[MaterialCode] = i.[MaterialCode],
        c.[Description] = i.[Description],
        c.[CatId] = i.[CatId],
        c.[CreatedOn] = i.[createdat],   -- Corrected the field
        c.[UpdatedOn] = i.[updatedat],   -- Corrected the field
        c.[CreatedBy] = i.[createdby],
        c.[UpdatedBy] = i.[updatedby],
        c.[IsActive] = 1
    FROM [DAW_CDP].[dbo].[tblProduct] c
    INNER JOIN INSERTED i ON c.Id = i.Id;
END;
GO
CREATE TRIGGER TR_Product_AfterDelete
ON Product
AFTER DELETE
AS
BEGIN
    UPDATE c
    SET 
        c.[DeletedOn] = GETDATE(),
        c.[IsActive] = 0
    FROM [DAW_CDP].[dbo].[tblProduct] c
    INNER JOIN DELETED d ON c.Id = d.Id;
END;
GO
-- Trigger to handle inserts
CREATE TRIGGER TR_Category_AfterInsert
ON Category
AFTER INSERT
AS
BEGIN
    SET IDENTITY_INSERT [DAW_CDP].[dbo].[tblCategory] ON;
    INSERT INTO [DAW_CDP].[dbo].[tblCategory] ([Id],
        [Name], [CatLevel], [CatParentId], [CatImg], 
        [CreatedOn], [UpdatedOn], [CreatedBy], [UpdatedBy], [IsActive]
    )
    SELECT 
		inserted.id,
        INSERTED.[Name],
        INSERTED.[CatLevel],
        INSERTED.[CatParentId],
        INSERTED.[CatImg],
        INSERTED.[createdat],
        INSERTED.[updatedat],
        INSERTED.[createdby],
        INSERTED.[updatedby],
        1
    FROM INSERTED;
    SET IDENTITY_INSERT [DAW_CDP].[dbo].[tblCategory] OFF;
END;
GO

-- Trigger to handle updates
CREATE TRIGGER TR_Category_AfterUpdate
ON Category
AFTER UPDATE
AS
BEGIN
    UPDATE c
    SET 
        c.[Name] = i.[Name],
        c.[CatLevel] = i.[CatLevel],
        c.[CatParentId] = i.[CatParentId],
        c.[CatImg] = i.[CatImg],
        c.[CreatedOn] = i.[createdat],
        c.[UpdatedOn] = i.[updatedat],
        c.[CreatedBy] = i.[createdby],
        c.[UpdatedBy] = i.[updatedby],
        c.[IsActive] = 1
    FROM [DAW_CDP].[dbo].[tblCategory] c
    INNER JOIN INSERTED i ON c.Id = i.id;
END;
GO

-- Trigger to handle deletes
CREATE TRIGGER TR_Category_AfterDelete
ON Category
AFTER DELETE
AS
BEGIN
    UPDATE c
    SET 
        c.[DeletedOn] = GetDate(),
        c.[IsActive] = 0
    FROM [DAW_CDP].[dbo].[tblCategory] c
    INNER JOIN DELETED d ON c.Id = d.id;
END;
GO
CREATE TRIGGER TR_Dealer_AfterInsert
ON dealer_update
AFTER INSERT
AS
BEGIN
    SET IDENTITY_INSERT [DAW_CDP].[dbo].[tblDealer] ON;
    INSERT INTO [DAW_CDP].[dbo].[tblDealer] ([Id],
           [DealerCode],
           [DealerName],
           [IsParent],
           [ParentCode],
           [CreatedOn],
           [UpdatedOn],
           [DeletedOn],
           [CreatedBy],
           [UpdatedBy],
           [DeletedBy],
           [IsActive]
    )
    SELECT inserted.id,
        INSERTED.[DealerCode],
        INSERTED.[DealerName],
        INSERTED.[IsParent],
        INSERTED.[ParentCode],
        INSERTED.[createdat],
        INSERTED.[updatedat],
        NULL,                       -- Assuming DeletedOn is NULL on insert
        INSERTED.[createdby],
        INSERTED.[updatedby],
        NULL,                       -- Assuming DeletedBy is NULL on insert
        1
    FROM INSERTED;
    SET IDENTITY_INSERT [DAW_CDP].[dbo].[tblDealer] OFF;
END;
GO
CREATE TRIGGER TR_Dealer_AfterUpdate
ON dealer_update
AFTER UPDATE
AS
BEGIN
    UPDATE c
    SET 
        c.[DealerCode] = i.[DealerCode],
        c.[DealerName] = i.[DealerName],
        c.[IsParent] = i.[IsParent],
        c.[ParentCode] = i.[ParentCode],
        c.[CreatedOn] = i.[createdat],
        c.[UpdatedOn] = i.[updatedat],   -- Corrected the field
        c.[DeletedOn] = NULL,            -- Corrected the field
        c.[CreatedBy] = i.[createdby],
        c.[UpdatedBy] = i.[updatedby],
        c.[DeletedBy] = NULL,
        c.[IsActive] = 1
    FROM [DAW_CDP].[dbo].[tblDealer] c
    INNER JOIN INSERTED i ON c.Id = i.Id;
END;
GO
CREATE TRIGGER TR_Dealer_AfterDelete
ON dealer_update
AFTER DELETE
AS
BEGIN
    UPDATE c
    SET 
        c.[DeletedOn] = GETDATE(),
        c.[IsActive] = 0
    FROM [DAW_CDP].[dbo].[tblDealer] c
    INNER JOIN DELETED d ON c.Id = d.Id;
END;
GO

DROP TRIGGER IF EXISTS TR_Product_AfterInsert;
GO
DROP TRIGGER IF EXISTS TR_Product_AfterUpdate;
GO
DROP TRIGGER IF EXISTS TR_Product_AfterDelete;
GO
DROP TRIGGER IF EXISTS TR_Category_AfterInsert;
GO
DROP TRIGGER IF EXISTS TR_Category_AfterUpdate;
GO
DROP TRIGGER IF EXISTS TR_Category_AfterDelete;
GO
DROP TRIGGER IF EXISTS TR_Dealer_AfterInsert;
GO
DROP TRIGGER IF EXISTS TR_Dealer_AfterUpdate;
GO
DROP TRIGGER IF EXISTS TR_Dealer_AfterDelete;
GO