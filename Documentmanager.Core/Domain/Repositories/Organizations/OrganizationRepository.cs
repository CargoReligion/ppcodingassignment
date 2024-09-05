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

        public async Task<int> Create(Organization organization)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var sql = "INSERT INTO Organization (name, created_by, date_created, date_modified, modified_by) VALUES (@Name, @CreatedBy, @DateCreated, @DateModified, @ModifiedBy) RETURNING Id";
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
            var sql = "SELECT * FROM Organization WHERE Name = @Name";
            return await connection.QuerySingleOrDefaultAsync<Organization>(sql, new {Name = name});
        }
    }
}
