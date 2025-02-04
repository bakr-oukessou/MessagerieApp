CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(256) NOT NULL,
    Email NVARCHAR(256) NOT NULL,
    Role INT NOT NULL,
    SupplierId INT NULL,
    FOREIGN KEY (SupplierId) REFERENCES Fournisseurs(Id) -- Fournisseurs existe maintenant
);