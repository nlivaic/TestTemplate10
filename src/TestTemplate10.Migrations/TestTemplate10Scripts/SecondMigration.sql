BEGIN TRANSACTION;
GO

ALTER TABLE [Foos] ADD [Text1] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240611120933_SecondMigration', N'6.0.8');
GO

COMMIT;
GO

