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
using Documentmanager.Core.Domain.Models.Users;

namespace Documentmanager.Core.Domain.Repositories.Organizations
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            _connectionString = ConnectionInfo.BuildConnectionString(config);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM dsuser";
            return await connection.QueryAsync<User>(sql);
        }

        public async Task<User?> GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM dsuser WHERE id = @Id";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<int> Create(User user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "INSERT INTO dsuser (email, created_by, date_created, date_modified, modified_by) VALUES (@Email, @CreatedBy, @DateCreated, @DateModified, @ModifiedBy) RETURNING Id";
            return await connection.ExecuteScalarAsync<int>(sql, user);
        }

        public async Task Delete(User user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "DELETE FROM dsuser WHERE id = @Id";
            await connection.ExecuteAsync(sql, new {Id = user.Id});
        }

        public async Task<int> Update(User user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "UPDATE dsuser SET email = @Email, organization_id = @OrganizationId, created_by = @CreatedBy, date_created = @DateCreated, date_modified = @DateModified, modified_by = @ModifiedBy WHERE id = @Id RETURNING Id";
            return await connection.ExecuteScalarAsync<int>(sql, user);
        }

        public async Task<User?> GetByEmail(string email)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM dsuser WHERE Email = @Email";
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new {Email = email});
        }
    }
}
