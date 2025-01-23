using Dapper;
using MessagerieApp.Data;
using MessagerieApp.Models;
using System.Data.SqlClient;

namespace MessagerieApp.Repository
{
    public class RessourceRepository : IGenericRepository<Ressource>
    {
        private readonly string _connectionString;

        public RessourceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Ressource> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Fetch resource with additional type-specific details
            var query = @"
            SELECT r.*, 
                   c.CPU, c.RAM, c.HardDrive, c.Screen,
                   p.PrintSpeed, p.Resolution
            FROM Ressources r
            LEFT JOIN Computers c ON r.Id = c.RessourceId
            LEFT JOIN Printers p ON r.Id = p.RessourceId
            WHERE r.Id = @Id";

            var result = await connection.QueryFirstOrDefaultAsync<dynamic>(query, new { Id = id });

            if (result == null) return null;

            var ressource = new Ressource
            {
                Id = result.Id,
                InventoryNumber = result.InventoryNumber,
                Type = result.Type,
                Brand = result.Brand,
                DepartmentId = result.DepartmentId,
                AssignedToUserId = result.AssignedToUserId,
                AcquisitionDate = result.AcquisitionDate,
                WarrantyEndDate = result.WarrantyEndDate,
                Status = result.Status
            };

            // Add type-specific details
            if (ressource.Type == "Computer")
            {
                ressource.ComputerDetails = new Computer
                {
                    CPU = result.CPU,
                    RAM = result.RAM,
                    HardDrive = result.HardDrive,
                    Screen = result.Screen
                };
            }
            else if (ressource.Type == "Printer")
            {
                ressource.ImprimanteDetails = new Imprimante
                {
                    PrintSpeed = result.PrintSpeed,
                    Resolution = result.Resolution
                };
            }

            return ressource;
        }

        public async Task<IEnumerable<Ressource>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"
            SELECT r.*, 
                   c.CPU, c.RAM, c.HardDrive, c.Screen,
                   p.PrintSpeed, p.Resolution
            FROM Ressources r
            LEFT JOIN Computers c ON r.Id = c.RessourceId
            LEFT JOIN Printers p ON r.Id = p.RessourceId";

            var results = await connection.QueryAsync<dynamic>(query);

            var ressources = new List<Ressource>();
            foreach (var result in results)
            {
                var ressource = new Ressource
                {
                    Id = result.Id,
                    InventoryNumber = result.InventoryNumber,
                    Type = result.Type,
                    Brand = result.Brand,
                    DepartmentId = result.DepartmentId,
                    AssignedToUserId = result.AssignedToUserId,
                    AcquisitionDate = result.AcquisitionDate,
                    WarrantyEndDate = result.WarrantyEndDate,
                    Status = result.Status
                };

                // Add type-specific details
                if (ressource.Type == "Computer")
                {
                    ressource.ComputerDetails = new Computer
                    {
                        CPU = result.CPU,
                        RAM = result.RAM,
                        HardDrive = result.HardDrive,
                        Screen = result.Screen
                    };
                }
                else if (ressource.Type == "Printer")
                {
                    ressource.ImprimanteDetails = new Imprimante
                    {
                        PrintSpeed = result.PrintSpeed,
                        Resolution = result.Resolution
                    };
                }

                ressources.Add(ressource);
            }

            return ressources;
        }

        public async Task<int> AddAsync(Ressource entity)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Insert into Ressources table
                var ressourceQuery = @"
                INSERT INTO Ressources 
                (InventoryNumber, Type, Brand, DepartmentId, AssignedToUserId, 
                AcquisitionDate, WarrantyEndDate, Status)
                VALUES 
                (@InventoryNumber, @Type, @Brand, @DepartmentId, @AssignedToUserId, 
                @AcquisitionDate, @WarrantyEndDate, @Status);
                SELECT SCOPE_IDENTITY();";

                var ressourceId = await connection.ExecuteScalarAsync<int>(ressourceQuery, entity, transaction);

                // Insert type-specific details
                if (entity.Type == "Computer" && entity.ComputerDetails != null)
                {
                    var computerQuery = @"
                    INSERT INTO Computers 
                    (RessourceId, CPU, RAM, HardDrive, Screen)
                    VALUES 
                    (@RessourceId, @CPU, @RAM, @HardDrive, @Screen)";

                    await connection.ExecuteAsync(computerQuery, new
                    {
                        RessourceId = ressourceId,
                        entity.ComputerDetails.CPU,
                        entity.ComputerDetails.RAM,
                        entity.ComputerDetails.HardDrive,
                        entity.ComputerDetails.Screen
                    }, transaction);
                }
                else if (entity.Type == "Printer" && entity.ImprimanteDetails != null)
                {
                    var printerQuery = @"
                    INSERT INTO Printers 
                    (RessourceId, PrintSpeed, Resolution)
                    VALUES 
                    (@RessourceId, @PrintSpeed, @Resolution)";

                    await connection.ExecuteAsync(printerQuery, new
                    {
                        RessourceId = ressourceId,
                        entity.ImprimanteDetails.PrintSpeed,
                        entity.ImprimanteDetails.Resolution
                    }, transaction);
                }

                transaction.Commit();
                return ressourceId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Ressource entity)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Update Ressources table
                var ressourceQuery = @"
                UPDATE Ressources 
                SET InventoryNumber = @InventoryNumber, 
                    Type = @Type, 
                    Brand = @Brand, 
                    DepartmentId = @DepartmentId, 
                    AssignedToUserId = @AssignedToUserId, 
                    AcquisitionDate = @AcquisitionDate, 
                    WarrantyEndDate = @WarrantyEndDate, 
                    Status = @Status
                WHERE Id = @Id";

                await connection.ExecuteAsync(ressourceQuery, entity, transaction);

                // Update type-specific details
                if (entity.Type == "Computer" && entity.ComputerDetails != null)
                {
                    var computerQuery = @"
                    UPDATE Computers 
                    SET CPU = @CPU, 
                        RAM = @RAM, 
                        HardDrive = @HardDrive, 
                        Screen = @Screen
                    WHERE RessourceId = @RessourceId";

                    await connection.ExecuteAsync(computerQuery, new
                    {
                        RessourceId = entity.Id,
                        entity.ComputerDetails.CPU,
                        entity.ComputerDetails.RAM,
                        entity.ComputerDetails.HardDrive,
                        entity.ComputerDetails.Screen
                    }, transaction);
                }
                else if (entity.Type == "Printer" && entity.ImprimanteDetails != null)
                {
                    var printerQuery = @"
                    UPDATE Printers 
                    SET PrintSpeed = @PrintSpeed, 
                        Resolution = @Resolution
                    WHERE RessourceId = @RessourceId";

                    await connection.ExecuteAsync(printerQuery, new
                    {
                        RessourceId = entity.Id,
                        entity.ImprimanteDetails.PrintSpeed,
                        entity.ImprimanteDetails.Resolution
                    }, transaction);
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Delete type-specific details first
                await connection.ExecuteAsync("DELETE FROM Computers WHERE RessourceId = @Id", new { Id = id }, transaction);
                await connection.ExecuteAsync("DELETE FROM Printers WHERE RessourceId = @Id", new { Id = id }, transaction);

                // Then delete from main Ressources table
                var deleteQuery = "DELETE FROM Ressources WHERE Id = @Id";
                await connection.ExecuteAsync(deleteQuery, new { Id = id }, transaction);

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
