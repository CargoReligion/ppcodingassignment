using Documentmanager.Core.Domain.Models.Organizations;
using Documentmanager.Core.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documentmanager.Core.Domain.Repositories.Common;

namespace Documentmanager.Core.Domain.Repositories.Organizations
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly string _connectionString;

        public OrganizationRepository(IConfiguration config)
        {
            _connectionString = ConnectionInfo.BuildConnectionString(config);
        }

        public async Task<IEnumerable<Organization>> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Organization";
            return await connection.QueryAsync<Organization>(sql);
        }

        public async Task<Organization?> GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Organization WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Organization>(sql, new { Id = id });
        }

        public async Task<int> Create(Organization organization)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
            var sql = "INSERT INTO Organization (name, created_by, date_created, date_modified, modified_by) VALUES (@Name, @CreatedBy, @DateCreated, @DateModified, @ModifiedBy) RETURNING Id";
            return await connection.ExecuteScalarAsync<int>(sql, organization);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            
        }

        public async Task Delete(Organization organization)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "DELETE FROM Organization WHERE id = @Id";
            await connection.ExecuteAsync(sql, new {Id = organization.Id});
        }

        public async Task<int> Update(Organization organization)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "UPDATE Organization SET name = @Name, created_by = @CreatedBy, date_created = @DateCreated, date_modified = @DateModified, modified_by = @ModifiedBy WHERE id = @Id RETURNING Id";
            return await connection.ExecuteScalarAsync<int>(sql, organization);
        }

        public async Task<Organization?> GetByName(string name)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Organization WHERE Name = @Name";
            return await connection.QuerySingleOrDefaultAsync<Organization>(sql, new {Name = name});
        }
    }
}
