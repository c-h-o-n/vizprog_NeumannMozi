
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/06/2020 20:24:37
-- Generated from EDMX file: C:\Users\Chon\Desktop\Neumann Mozi\Git Repo\NeumannMozi\NeumannMozi_DAL\edmNeumannMozi.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\USERS\CHON\DESKTOP\NEUMANN MOZI\GIT REPO\NEUMANNMOZI\DATABASE\DATABASE_NEUMANNMOZI.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UlesUles_foglalas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ules_foglalasSet] DROP CONSTRAINT [FK_UlesUles_foglalas];
GO
IF OBJECT_ID(N'[dbo].[FK_TeremVetites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VetitesSet] DROP CONSTRAINT [FK_TeremVetites];
GO
IF OBJECT_ID(N'[dbo].[FK_FilmVetites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VetitesSet] DROP CONSTRAINT [FK_FilmVetites];
GO
IF OBJECT_ID(N'[dbo].[FK_TeremUles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UlesSet] DROP CONSTRAINT [FK_TeremUles];
GO
IF OBJECT_ID(N'[dbo].[FK_VetitesUles_foglalas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ules_foglalasSet] DROP CONSTRAINT [FK_VetitesUles_foglalas];
GO
IF OBJECT_ID(N'[dbo].[FK_FelhasznaloFoglalas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FoglalasSet] DROP CONSTRAINT [FK_FelhasznaloFoglalas];
GO
IF OBJECT_ID(N'[dbo].[FK_FoglalasUles_foglalas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ules_foglalasSet] DROP CONSTRAINT [FK_FoglalasUles_foglalas];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FelhasznaloSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FelhasznaloSet];
GO
IF OBJECT_ID(N'[dbo].[FoglalasSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FoglalasSet];
GO
IF OBJECT_ID(N'[dbo].[Ules_foglalasSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ules_foglalasSet];
GO
IF OBJECT_ID(N'[dbo].[UlesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UlesSet];
GO
IF OBJECT_ID(N'[dbo].[TeremSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeremSet];
GO
IF OBJECT_ID(N'[dbo].[FilmSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FilmSet];
GO
IF OBJECT_ID(N'[dbo].[VetitesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VetitesSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FelhasznaloSet'
CREATE TABLE [dbo].[FelhasznaloSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nev] nvarchar(max)  NOT NULL,
    [Jelszo] nvarchar(max)  NOT NULL,
    [Admin] bit  NOT NULL
);
GO

-- Creating table 'FoglalasSet'
CREATE TABLE [dbo].[FoglalasSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Foglalt] bit  NOT NULL,
    [FizetveVan] bit  NOT NULL,
    [Aktiv] bit  NOT NULL,
    [Datum] datetime  NOT NULL,
    [FelhasznaloId] int  NULL
);
GO

-- Creating table 'Ules_foglalasSet'
CREATE TABLE [dbo].[Ules_foglalasSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Kategoria] nvarchar(max)  NOT NULL,
    [UlesId] int  NOT NULL,
    [VetitesId] int  NOT NULL,
    [FoglalasId] int  NOT NULL
);
GO

-- Creating table 'UlesSet'
CREATE TABLE [dbo].[UlesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Sor] int  NOT NULL,
    [Szam] int  NOT NULL,
    [TeremId] int  NOT NULL
);
GO

-- Creating table 'TeremSet'
CREATE TABLE [dbo].[TeremSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nev] nvarchar(max)  NOT NULL,
    [UlesekSzama] int  NOT NULL,
    [TakaritaniKell] bit  NOT NULL
);
GO

-- Creating table 'FilmSet'
CREATE TABLE [dbo].[FilmSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Cim] nvarchar(max)  NOT NULL,
    [Rendezo] nvarchar(max)  NOT NULL,
    [Szereplok] nvarchar(max)  NOT NULL,
    [Leiras] nvarchar(max)  NOT NULL,
    [Hossz] int  NOT NULL,
    [Korhatar] int  NOT NULL,
    [Kategoria] nvarchar(max)  NOT NULL,
    [ElozetesLink] nvarchar(max)  NOT NULL,
    [ImdbLink] nvarchar(max)  NOT NULL,
    [Poszter] varbinary(max)  NULL
);
GO

-- Creating table 'VetitesSet'
CREATE TABLE [dbo].[VetitesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Kezdete] datetime  NOT NULL,
    [TeremId] int  NOT NULL,
    [FilmId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'FelhasznaloSet'
ALTER TABLE [dbo].[FelhasznaloSet]
ADD CONSTRAINT [PK_FelhasznaloSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FoglalasSet'
ALTER TABLE [dbo].[FoglalasSet]
ADD CONSTRAINT [PK_FoglalasSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ules_foglalasSet'
ALTER TABLE [dbo].[Ules_foglalasSet]
ADD CONSTRAINT [PK_Ules_foglalasSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UlesSet'
ALTER TABLE [dbo].[UlesSet]
ADD CONSTRAINT [PK_UlesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TeremSet'
ALTER TABLE [dbo].[TeremSet]
ADD CONSTRAINT [PK_TeremSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FilmSet'
ALTER TABLE [dbo].[FilmSet]
ADD CONSTRAINT [PK_FilmSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VetitesSet'
ALTER TABLE [dbo].[VetitesSet]
ADD CONSTRAINT [PK_VetitesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UlesId] in table 'Ules_foglalasSet'
ALTER TABLE [dbo].[Ules_foglalasSet]
ADD CONSTRAINT [FK_UlesUles_foglalas]
    FOREIGN KEY ([UlesId])
    REFERENCES [dbo].[UlesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UlesUles_foglalas'
CREATE INDEX [IX_FK_UlesUles_foglalas]
ON [dbo].[Ules_foglalasSet]
    ([UlesId]);
GO

-- Creating foreign key on [TeremId] in table 'VetitesSet'
ALTER TABLE [dbo].[VetitesSet]
ADD CONSTRAINT [FK_TeremVetites]
    FOREIGN KEY ([TeremId])
    REFERENCES [dbo].[TeremSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeremVetites'
CREATE INDEX [IX_FK_TeremVetites]
ON [dbo].[VetitesSet]
    ([TeremId]);
GO

-- Creating foreign key on [FilmId] in table 'VetitesSet'
ALTER TABLE [dbo].[VetitesSet]
ADD CONSTRAINT [FK_FilmVetites]
    FOREIGN KEY ([FilmId])
    REFERENCES [dbo].[FilmSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FilmVetites'
CREATE INDEX [IX_FK_FilmVetites]
ON [dbo].[VetitesSet]
    ([FilmId]);
GO

-- Creating foreign key on [TeremId] in table 'UlesSet'
ALTER TABLE [dbo].[UlesSet]
ADD CONSTRAINT [FK_TeremUles]
    FOREIGN KEY ([TeremId])
    REFERENCES [dbo].[TeremSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeremUles'
CREATE INDEX [IX_FK_TeremUles]
ON [dbo].[UlesSet]
    ([TeremId]);
GO

-- Creating foreign key on [VetitesId] in table 'Ules_foglalasSet'
ALTER TABLE [dbo].[Ules_foglalasSet]
ADD CONSTRAINT [FK_VetitesUles_foglalas]
    FOREIGN KEY ([VetitesId])
    REFERENCES [dbo].[VetitesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VetitesUles_foglalas'
CREATE INDEX [IX_FK_VetitesUles_foglalas]
ON [dbo].[Ules_foglalasSet]
    ([VetitesId]);
GO

-- Creating foreign key on [FelhasznaloId] in table 'FoglalasSet'
ALTER TABLE [dbo].[FoglalasSet]
ADD CONSTRAINT [FK_FelhasznaloFoglalas]
    FOREIGN KEY ([FelhasznaloId])
    REFERENCES [dbo].[FelhasznaloSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FelhasznaloFoglalas'
CREATE INDEX [IX_FK_FelhasznaloFoglalas]
ON [dbo].[FoglalasSet]
    ([FelhasznaloId]);
GO

-- Creating foreign key on [FoglalasId] in table 'Ules_foglalasSet'
ALTER TABLE [dbo].[Ules_foglalasSet]
ADD CONSTRAINT [FK_FoglalasUles_foglalas]
    FOREIGN KEY ([FoglalasId])
    REFERENCES [dbo].[FoglalasSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FoglalasUles_foglalas'
CREATE INDEX [IX_FK_FoglalasUles_foglalas]
ON [dbo].[Ules_foglalasSet]
    ([FoglalasId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------