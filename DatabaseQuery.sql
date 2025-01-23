create database GestionRessourcesMateriellesDB;

-- Users and Departments
CREATE TABLE Users (
    Id int PRIMARY KEY,
    UserName NVARCHAR(256) NOT NULL,
    Email NVARCHAR(256),
    PasswordHash NVARCHAR(MAX),
    Role NVARCHAR(50) NOT NULL,
    DepartmentId NVARCHAR(450) NULL
);

CREATE TABLE Departments (
    Id int PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    ResponsableId int,
    FOREIGN KEY (ResponsableId) REFERENCES Users(Id)
);


-- Ressources
CREATE TABLE Ressources (
    Id int PRIMARY KEY,
    InventoryNumber NVARCHAR(100) UNIQUE NOT NULL,
    Type NVARCHAR(50) NOT NULL, -- 'Computer' or 'Printer'
    Brand NVARCHAR(100) NOT NULL,
    DepartmentId int,
    AssignedToUserId int,
    AcquisitionDate DATETIME NOT NULL,
    WarrantyEndDate DATETIME,
    Status NVARCHAR(50) NOT NULL, -- Active, UnderMaintenance, Disposed
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    FOREIGN KEY (AssignedToUserId) REFERENCES Users(Id)
);

CREATE TABLE Computers (
    RessourceId int PRIMARY KEY,
    CPU NVARCHAR(100) NOT NULL,
    RAM NVARCHAR(50) NOT NULL,
    HardDrive NVARCHAR(100) NOT NULL,
    Screen NVARCHAR(100) NOT NULL,
    FOREIGN KEY (RessourceId) REFERENCES Ressources(Id)
);

CREATE TABLE Printers (
    RessourceId int PRIMARY KEY,
    PrintSpeed NVARCHAR(50) NOT NULL,
    Resolution NVARCHAR(50) NOT NULL,
    FOREIGN KEY (RessourceId) REFERENCES Ressources(Id)
);

-- Procurement Process
CREATE TABLE RessourceRequests (
    Id int PRIMARY KEY,
    DepartementId int NOT NULL,
    RequestDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- Draft, Submitted, Approved, Rejected
    FOREIGN KEY (DepartementId) REFERENCES Departments(Id)
);

CREATE TABLE RessourceRequestItems (
    Id int PRIMARY KEY,
    ResourceRequestId int NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    Specifications NVARCHAR(MAX) NOT NULL,
    Quantity INT NOT NULL,
    AssignedToUserId int,
    FOREIGN KEY (ResourceRequestId) REFERENCES RessourceRequests(Id),
    FOREIGN KEY (AssignedToUserId) REFERENCES Users(Id)
);

CREATE TABLE Offres (
    Id int PRIMARY KEY,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Status NVARCHAR(50) NOT NULL -- Open, Closed, Awarded
);

CREATE TABLE Fournisseur (
    Id int PRIMARY KEY,
    CompanyName NVARCHAR(200) NOT NULL,
    Location NVARCHAR(200),
    Address NVARCHAR(500),
    Website NVARCHAR(200),
    ManagerName NVARCHAR(200),
    IsBlacklisted BIT DEFAULT 0,
    BlacklistReason NVARCHAR(MAX)
);

CREATE TABLE AppelOffres (
    Id int PRIMARY KEY,
    OffreId int NOT NULL,
    FournisseurId int NOT NULL,
    SubmissionDate DATETIME NOT NULL,
    ProposedDeliveryDate DATETIME NOT NULL,
    WarrantyMonths INT NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- Submitted, Accepted, Rejected
    FOREIGN KEY (OffreId) REFERENCES Offres(Id),
    FOREIGN KEY (FournisseurId) REFERENCES Fournisseur(Id)
);


CREATE TABLE AppelOffreItems (
    Id int PRIMARY KEY,
    AppelOffreId int NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    Marque NVARCHAR(100) NOT NULL,
    Specifications NVARCHAR(MAX) NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (AppelOffreId) REFERENCES AppelOffres(Id)
);

-- Maintenance
CREATE TABLE ConstatMaintenance (
    Id int PRIMARY KEY,
    ResourceId int NOT NULL,
    ReportedByUserId int NOT NULL,
    TechnicianId int,
    ReportDate DATETIME NOT NULL,
    IssueDescription NVARCHAR(MAX) NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- Reported, InProgress, Diagnosed, Resolved, RequiresReplacement
    FOREIGN KEY (ResourceId) REFERENCES Ressources(Id),
    FOREIGN KEY (ReportedByUserId) REFERENCES Users(Id),
    FOREIGN KEY (TechnicianId) REFERENCES Users(Id)
);

CREATE TABLE MaintenanceDiagnosis (
    Id int PRIMARY KEY,
    ConstatMaintenanceId int NOT NULL,
    DiagnosisDate DATETIME NOT NULL,
    ProblemDescription NVARCHAR(MAX) NOT NULL,
    Frequency NVARCHAR(50) NOT NULL, -- Rare, Frequent, Permanent
    IssueType NVARCHAR(50) NOT NULL, -- Software, Hardware
    RequiresReplacement BIT NOT NULL,
    FOREIGN KEY (ConstatMaintenanceId) REFERENCES ConstatMaintenance(Id)
);

-- Notifications and Messages
CREATE TABLE Notifications (
    Id int PRIMARY KEY,
    UserId int NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    IsRead BIT DEFAULT 0,
    Type NVARCHAR(50) NOT NULL, -- Info, Warning, Error, Success
    RelatedEntityId int,
    RelatedEntityType NVARCHAR(100),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE Messages (
    Id int PRIMARY KEY,
    SenderId int NOT NULL,
    ReceiverId int NOT NULL,
    Subject NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    SentDate DATETIME NOT NULL,
    IsRead BIT DEFAULT 0,
    FOREIGN KEY (SenderId) REFERENCES Users(Id),
    FOREIGN KEY (ReceiverId) REFERENCES Users(Id)
);