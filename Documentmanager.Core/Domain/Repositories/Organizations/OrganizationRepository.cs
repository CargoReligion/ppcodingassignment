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

namespace Documentmanager.Core.Domain.Repositories.Organizations
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly string _connectionString;

        public OrganizationRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<int> Create(Organization organization)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = @"
                INSERT INTO Organization (Id, Name)
                VALUES (@Id, @Name)
                RETURNING Id";
            return await connection.ExecuteScalarAsync<int>(sql, organization);
        }

        public Task Delete(Organization t)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Organization t)
        {
            throw new NotImplementedException();
        }

        public async Task<Organization> GetByName(string name)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "SELECT * FROM Organization WHERE Name = @name";
            return await connection.QuerySingleAsync<Organization>(sql, name);
        }
    }
}
