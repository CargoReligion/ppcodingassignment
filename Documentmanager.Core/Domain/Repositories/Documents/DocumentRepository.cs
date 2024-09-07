using Documentmanager.Core.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using Documentmanager.Core.Domain.Repositories.Common;
using Documentmanager.Core.Domain.Models.Users;
using Documentmanager.Core.Domain.Models.Document;

namespace Documentmanager.Core.Domain.Repositories.Users
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly string _connectionString;

        public DocumentRepository(IConfiguration config)
        {
            _connectionString = ConnectionInfo.BuildConnectionString(config);
        }

        public async Task<IEnumerable<Document>> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM document";
            return await connection.QueryAsync<Document>(sql);
        }

        public async Task<Document?> GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM document WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Document>(sql, new { Id = id });
        }

        public async Task<int> Create(Document document)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "INSERT INTO document (name, storage_path, user_id, organization_id, created_by, date_created, date_modified, modified_by) VALUES (@Name, @StoragePath, @UserId, @OrganizationId, @CreatedBy, @DateCreated, @DateModified, @ModifiedBy) RETURNING Id";
            return await connection.ExecuteScalarAsync<int>(sql, document);
        }

        public async Task Delete(Document document)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "DELETE FROM document WHERE id = @Id";
            await connection.ExecuteAsync(sql, new {Id = document.Id});
        }

        public async Task<int> Update(Document document)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "UPDATE document SET name = @Name, storage_path = @StoragePath, user_id = @UserId, organization_id = @OrganizationId, created_by = @CreatedBy, date_created = @DateCreated, date_modified = @DateModified, modified_by = @ModifiedBy WHERE id = @Id RETURNING Id";
            return await connection.ExecuteScalarAsync<int>(sql, document);
        }
    }
}
