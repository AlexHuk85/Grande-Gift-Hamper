IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Email] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [PasswordHash] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles] ([UserId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'2.0.3-rtm-10026');

GO

DROP INDEX [UserNameIndex] ON [AspNetUsers];

GO

DROP INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles];

GO

DROP INDEX [RoleNameIndex] ON [AspNetRoles];

GO

ALTER TABLE [AspNetUsers] ADD [FullName] nvarchar(max) NULL;

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190530082131_add-user-fullname', N'2.0.3-rtm-10026');

GO

CREATE TABLE [Category] (
    [CategoryId] int NOT NULL IDENTITY,
    [CategoryName] nvarchar(max) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryId])
);

GO

CREATE TABLE [Product] (
    [ProductId] int NOT NULL IDENTITY,
    [ProductCategory] nvarchar(max) NULL,
    [ProductName] nvarchar(max) NULL,
    [ProductQty] nvarchar(max) NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId])
);

GO

CREATE TABLE [Hamper] (
    [HamperId] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [ContentSize] bigint NOT NULL,
    [ContentType] nvarchar(max) NULL,
    [FileContent] varbinary(max) NULL,
    [FileName] nvarchar(max) NULL,
    [HamperDetail] nvarchar(max) NULL,
    [HamperName] nvarchar(max) NULL,
    [HamperPrice] decimal(18, 2) NOT NULL,
    CONSTRAINT [PK_Hamper] PRIMARY KEY ([HamperId]),
    CONSTRAINT [FK_Hamper_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]) ON DELETE CASCADE
);

GO

CREATE TABLE [HamperProduct] (
    [HamperProductId] int NOT NULL IDENTITY,
    [HamperId] int NOT NULL,
    [ProductId] int NOT NULL,
    CONSTRAINT [PK_HamperProduct] PRIMARY KEY ([HamperProductId]),
    CONSTRAINT [FK_HamperProduct_Hamper_HamperId] FOREIGN KEY ([HamperId]) REFERENCES [Hamper] ([HamperId]) ON DELETE CASCADE,
    CONSTRAINT [FK_HamperProduct_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Hamper_CategoryId] ON [Hamper] ([CategoryId]);

GO

CREATE INDEX [IX_HamperProduct_HamperId] ON [HamperProduct] ([HamperId]);

GO

CREATE INDEX [IX_HamperProduct_ProductId] ON [HamperProduct] ([ProductId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190530083417_Models-Tables', N'2.0.3-rtm-10026');

GO

ALTER TABLE [HamperProduct] DROP CONSTRAINT [FK_HamperProduct_Hamper_HamperId];

GO

ALTER TABLE [HamperProduct] DROP CONSTRAINT [FK_HamperProduct_Product_ProductId];

GO

ALTER TABLE [HamperProduct] DROP CONSTRAINT [PK_HamperProduct];

GO

EXEC sp_rename N'HamperProduct', N'hamperProducts';

GO

EXEC sp_rename N'hamperProducts.IX_HamperProduct_ProductId', N'IX_hamperProducts_ProductId', N'INDEX';

GO

EXEC sp_rename N'hamperProducts.IX_HamperProduct_HamperId', N'IX_hamperProducts_HamperId', N'INDEX';

GO

ALTER TABLE [hamperProducts] ADD CONSTRAINT [PK_hamperProducts] PRIMARY KEY ([HamperProductId]);

GO

ALTER TABLE [hamperProducts] ADD CONSTRAINT [FK_hamperProducts_Hamper_HamperId] FOREIGN KEY ([HamperId]) REFERENCES [Hamper] ([HamperId]) ON DELETE CASCADE;

GO

ALTER TABLE [hamperProducts] ADD CONSTRAINT [FK_hamperProducts_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190530221828_Hampers', N'2.0.3-rtm-10026');

GO

CREATE TABLE [ProductFeedback] (
    [ProductFeedbackID] int NOT NULL IDENTITY,
    [Comment] nvarchar(max) NULL,
    [ProductId] int NOT NULL,
    [UserName] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductFeedback] PRIMARY KEY ([ProductFeedbackID]),
    CONSTRAINT [FK_ProductFeedback_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_ProductFeedback_ProductId] ON [ProductFeedback] ([ProductId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190603224443_Add-ProductFeedback', N'2.0.3-rtm-10026');

GO

CREATE TABLE [HamperFeedBack] (
    [HamperFeedBackId] int NOT NULL IDENTITY,
    [HamperId] int NOT NULL,
    [UserFeedBack] nvarchar(max) NULL,
    [UserName] nvarchar(max) NULL,
    CONSTRAINT [PK_HamperFeedBack] PRIMARY KEY ([HamperFeedBackId]),
    CONSTRAINT [FK_HamperFeedBack_Hamper_HamperId] FOREIGN KEY ([HamperId]) REFERENCES [Hamper] ([HamperId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_HamperFeedBack_HamperId] ON [HamperFeedBack] ([HamperId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190603224954_Add-HamperFeedBack', N'2.0.3-rtm-10026');

GO

ALTER TABLE [HamperFeedBack] DROP CONSTRAINT [FK_HamperFeedBack_Hamper_HamperId];

GO

ALTER TABLE [HamperFeedBack] DROP CONSTRAINT [PK_HamperFeedBack];

GO

EXEC sp_rename N'HamperFeedBack', N'hamperFeedBacks';

GO

EXEC sp_rename N'hamperFeedBacks.IX_HamperFeedBack_HamperId', N'IX_hamperFeedBacks_HamperId', N'INDEX';

GO

ALTER TABLE [hamperFeedBacks] ADD CONSTRAINT [PK_hamperFeedBacks] PRIMARY KEY ([HamperFeedBackId]);

GO

ALTER TABLE [hamperFeedBacks] ADD CONSTRAINT [FK_hamperFeedBacks_Hamper_HamperId] FOREIGN KEY ([HamperId]) REFERENCES [Hamper] ([HamperId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190604214014_feedback', N'2.0.3-rtm-10026');

GO

ALTER TABLE [AspNetUsers] ADD [UserAddressId] int NOT NULL DEFAULT 0;

GO

CREATE TABLE [UserAddress] (
    [UserAddressId] int NOT NULL IDENTITY,
    [Address] nvarchar(max) NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    CONSTRAINT [PK_UserAddress] PRIMARY KEY ([UserAddressId]),
    CONSTRAINT [FK_UserAddress_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_UserAddress_ApplicationUserId] ON [UserAddress] ([ApplicationUserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190605031722_addAddress', N'2.0.3-rtm-10026');

GO

ALTER TABLE [UserAddress] DROP CONSTRAINT [FK_UserAddress_AspNetUsers_ApplicationUserId];

GO

DROP INDEX [IX_UserAddress_ApplicationUserId] ON [UserAddress];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'UserAddress') AND [c].[name] = N'ApplicationUserId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [UserAddress] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [UserAddress] ALTER COLUMN [ApplicationUserId] int NOT NULL;

GO

ALTER TABLE [UserAddress] ADD [ApplicationUserId1] nvarchar(450) NULL;

GO

CREATE INDEX [IX_UserAddress_ApplicationUserId1] ON [UserAddress] ([ApplicationUserId1]);

GO

ALTER TABLE [UserAddress] ADD CONSTRAINT [FK_UserAddress_AspNetUsers_ApplicationUserId1] FOREIGN KEY ([ApplicationUserId1]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190605032128_addAddressID', N'2.0.3-rtm-10026');

GO

DROP TABLE [UserAddress];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'UserAddressId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AspNetUsers] DROP COLUMN [UserAddressId];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190605032933_Remove', N'2.0.3-rtm-10026');

GO

CREATE TABLE [UserAddress] (
    [UserAddressId] int NOT NULL IDENTITY,
    [Address] nvarchar(max) NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    CONSTRAINT [PK_UserAddress] PRIMARY KEY ([UserAddressId]),
    CONSTRAINT [FK_UserAddress_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_UserAddress_ApplicationUserId] ON [UserAddress] ([ApplicationUserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190605041734_AddUserField', N'2.0.3-rtm-10026');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190606014802_cart', N'2.0.3-rtm-10026');

GO

ALTER TABLE [Hamper] ADD [OrderId] int NULL;

GO

CREATE TABLE [Order] (
    [OrderId] int NOT NULL IDENTITY,
    [OrderDate] datetime2 NOT NULL,
    [Total] decimal(18, 2) NOT NULL,
    [UserAddress] nvarchar(max) NULL,
    [UserId] nvarchar(max) NULL,
    [UserName] nvarchar(max) NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([OrderId])
);

GO

CREATE INDEX [IX_Hamper_OrderId] ON [Hamper] ([OrderId]);

GO

ALTER TABLE [Hamper] ADD CONSTRAINT [FK_Hamper_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([OrderId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190606224326_OrderTable', N'2.0.3-rtm-10026');

GO

ALTER TABLE [Order] ADD [TotalWithShipping] decimal(18, 2) NOT NULL DEFAULT 0.0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190607050208_OrderTableTotal', N'2.0.3-rtm-10026');

GO

ALTER TABLE [Hamper] DROP CONSTRAINT [FK_Hamper_Order_OrderId];

GO

DROP INDEX [IX_Hamper_OrderId] ON [Hamper];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Hamper') AND [c].[name] = N'OrderId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Hamper] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Hamper] DROP COLUMN [OrderId];

GO

CREATE TABLE [OrderHamper] (
    [OrderHamperId] int NOT NULL IDENTITY,
    [HamperName] nvarchar(max) NULL,
    [OrderId] int NOT NULL,
    [Qty] int NOT NULL,
    CONSTRAINT [PK_OrderHamper] PRIMARY KEY ([OrderHamperId]),
    CONSTRAINT [FK_OrderHamper_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([OrderId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_OrderHamper_OrderId] ON [OrderHamper] ([OrderId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190608110135_addOrderHamper', N'2.0.3-rtm-10026');

GO

ALTER TABLE [OrderHamper] ADD [HamperId] int NULL;

GO

CREATE INDEX [IX_OrderHamper_HamperId] ON [OrderHamper] ([HamperId]);

GO

ALTER TABLE [OrderHamper] ADD CONSTRAINT [FK_OrderHamper_Hamper_HamperId] FOREIGN KEY ([HamperId]) REFERENCES [Hamper] ([HamperId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190614025908_addHamperInHamperOrder', N'2.0.3-rtm-10026');

GO

ALTER TABLE [Hamper] ADD [IsRemove] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190614030100_addBoolInHamper', N'2.0.3-rtm-10026');

GO

