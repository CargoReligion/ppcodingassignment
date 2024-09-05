using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentmanager.Core.Domain.Repositories.Common
{
    public class ConnectionInfo
    {
        public static string BuildConnectionString(IConfiguration config)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Database = config.GetValue("DB", "panther"),
                Host = config.GetValue("HOST", "localhost"),
                Port = config.GetValue("DB_PORT", 5432),
                Username = config.GetValue("DB_USERNAME", "postgres"),
                Password = config.GetValue("DB_PASSWORD", "admin"),
                Enlist = true
            };
            return builder.ToString();
        }
    }
}
