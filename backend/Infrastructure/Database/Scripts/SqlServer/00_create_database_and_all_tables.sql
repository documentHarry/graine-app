-- ============================================================
-- SCHEMA DE BASE DE DONNEES - SQL SERVER
-- ============================================================

IF DB_ID(N'graines') IS NULL
BEGIN
    CREATE DATABASE graines;
END;
GO

USE graines;
GO

-- ============================================================
-- TABLE : Localite
-- ============================================================
IF OBJECT_ID(N'Localite', N'U') IS NULL
BEGIN
    CREATE TABLE Localite (
        IdLocalite INT IDENTITY(1,1) NOT NULL,
        CodePostal NVARCHAR(20) NOT NULL,
        NomLocalite NVARCHAR(200) NOT NULL,
        CONSTRAINT PK_Localite PRIMARY KEY (IdLocalite),
        CONSTRAINT UQ_Localite_CodePostal_Localite UNIQUE (CodePostal, NomLocalite)
    );
END;
GO

-- ============================================================
-- TABLE : Utilisateur
-- ============================================================
IF OBJECT_ID(N'Utilisateur', N'U') IS NULL
BEGIN
    CREATE TABLE Utilisateur (
        IdUtilisateur INT IDENTITY(1,1) NOT NULL,
        Nom NVARCHAR(40) NOT NULL,
        Prenom NVARCHAR(40) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        MotDePasseHash NVARCHAR(MAX) NOT NULL,
        MotDePasseSalt VARBINARY(MAX) NOT NULL,
        DateInscription DATETIME2(0) NOT NULL CONSTRAINT DF_Utilisateur_DateInscription DEFAULT SYSDATETIME(),
        Actif BIT NOT NULL CONSTRAINT DF_Utilisateur_Actif DEFAULT 1,
        CONSTRAINT PK_Utilisateur PRIMARY KEY (IdUtilisateur),
        CONSTRAINT UQ_Utilisateur_Email UNIQUE (Email),
        CONSTRAINT CK_Utilisateur_Actif CHECK (Actif IN (0, 1))
    );
END;
GO

-- ============================================================
-- TABLE : AdresseLivraison
-- ============================================================
IF OBJECT_ID(N'AdresseLivraison', N'U') IS NULL
BEGIN
    CREATE TABLE AdresseLivraison (
        IdAdresse INT IDENTITY(1,1) NOT NULL,
        Rue NVARCHAR(255) NOT NULL,
        Numero NVARCHAR(50) NOT NULL,
        ParDefaut BIT NOT NULL CONSTRAINT DF_AdresseLivraison_ParDefaut DEFAULT 0,
        UtilisateurId INT NOT NULL,
        LocaliteId INT NOT NULL,
        CONSTRAINT PK_AdresseLivraison PRIMARY KEY (IdAdresse),
        CONSTRAINT CK_AdresseLivraison_ParDefaut CHECK (ParDefaut IN (0, 1)),
        CONSTRAINT FK_AdresseLivraison_Utilisateur
            FOREIGN KEY (UtilisateurId)
            REFERENCES Utilisateur(IdUtilisateur)
            ON DELETE CASCADE,
        CONSTRAINT FK_AdresseLivraison_Localite
            FOREIGN KEY (LocaliteId)
            REFERENCES Localite(IdLocalite)
            ON DELETE NO ACTION
    );
END;
GO

-- ============================================================
-- TABLE : Role
-- ============================================================
IF OBJECT_ID(N'Role', N'U') IS NULL
BEGIN
    CREATE TABLE Role (
        IdRole INT IDENTITY(1,1) NOT NULL,
        NomRole NVARCHAR(100) NOT NULL,
        CONSTRAINT PK_Role PRIMARY KEY (IdRole),
        CONSTRAINT UQ_Role_NomRole UNIQUE (NomRole)
    );
END;
GO

-- ============================================================
-- TABLE : UtilisateurRole
-- ============================================================
IF OBJECT_ID(N'UtilisateurRole', N'U') IS NULL
BEGIN
    CREATE TABLE UtilisateurRole (
        UtilisateurId INT NOT NULL,
        RoleId INT NOT NULL,
        CONSTRAINT PK_UtilisateurRole PRIMARY KEY (UtilisateurId, RoleId),
        CONSTRAINT FK_UtilisateurRole_Utilisateur
            FOREIGN KEY (UtilisateurId)
            REFERENCES Utilisateur(IdUtilisateur)
            ON DELETE CASCADE,
        CONSTRAINT FK_UtilisateurRole_Role
            FOREIGN KEY (RoleId)
            REFERENCES Role(IdRole)
            ON DELETE CASCADE
    );
END;
GO

-- ============================================================
-- TABLE : Categorie
-- ============================================================
IF OBJECT_ID(N'Categorie', N'U') IS NULL
BEGIN
    CREATE TABLE Categorie (
        IdCategorie INT IDENTITY(1,1) NOT NULL,
        NomCategorie NVARCHAR(50) NOT NULL,
        Descriptif NVARCHAR(MAX) NULL,
        CONSTRAINT PK_Categorie PRIMARY KEY (IdCategorie),
        CONSTRAINT UQ_Categorie_NomCategorie UNIQUE (NomCategorie)
    );
END;
GO

-- ============================================================
-- TABLE : Espece
-- ============================================================
IF OBJECT_ID(N'Espece', N'U') IS NULL
BEGIN
    CREATE TABLE Espece (
        IdEspece INT IDENTITY(1,1) NOT NULL,
        NomScientifique NVARCHAR(100) NOT NULL,
        NomCommun NVARCHAR(100) NOT NULL,
        CONSTRAINT PK_Espece PRIMARY KEY (IdEspece),
        CONSTRAINT UQ_Espece_NomScientifique_NomCommun UNIQUE (NomScientifique, NomCommun)
    );
END;
GO

-- ============================================================
-- TABLE : Variete
-- ============================================================
IF OBJECT_ID(N'Variete', N'U') IS NULL
BEGIN
    CREATE TABLE Variete (
        IdVariete INT IDENTITY(1,1) NOT NULL,
        Nom NVARCHAR(255) NOT NULL,
        Descriptif NVARCHAR(MAX) NULL,
        Bio BIT NOT NULL CONSTRAINT DF_Variete_Bio DEFAULT 1,
        CycleJours INT NULL,
        CouleurLegume NVARCHAR(50) NULL,
        TailleFixeLegume FLOAT NULL,
        TailleMinLegume FLOAT NULL,
        TailleMaxLegume FLOAT NULL,
        EspacementEntreLesPlants INT NULL,
        EspacementEntreLesLignes INT NULL,
        TypeEnsoleillement NVARCHAR(30) NULL,
        TypeFeuillage NVARCHAR(50) NULL,
        HauteurAdulteMin INT NULL,
        HauteurAdulteMax INT NULL,
        DureeDeGermination NVARCHAR(30) NULL,
        TemperatureMinDeGermination INT NULL,
        CycleDeVie NVARCHAR(20) NULL,
        RusticitePlante NVARCHAR(100) NULL,
        DateSemisMin NVARCHAR(40) NULL,
        DateSemisMax NVARCHAR(40) NULL,
        DureeAvantRecolte NVARCHAR(200) NULL,
        TypeDeSol NVARCHAR(150) NULL,
        ConseilPlantation NVARCHAR(MAX) NULL,
        EspeceId INT NOT NULL,
        CONSTRAINT PK_Variete PRIMARY KEY (IdVariete),
        CONSTRAINT CK_Variete_Bio CHECK (Bio IN (0, 1)),
        CONSTRAINT CK_Variete_EspacementEntreLesPlants CHECK (
            EspacementEntreLesPlants IS NULL OR
            EspacementEntreLesPlants IN (5, 8, 10, 15, 20, 25, 30, 40, 50, 60, 75, 80, 100, 200)
        ),
        CONSTRAINT CK_Variete_EspacementEntreLesLignes CHECK (
            EspacementEntreLesLignes IS NULL OR
            EspacementEntreLesLignes IN (5, 8, 15, 20, 25, 30, 35, 40, 45, 50, 60, 70, 75, 80, 100, 120)
        ),
        CONSTRAINT CK_Variete_CycleDeVie CHECK (
            CycleDeVie IS NULL OR CycleDeVie IN (N'annuelle', N'bisannuelle', N'vivace')
        ),
        CONSTRAINT FK_Variete_Espece
            FOREIGN KEY (EspeceId)
            REFERENCES Espece(IdEspece)
            ON DELETE NO ACTION
    );
END;
GO

-- ============================================================
-- TABLE : Aromate
-- ============================================================
IF OBJECT_ID(N'Aromate', N'U') IS NULL
BEGIN
    CREATE TABLE Aromate (
        IdAromate INT IDENTITY(1,1) NOT NULL,
        PartieUtilisee NVARCHAR(50) NULL,
        Propriete NVARCHAR(MAX) NULL,
        UsageCulinaire NVARCHAR(MAX) NULL,
        VarieteId INT NOT NULL,
        CONSTRAINT PK_Aromate PRIMARY KEY (IdAromate),
        CONSTRAINT FK_Aromate_Variete
            FOREIGN KEY (VarieteId)
            REFERENCES Variete(IdVariete)
            ON DELETE CASCADE
            ON UPDATE NO ACTION
    );
END;
GO

-- ============================================================
-- TABLE : ProprieteMedicinale
-- ============================================================
IF OBJECT_ID(N'ProprieteMedicinale', N'U') IS NULL
BEGIN
    CREATE TABLE ProprieteMedicinale (
        IdPropriete INT IDENTITY(1,1) NOT NULL,
        NomPropriete NVARCHAR(50) NOT NULL,
        CONSTRAINT PK_ProprieteMedicinale PRIMARY KEY (IdPropriete)
    );
END;
GO

-- ============================================================
-- TABLE : AromatePropriete
-- ============================================================
IF OBJECT_ID(N'AromatePropriete', N'U') IS NULL
BEGIN
    CREATE TABLE AromatePropriete (
        AromateId INT NOT NULL,
        ProprieteId INT NOT NULL,
        CONSTRAINT PK_AromatePropriete PRIMARY KEY (AromateId, ProprieteId),
        CONSTRAINT FK_AromatePropriete_Aromate
            FOREIGN KEY (AromateId)
            REFERENCES Aromate(IdAromate)
            ON DELETE CASCADE,
        CONSTRAINT FK_AromatePropriete_ProprieteMedicinale
            FOREIGN KEY (ProprieteId)
            REFERENCES ProprieteMedicinale(IdPropriete)
            ON DELETE CASCADE
            ON UPDATE NO ACTION
    );
END;
GO

-- ============================================================
-- TABLE : Produit
-- ============================================================
IF OBJECT_ID(N'Produit', N'U') IS NULL
BEGIN
    CREATE TABLE Produit (
        IdProduit INT IDENTITY(1,1) NOT NULL,
        Intitule NVARCHAR(200) NOT NULL,
        PrixUnitaire DECIMAL(10, 2) NOT NULL,
        Quantite INT NOT NULL CONSTRAINT DF_Produit_Quantite DEFAULT 0,
        ImageProduit NVARCHAR(MAX) NULL,
        DateAjout DATETIME2(0) NOT NULL CONSTRAINT DF_Produit_DateAjout DEFAULT SYSDATETIME(),
        VarieteId INT NOT NULL,
        CategorieId INT NOT NULL,
        CONSTRAINT PK_Produit PRIMARY KEY (IdProduit),
        CONSTRAINT CK_Produit_PrixUnitaire CHECK (PrixUnitaire > 0),
        CONSTRAINT CK_Produit_Quantite CHECK (Quantite >= 0),
        CONSTRAINT FK_Produit_Variete
            FOREIGN KEY (VarieteId)
            REFERENCES Variete(IdVariete)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION,
        CONSTRAINT FK_Produit_Categorie
            FOREIGN KEY (CategorieId)
            REFERENCES Categorie(IdCategorie)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION
    );
END;
GO
