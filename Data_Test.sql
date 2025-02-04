-- 🚀 Script d'Insertion des Données

-- ✅ Insertion des Fournisseurs
INSERT INTO Fournisseurs (CompanyName, IsBlacklisted, BlacklistReason) VALUES
('TechCorp', 0, NULL),
('GlobalSupplies', 1, 'Non-conformité des livraisons'),
('EcoSolutions', 0, NULL);

-- ✅ Insertion des Users
INSERT INTO Users (UserName, Email, Password, Role, SupplierId) VALUES
('admin', 'admin@system.com', 'AdminPass123', 0, NULL),
('rmanager', 'rmanager@company.com', 'ResManPass456', 1, NULL),
('dhead', 'dhead@dept.com', 'DeptHeadPass789', 2, NULL),
('supplier_user', 'supplier@techcorp.com', 'SuppPass321', 3, 1),
('tech_guy', 'tech@maintenance.com', 'TechPass654', 4, NULL);

-- ✅ Insertion des Départements
INSERT INTO Departements (Nom, Code, ResponsableId) VALUES
('Informatique', 'IT001', 3),
('Logistique', 'LG002', NULL);

-- ✅ Insertion des Ressources
INSERT INTO Ressources (InventoryNumber, Type, Brand, DepartmentId, AssignedToUserId, AcquisitionDate, WarrantyEndDate, Status) VALUES
('INV1001', 'Ordinateur', 'Dell', 1, 2, '2023-01-15', '2025-01-15', 'Actif'),
('INV1002', 'Imprimante', 'HP', 2, NULL, '2022-06-20', NULL, 'En Réparation');

-- ✅ Insertion des Demandes de Ressources
INSERT INTO DemandeRessources (DepartmentId, RequestDate, Status) VALUES
(1, '2024-08-01', 'En Attente'),
(2, '2024-08-05', 'Approuvée');

-- ✅ Insertion des Items de Demandes de Ressources
INSERT INTO DemandeRessourceItems (ResourceRequestId, Type, Specifications, Quantity, AssignedToUserId) VALUES
(1, 'Laptop', '16GB RAM, 512GB SSD', 5, NULL),
(2, 'Scanner', 'Haute résolution', 2, 4);

-- ✅ Insertion des Offres
INSERT INTO Offres (StartDate, EndDate, Status) VALUES
('2024-07-01', '2024-12-31', 'Ouverte'),
('2023-01-01', '2023-06-30', 'Fermée');

-- ✅ Insertion des Appels d'Offres
INSERT INTO AppelOffres (OffreId, FournisseurId, SubmissionDate, ProposedDeliveryDate, WarrantyMonths, TotalPrice, Status) VALUES
(1, 1, '2024-08-10', '2024-09-15', 24, 15000.00, 'En Cours'),
(2, 2, '2023-02-15', '2023-03-20', 12, 8000.00, 'Attribué');

-- ✅ Insertion des Items des Appels d'Offres
INSERT INTO AppelOffresItems (AppelOffreID, Type, Brand, Specifications, Quantity, UnitPrice) VALUES
(1, 'Ordinateur Portable', 'Lenovo', 'Core i7, 16GB RAM', 10, 1200.00),
(2, 'Projecteur', 'Epson', 'Full HD, 3000 lumens', 5, 400.00);

-- ✅ Insertion des Notifications
INSERT INTO Notifications (EmetteurId, DestinataireId, Titre, Corps, Type, Statut, DateCreation, DateLecture) VALUES
(1, 2, 'Ressource Assignée', 'Une nouvelle ressource vous a été assignée.', 1, 0, '2024-08-15', NULL),
(3, 1, 'Demande Approuvée', 'Votre demande de ressource a été approuvée.', 2, 1, '2024-08-16', '2024-08-17');

-- ✅ Insertion des Constats de Maintenance
INSERT INTO ConstatMaintenances (ResourceId, ReportedByUserId, TechnicianId, ReportDate, IssueDescription, Status) VALUES
(2, 2, 5, '2024-08-20', 'L''imprimante ne s''allume plus.', 'En Cours');

-- ✅ Insertion des Détails pour Ordinateurs
INSERT INTO Computers (Id, CPU, RAM, HardDrive, Screen) VALUES
(1, 'Intel i5', '8GB', '256GB SSD', '15 pouces');

-- ✅ Insertion des Détails pour Imprimantes
INSERT INTO Imprimantes (Id, PrintSpeed, Resolution) VALUES
(2, '20 ppm', '1200x1200 dpi');
