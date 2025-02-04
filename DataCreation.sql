-- 📄 Script de Création des Tables

-- ✅ Table Fournisseurs
CREATE TABLE Fournisseurs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CompanyName NVARCHAR(200) NOT NULL,
    IsBlacklisted BIT NOT NULL DEFAULT 0,
    BlacklistReason NVARCHAR(1000) NULL
);

-- ✅ Table Offres
CREATE TABLE Offres (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL
);

-- ✅ Table Users
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(256) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    Password NVARCHAR(256) NOT NULL, -- Ajout du mot de passe
    Role INT NOT NULL,               -- 0 = Admin, 1 = ResourceManager, 2 = DepartmentHead, 3 = Supplier, 4 = MaintenanceStaff
    SupplierId INT NULL,
    FOREIGN KEY (SupplierId) REFERENCES Fournisseurs(Id)
);

-- ✅ Table Departements
CREATE TABLE Departements (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nom NVARCHAR(256) NOT NULL,
    Code NVARCHAR(50) NOT NULL,
    ResponsableId INT NULL,
    FOREIGN KEY (ResponsableId) REFERENCES Users(Id)
);

-- ✅ Table Ressources
CREATE TABLE Ressources (
    Id INT PRIMARY KEY IDENTITY(1,1),
    InventoryNumber NVARCHAR(50) NOT NULL,
    Type NVARCHAR(100) NOT NULL,
    Brand NVARCHAR(100) NOT NULL,
    DepartmentId INT NULL,
    AssignedToUserId INT NULL,
    AcquisitionDate DATETIME NOT NULL,
    WarrantyEndDate DATETIME NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departements(Id),
    FOREIGN KEY (AssignedToUserId) REFERENCES Users(Id)
);

-- ✅ Table DemandeRessources
CREATE TABLE DemandeRessources (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DepartmentId INT NOT NULL,
    RequestDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departements(Id)
);

-- ✅ Table DemandeRessourceItems
CREATE TABLE DemandeRessourceItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ResourceRequestId INT NOT NULL,
    Type NVARCHAR(100) NOT NULL,
    Specifications NVARCHAR(1000) NOT NULL,
    Quantity INT NOT NULL,
    AssignedToUserId INT NULL,
    FOREIGN KEY (ResourceRequestId) REFERENCES DemandeRessources(Id),
    FOREIGN KEY (AssignedToUserId) REFERENCES Users(Id)
);

-- ✅ Table Notifications
CREATE TABLE Notifications (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmetteurId INT NOT NULL,
    DestinataireId INT NOT NULL,
    Titre NVARCHAR(256) NOT NULL,
    Corps NVARCHAR(1000) NOT NULL,
    Type INT NOT NULL,
    Statut INT NOT NULL,
    DateCreation DATETIME NOT NULL,
    DateLecture DATETIME NULL,
    FOREIGN KEY (EmetteurId) REFERENCES Users(Id),
    FOREIGN KEY (DestinataireId) REFERENCES Users(Id)
);

-- ✅ Table AppelOffres
CREATE TABLE AppelOffres (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OffreId INT NOT NULL,
    FournisseurId INT NOT NULL,
    SubmissionDate DATETIME NOT NULL,
    ProposedDeliveryDate DATETIME NOT NULL,
    WarrantyMonths INT NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (OffreId) REFERENCES Offres(Id),
    FOREIGN KEY (FournisseurId) REFERENCES Fournisseurs(Id)
);

-- ✅ Table AppelOffresItems
CREATE TABLE AppelOffresItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    AppelOffreID INT NOT NULL,
    Type NVARCHAR(100) NOT NULL,
    Brand NVARCHAR(100) NOT NULL,
    Specifications NVARCHAR(1000) NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (AppelOffreID) REFERENCES AppelOffres(Id)
);

-- ✅ Table ConstatMaintenances
CREATE TABLE ConstatMaintenances (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ResourceId INT NOT NULL,
    ReportedByUserId INT NOT NULL,
    TechnicianId INT NULL,
    ReportDate DATETIME NOT NULL,
    IssueDescription NVARCHAR(1000) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (ResourceId) REFERENCES Ressources(Id),
    FOREIGN KEY (ReportedByUserId) REFERENCES Users(Id),
    FOREIGN KEY (TechnicianId) REFERENCES Users(Id)
);

-- ✅ Table Computers
CREATE TABLE Computers (
    Id INT PRIMARY KEY,
    CPU NVARCHAR(100) NOT NULL,
    RAM NVARCHAR(100) NOT NULL,
    HardDrive NVARCHAR(100) NOT NULL,
    Screen NVARCHAR(100) NOT NULL,
    FOREIGN KEY (Id) REFERENCES Ressources(Id)
);

-- ✅ Table Imprimantes
CREATE TABLE Imprimantes (
    Id INT PRIMARY KEY,
    PrintSpeed NVARCHAR(100) NOT NULL,
    Resolution NVARCHAR(100) NOT NULL,
    FOREIGN KEY (Id) REFERENCES Ressources(Id)
);