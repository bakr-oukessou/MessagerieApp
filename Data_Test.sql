
Use GestionRessourcesMateriellesDB;

CREATE TABLE [dbo].[Offres] (
    [Id]        INT           NOT NULL,
    [StartDate] DATETIME      NOT NULL,
    [EndDate]   DATETIME      NOT NULL,
    [Status]    NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

Use GestionRessourcesMateriellesDB;
CREATE TABLE [dbo].[Users] (
    [Id]           INT            NOT NULL,
    [UserName]     NVARCHAR (256) NOT NULL,
    [Email]        NVARCHAR (256) NULL,
    [PasswordHash] NVARCHAR (MAX) NULL,
    [Role]         NVARCHAR (50)  NOT NULL,
    [DepartmentId] NVARCHAR (450) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Departments] (
    [Id]            INT            NOT NULL,
    [Name]          NVARCHAR (100) NOT NULL,
    [ResponsableId] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ResponsableId]) REFERENCES [dbo].[Users] ([Id])
);
CREATE TABLE [dbo].[RessourceRequests] (
    [Id]            INT           NOT NULL,
    [DepartementId] INT           NOT NULL,
    [RequestDate]   DATETIME      NOT NULL,
    [Status]        NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([DepartementId]) REFERENCES [dbo].[Departments] ([Id])
);

CREATE TABLE [dbo].[RessourceRequestItems] (
    [Id]                INT            NOT NULL,
    [ResourceRequestId] INT            NOT NULL,
    [Type]              NVARCHAR (50)  NOT NULL,
    [Specifications]    NVARCHAR (MAX) NOT NULL,
    [Quantity]          INT            NOT NULL,
    [AssignedToUserId]  INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ResourceRequestId]) REFERENCES [dbo].[RessourceRequests] ([Id]),
    FOREIGN KEY ([AssignedToUserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Ressources] (
    [Id]               INT            NOT NULL,
    [InventoryNumber]  NVARCHAR (100) NOT NULL,
    [Type]             NVARCHAR (50)  NOT NULL,
    [Brand]            NVARCHAR (100) NOT NULL,
    [DepartmentId]     INT            NULL,
    [AssignedToUserId] INT            NULL,
    [AcquisitionDate]  DATETIME       NOT NULL,
    [WarrantyEndDate]  DATETIME       NULL,
    [Status]           NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([InventoryNumber] ASC),
    FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([Id]),
    FOREIGN KEY ([AssignedToUserId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Printers] (
    [RessourceId] INT           NOT NULL,
    [PrintSpeed]  NVARCHAR (50) NOT NULL,
    [Resolution]  NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([RessourceId] ASC),
    FOREIGN KEY ([RessourceId]) REFERENCES [dbo].[Ressources] ([Id])
);
CREATE TABLE [dbo].[Fournisseur] (
    [Id]              INT            NOT NULL,
    [CompanyName]     NVARCHAR (200) NOT NULL,
    [Location]        NVARCHAR (200) NULL,
    [Address]         NVARCHAR (500) NULL,
    [Website]         NVARCHAR (200) NULL,
    [ManagerName]     NVARCHAR (200) NULL,
    [IsBlacklisted]   BIT            DEFAULT ((0)) NULL,
    [BlacklistReason] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[AppelOffres] (
    [Id]                   INT             NOT NULL,
    [OffreId]              INT             NOT NULL,
    [FournisseurId]        INT             NOT NULL,
    [SubmissionDate]       DATETIME        NOT NULL,
    [ProposedDeliveryDate] DATETIME        NOT NULL,
    [WarrantyMonths]       INT             NOT NULL,
    [TotalPrice]           DECIMAL (18, 2) NOT NULL,
    [Status]               NVARCHAR (50)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([OffreId]) REFERENCES [dbo].[Offres] ([Id]),
    FOREIGN KEY ([FournisseurId]) REFERENCES [dbo].[Fournisseur] ([Id])
);

CREATE TABLE [dbo].[AppelOffreItems] (
    [Id]             INT             NOT NULL,
    [AppelOffreId]   INT             NOT NULL,
    [Type]           NVARCHAR (50)   NOT NULL,
    [Marque]         NVARCHAR (100)  NOT NULL,
    [Specifications] NVARCHAR (MAX)  NOT NULL,
    [Quantity]       INT             NOT NULL,
    [UnitPrice]      DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AppelOffreId]) REFERENCES [dbo].[AppelOffres] ([Id])
);

CREATE TABLE [dbo].[Computers] (
    [RessourceId] INT            NOT NULL,
    [CPU]         NVARCHAR (100) NOT NULL,
    [RAM]         NVARCHAR (50)  NOT NULL,
    [HardDrive]   NVARCHAR (100) NOT NULL,
    [Screen]      NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([RessourceId] ASC),
    FOREIGN KEY ([RessourceId]) REFERENCES [dbo].[Ressources] ([Id])
);

CREATE TABLE [dbo].[ConstatMaintenance] (
    [Id]               INT            NOT NULL,
    [ResourceId]       INT            NOT NULL,
    [ReportedByUserId] INT            NOT NULL,
    [TechnicianId]     INT            NULL,
    [ReportDate]       DATETIME       NOT NULL,
    [IssueDescription] NVARCHAR (MAX) NOT NULL,
    [Status]           NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Ressources] ([Id]),
    FOREIGN KEY ([ReportedByUserId]) REFERENCES [dbo].[Users] ([Id]),
    FOREIGN KEY ([TechnicianId]) REFERENCES [dbo].[Users] ([Id])
);



CREATE TABLE [dbo].[MaintenanceDiagnosis] (
    [Id]                   INT            NOT NULL,
    [ConstatMaintenanceId] INT            NOT NULL,
    [DiagnosisDate]        DATETIME       NOT NULL,
    [ProblemDescription]   NVARCHAR (MAX) NOT NULL,
    [Frequency]            NVARCHAR (50)  NOT NULL,
    [IssueType]            NVARCHAR (50)  NOT NULL,
    [RequiresReplacement]  BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ConstatMaintenanceId]) REFERENCES [dbo].[ConstatMaintenance] ([Id])
);

CREATE TABLE [dbo].[Messages] (
    [Id]         INT            NOT NULL,
    [SenderId]   INT            NOT NULL,
    [ReceiverId] INT            NOT NULL,
    [Subject]    NVARCHAR (200) NOT NULL,
    [Content]    NVARCHAR (MAX) NOT NULL,
    [SentDate]   DATETIME       NOT NULL,
    [IsRead]     BIT            DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Users] ([Id]),
    FOREIGN KEY ([ReceiverId]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Notifications] (
    [Id]             INT            NOT NULL,
    [DestinataireId] INT            NOT NULL,
    [Titre]          NVARCHAR (200) NOT NULL,
    [Corps]          NVARCHAR (MAX) NOT NULL,
    [Type]           INT            NOT NULL,
    [Statut]         INT            DEFAULT ((0)) NOT NULL,
    [DateCreation]   DATETIME       NOT NULL,
    [DateLecture]    DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([DestinataireId]) REFERENCES [dbo].[Users] ([Id])
);


