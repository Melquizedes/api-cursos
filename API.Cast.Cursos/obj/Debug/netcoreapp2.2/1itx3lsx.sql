IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Categorias] (
    [Codigo] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([Codigo])
);

GO

CREATE TABLE [Cursos] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NULL,
    [DataInicio] datetime2 NOT NULL,
    [DataTermino] datetime2 NOT NULL,
    [QntAlunos] int NULL,
    [CategoriaId] int NOT NULL,
    CONSTRAINT [PK_Cursos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cursos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([Codigo]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Cursos_CategoriaId] ON [Cursos] ([CategoriaId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201020123252_inicial', N'2.2.2-servicing-10034');

GO

