﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [USUARIO] (
    [ID] uniqueidentifier NOT NULL,
    [NOME] nvarchar(150) NOT NULL,
    [EMAIL] nvarchar(50) NOT NULL,
    [SENHA] nvarchar(100) NOT NULL,
    [DATAHORACRIACAO] datetime2 NOT NULL,
    CONSTRAINT [PK_USUARIO] PRIMARY KEY ([ID])
);
GO

CREATE UNIQUE INDEX [IX_USUARIO_EMAIL] ON [USUARIO] ([EMAIL]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250107000154_Initial', N'8.0.11');
GO

COMMIT;
GO

