CREATE DATABASE IF NOT EXISTS graines
    CHARACTER SET utf8mb4
    COLLATE 'utf8mb4_0900_as_cs';

USE graines;

CREATE TABLE IF NOT EXISTS Localite (
    IdLocalite INT NOT NULL AUTO_INCREMENT,
    CodePostal VARCHAR(20) NOT NULL,
    NomLocalite VARCHAR(200) NOT NULL,
    CONSTRAINT PK_Localite PRIMARY KEY (IdLocalite),
    CONSTRAINT UQ_Localite_CodePostal_Localite UNIQUE (CodePostal, NomLocalite)
);

CREATE TABLE IF NOT EXISTS Utilisateur (
    IdUtilisateur INT NOT NULL AUTO_INCREMENT,
    Nom VARCHAR(40) NOT NULL,
    Prenom VARCHAR(40) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    MotDePasseHash LONGTEXT NOT NULL,
    MotDePasseSalt LONGBLOB NOT NULL,
    DateInscription DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Actif BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT PK_Utilisateur PRIMARY KEY (IdUtilisateur),
    CONSTRAINT UQ_Utilisateur_Email UNIQUE (Email),
    CONSTRAINT CK_Utilisateur_Actif CHECK (Actif IN (0, 1))
);

CREATE TABLE IF NOT EXISTS AdresseLivraison (
    IdAdresse INT NOT NULL AUTO_INCREMENT,
    Rue VARCHAR(255) NOT NULL,
    Numero VARCHAR(50) NOT NULL,
    ParDefaut BOOLEAN NOT NULL DEFAULT FALSE,
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
);

CREATE TABLE IF NOT EXISTS Role (
    IdRole INT NOT NULL AUTO_INCREMENT,
    NomRole VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Role PRIMARY KEY (IdRole),
    CONSTRAINT UQ_Role_NomRole UNIQUE (NomRole)
);

CREATE TABLE IF NOT EXISTS UtilisateurRole (
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

CREATE TABLE IF NOT EXISTS Categorie (
    IdCategorie INT NOT NULL AUTO_INCREMENT,
    NomCategorie VARCHAR(50) NOT NULL,
    Descriptif LONGTEXT NULL,
    CONSTRAINT PK_Categorie PRIMARY KEY (IdCategorie),
    CONSTRAINT UQ_Categorie_NomCategorie UNIQUE (NomCategorie)
);

CREATE TABLE IF NOT EXISTS Espece (
    IdEspece INT NOT NULL AUTO_INCREMENT,
    NomScientifique VARCHAR(100) NOT NULL,
    NomCommun VARCHAR(100) NOT NULL,
    CONSTRAINT PK_Espece PRIMARY KEY (IdEspece),
    CONSTRAINT UQ_Espece_NomScientifique_NomCommun UNIQUE (NomScientifique, NomCommun)
);

CREATE TABLE IF NOT EXISTS Variete (
    IdVariete INT NOT NULL AUTO_INCREMENT,
    Nom VARCHAR(255) NOT NULL,
    Descriptif LONGTEXT NULL,
    Bio BOOLEAN NOT NULL DEFAULT TRUE,
    CycleJours INT NULL,
    CouleurLegume VARCHAR(50) NULL,
    TailleFixeLegume DOUBLE NULL,
    TailleMinLegume DOUBLE NULL,
    TailleMaxLegume DOUBLE NULL,
    EspacementEntreLesPlants INT NULL,
    EspacementEntreLesLignes INT NULL,
    TypeEnsoleillement VARCHAR(30) NULL,
    TypeFeuillage VARCHAR(50) NULL,
    HauteurAdulteMin INT NULL,
    HauteurAdulteMax INT NULL,
    DureeDeGermination VARCHAR(30) NULL,
    TemperatureMinDeGermination INT NULL,
    CycleDeVie VARCHAR(20) NULL,
    RusticitePlante VARCHAR(100) NULL,
    DateSemisMin VARCHAR(40) NULL,
    DateSemisMax VARCHAR(40) NULL,
    DureeAvantRecolte VARCHAR(200) NULL,
    TypeDeSol VARCHAR(150) NULL,
    ConseilPlantation LONGTEXT NULL,
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
        CycleDeVie IS NULL OR CycleDeVie IN ('annuelle', 'bisannuelle', 'vivace')
    ),
    CONSTRAINT FK_Variete_Espece
        FOREIGN KEY (EspeceId)
        REFERENCES Espece(IdEspece)
);

CREATE TABLE IF NOT EXISTS Aromate (
    IdAromate INT NOT NULL AUTO_INCREMENT,
    PartieUtilisee VARCHAR(50) NULL,
    Propriete LONGTEXT NULL,
    UsageCulinaire LONGTEXT NULL,
    VarieteId INT NOT NULL,
    CONSTRAINT PK_Aromate PRIMARY KEY (IdAromate),
    CONSTRAINT FK_Aromate_Variete
        FOREIGN KEY (VarieteId)
        REFERENCES Variete(IdVariete)
        ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ProprieteMedicinale (
    IdPropriete INT NOT NULL AUTO_INCREMENT,
    NomPropriete VARCHAR(50) NOT NULL,
    CONSTRAINT PK_ProprieteMedicinale PRIMARY KEY (IdPropriete)
);

CREATE TABLE IF NOT EXISTS AromatePropriete (
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
);

CREATE TABLE IF NOT EXISTS Produit (
    IdProduit INT NOT NULL AUTO_INCREMENT,
    Intitule VARCHAR(200) NOT NULL,
    PrixUnitaire DECIMAL(10, 2) NOT NULL,
    Quantite INT NOT NULL DEFAULT 0,
    ImageProduit LONGTEXT NULL,
    DateAjout DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    VarieteId INT NOT NULL,
    CategorieId INT NOT NULL,

    CONSTRAINT PK_Produit PRIMARY KEY (IdProduit),
    CONSTRAINT CK_Produit_PrixUnitaire CHECK (PrixUnitaire > 0),
    CONSTRAINT CK_Produit_Quantite CHECK (Quantite >= 0),
    CONSTRAINT FK_Produit_Variete
        FOREIGN KEY (VarieteId)
        REFERENCES Variete(IdVariete),
    CONSTRAINT FK_Produit_Categorie
        FOREIGN KEY (CategorieId)
        REFERENCES Categorie(IdCategorie)
);