using Dapper;
using MessagerieApp.Models;
using System.Data.SqlClient;

namespace MessagerieApp.Repository
{
    public class OffreRepository : IOffreRepository
    {
        private readonly string _connectionString;

        public OffreRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Offre> GetByIdAsync(int id, bool includeDetails = false)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Base Offre query
            var query = @"
            SELECT * FROM Offres 
            WHERE Id = @Id";

            var offre = await connection.QueryFirstOrDefaultAsync<Offre>(query, new { Id = id });

            if (offre != null && includeDetails)
            {
                // Fetch associated AppelOffres
                var appelOffresQuery = @"
                SELECT ao.*, f.CompanyName, f.Location 
                FROM AppelOffres ao
                JOIN Fournisseur f ON ao.FournisseurId = f.Id
                WHERE ao.OffreId = @OffreId"
                ;
                offre.Offres = (await connection.QueryAsync<AppelOffres, Supplier, AppelOffres>(
                appelOffresQuery,
                    (appelOffre, fournisseur) => {
                        appelOffre.Fournisseur = fournisseur;
                        return appelOffre;
                    },
                    new { OffreId = id },
                    splitOn: "CompanyName"
                )).ToList();

                // Fetch items for each AppelOffre
                foreach (var appelOffre in offre.Offres)
                {
                    var itemsQuery = @"
                    SELECT * FROM AppelOffreItems 
                    WHERE AppelOffreId = @AppelOffreId"
                    ;

                    appelOffre.Items = (await connection.QueryAsync<AppelOffresItem>(
                        itemsQuery,
                        new { AppelOffreId = appelOffre.Id }
                    )).ToList();
                }
            }

            return offre;
        }

        public async Task<IEnumerable<Offre>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.QueryAsync<Offre>("SELECT * FROM Offres");
        }

        public async Task<int> CreateOffreAsync(Offre offre)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Insert Offre
                var offreQuery = @"
                INSERT INTO Offres 
                (StartDate, EndDate, Status) 
                VALUES 
                (@StartDate, @EndDate, @Status);
                SELECT SCOPE_IDENTITY();";

                var offreId = await connection.ExecuteScalarAsync<int>(offreQuery, offre, transaction);

                // Insert AppelOffres if any
                if (offre.Offres != null)
                {
                    foreach (var appelOffre in offre.Offres)
                    {
                        appelOffre.OffreId = offreId;
                        var appelOffreQuery = @"
                        INSERT INTO AppelOffres 
                        (OffreId, FournisseurId, SubmissionDate, 
                        ProposedDeliveryDate, WarrantyMonths, TotalPrice, Status) 
                        VALUES 
                        (@OffreId, @FournisseurId, @SubmissionDate, 
                        @ProposedDeliveryDate, @WarrantyMonths, @TotalPrice, @Status);
                        SELECT SCOPE_IDENTITY();";

                        var appelOffreId = await connection.ExecuteScalarAsync<int>(appelOffreQuery, appelOffre, transaction);

                        // Insert AppelOffreItems
                        if (appelOffre.Items != null)
                        {
                            foreach (var item in appelOffre.Items)
                            {
                                item.AppelOffreID = appelOffreId;
                                var itemQuery = @"
                                INSERT INTO AppelOffreItems 
                                (AppelOffreId, Type, Marque, Specifications, 
                                Quantity, UnitPrice) 
                                VALUES 
                                (@AppelOffreId, @Type, @Marque, @Specifications, 
                                @Quantity, @UnitPrice)";

                                await connection.ExecuteAsync(itemQuery, item, transaction);
                            }
                        }
                    }
                }

                transaction.Commit();
                return offreId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> UpdateOffreAsync(Offre offre)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Update Offre
                var offreQuery = @"
                UPDATE Offres 
                SET StartDate = @StartDate, 
                    EndDate = @EndDate, 
                    Status = @Status 
                WHERE Id = @Id";

                await connection.ExecuteAsync(offreQuery, offre, transaction);

                // Delete existing AppelOffres and Items
                await connection.ExecuteAsync(
                    "DELETE FROM AppelOffreItems WHERE AppelOffreId IN (SELECT Id FROM AppelOffres WHERE OffreId = @OffreId)",
                    new { OffreId = offre.Id },
                    transaction
                );
                await connection.ExecuteAsync(
                    "DELETE FROM AppelOffres WHERE OffreId = @OffreId",
                    new { OffreId = offre.Id },
                    transaction
                );

                // Reinsert AppelOffres
                if (offre.Offres != null)
                {
                    foreach (var appelOffre in offre.Offres)
                    {
                        appelOffre.OffreId = offre.Id;
                        var appelOffreQuery = @"
                        INSERT INTO AppelOffres 
                        (OffreId, FournisseurId, SubmissionDate, 
                        ProposedDeliveryDate, WarrantyMonths, TotalPrice, Status) 
                        VALUES 
                        (@OffreId, @FournisseurId, @SubmissionDate, 
                        @ProposedDeliveryDate, @WarrantyMonths, @TotalPrice, @Status);
                        SELECT SCOPE_IDENTITY();";

                        var appelOffreId = await connection.ExecuteScalarAsync<int>(appelOffreQuery, appelOffre, transaction);

                        // Insert AppelOffreItems
                        if (appelOffre.Items != null)
                        {
                            foreach (var item in appelOffre.Items)
                            {
                                item.AppelOffreID = appelOffreId;
                                var itemQuery = @"
                                INSERT INTO AppelOffreItems 
                                (AppelOffreId, Type, Marque, Specifications, 
                                Quantity, UnitPrice) 
                                VALUES 
                                (@AppelOffreId, @Type, @Marque, @Specifications, 
                                @Quantity, @UnitPrice)";

                                await connection.ExecuteAsync(itemQuery, item, transaction);
                            }
                        }
                    }
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

        public async Task<bool> DeleteOffreAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Delete AppelOffreItems
                await connection.ExecuteAsync(
                    "DELETE FROM AppelOffreItems WHERE AppelOffreId IN (SELECT Id FROM AppelOffres WHERE OffreId = @OffreId)",
                    new { OffreId = id },
                    transaction
                );

                // Delete AppelOffres
                await connection.ExecuteAsync(
                    "DELETE FROM AppelOffres WHERE OffreId = @OffreId",
                    new { OffreId = id },
                    transaction
                );

                // Delete Offre
                await connection.ExecuteAsync(
                    "DELETE FROM Offres WHERE Id = @Id",
                    new { Id = id },
                    transaction
                );

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<AppelOffres>> GetAppelOffresByOffreIdAsync(int offreId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"
            SELECT ao.*, f.CompanyName, f.Location 
            FROM AppelOffres ao
            JOIN Fournisseur f ON ao.FournisseurId = f.Id
            WHERE ao.OffreId = @OffreId"
            ;
            var appelOffres = await connection.QueryAsync<AppelOffres, Supplier, AppelOffres>(
                query,
                (appelOffre, fournisseur) => {
                    appelOffre.Fournisseur = fournisseur;
                    return appelOffre;
                },
                new { OffreId = offreId },
                splitOn: "CompanyName"
            );

            // Fetch items for each AppelOffre
            foreach (var appelOffre in appelOffres)
            {
                var itemsQuery = @"
                SELECT * FROM AppelOffreItems 
                WHERE AppelOffreId = @AppelOffreId";

                appelOffre.Items = (await connection.QueryAsync<AppelOffresItem>(
                    itemsQuery,
                    new { AppelOffreId = appelOffre.Id }
                )).ToList();
            }

            return appelOffres;
        }

    }
}
