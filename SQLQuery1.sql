USE GestionRessourcesMateriellesDB;
GO

-- Table des Départements
CREATE TABLE Departements (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nom NVARCHAR(100) NOT NULL,
    Code NVARCHAR(50) UNIQUE NOT NULL,
    ResponsableId INT
);

-- Table des Utilisateurs
CREATE TABLE Utilisateurs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nom NVARCHAR(100) NOT NULL,
    Prenom NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) UNIQUE NOT NULL,
    MotDePasse NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    DepartementId INT,
    FOREIGN KEY (DepartementId) REFERENCES Departements(Id)
);

-- Mise à jour de la table Départements avec la clé étrangère
ALTER TABLE Departements
ADD FOREIGN KEY (ResponsableId) REFERENCES Utilisateurs(Id);

-- Table des Ressources
CREATE TABLE Ressources (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nom NVARCHAR(100) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    Marque NVARCHAR(100),
    NumeroSerie NVARCHAR(100),
    DateAcquisition DATETIME NOT NULL,
    EtatRessource NVARCHAR(50) NOT NULL, -- Neuf, Bon état, Usé, En panne
    DepartementId INT,
    UtilisateurResponsableId INT,
    FOREIGN KEY (DepartementId) REFERENCES Departements(Id),
    FOREIGN KEY (UtilisateurResponsableId) REFERENCES Utilisateurs(Id)
);

-- Table des Notifications
CREATE TABLE Notifications (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmetteurId INT NOT NULL,
    DestinataireId INT NOT NULL,
    Titre NVARCHAR(200) NOT NULL,
    Corps NVARCHAR(MAX) NOT NULL,
    Type INT NOT NULL, -- Type de notification (enum)
    Statut INT NOT NULL, -- Statut de la notification (enum)
    DateCreation DATETIME NOT NULL,
    DateLecture DATETIME,
    FOREIGN KEY (EmetteurId) REFERENCES Utilisateurs(Id),
    FOREIGN KEY (DestinataireId) REFERENCES Utilisateurs(Id)
);

-- Table des Appels d'Offres
CREATE TABLE AppelsOffres (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titre NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    DateDebut DATETIME NOT NULL,
    DateFin DATETIME NOT NULL,
    Statut INT NOT NULL, -- En cours, Cloturé, Annulé
    ResponsableId INT NOT NULL,
    FOREIGN KEY (ResponsableId) REFERENCES Utilisateurs(Id)
);

-- Table des Propositions Fournisseurs
CREATE TABLE PropositionsFournisseurs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    AppelOffreId INT NOT NULL,
    FournisseurId INT NOT NULL,
    Prix DECIMAL(18,2) NOT NULL,
    Details NVARCHAR(MAX),
    Statut INT NOT NULL, -- En attente, Accepté, Rejeté
    DateSoumission DATETIME NOT NULL,
    FOREIGN KEY (AppelOffreId) REFERENCES AppelsOffres(Id),
    FOREIGN KEY (FournisseurId) REFERENCES Utilisateurs(Id)
);

-- Table des Demandes de Maintenance
CREATE TABLE DemandesMaintenance (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RessourceId INT NOT NULL,
    UtilisateurId INT NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    DateSignalement DATETIME NOT NULL,
    Statut INT NOT NULL, -- Nouveau, En cours, Résolu, Fermé
    PrioriteId INT NOT NULL,
    FOREIGN KEY (RessourceId) REFERENCES Ressources(Id),
    FOREIGN KEY (UtilisateurId) REFERENCES Utilisateurs(Id)
);

-- Table des Interventions de Maintenance
CREATE TABLE InterventionsMaintenance (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DemandeMaintenanceId INT NOT NULL,
    TechnicienId INT NOT NULL,
    DateIntervention DATETIME NOT NULL,
    Diagnostic NVARCHAR(MAX),
    ActionRealisee NVARCHAR(MAX),
    Statut INT NOT NULL, -- Diagnostiqué, Réparé, Nécessite Remplacement
    FOREIGN KEY (DemandeMaintenanceId) REFERENCES DemandesMaintenance(Id),
    FOREIGN KEY (TechnicienId) REFERENCES Utilisateurs(Id)
);

-- Création d'index pour optimiser les performances
CREATE INDEX IX_Ressources_DepartementId ON Ressources(DepartementId);
CREATE INDEX IX_Notifications_DestinataireId ON Notifications(DestinataireId);
CREATE INDEX IX_Notifications_DateCreation ON Notifications(DateCreation);
CREATE INDEX IX_DemandesMaintenance_Statut ON DemandesMaintenance(Statut);

-- Exemple de données pour les rôles
INSERT INTO Utilisateurs (Nom, Prenom, Email, MotDePasse, Role) VALUES 
('Admin', 'Système', 'admin@etablissement.com', 'MotDePasse123', 'Administrateur'),
('Responsable', 'Ressources', 'responsable@etablissement.com', 'MotDePasse123', 'ResponsableRessources');

-- Notes supplémentaires
-- Les mots de passe doivent être hachés en production
-- Utiliser des contraintes et des triggers si nécessaire pour la cohérence des données