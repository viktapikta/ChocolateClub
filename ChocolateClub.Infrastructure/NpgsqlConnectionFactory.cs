using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ChocolateClub.Infrastructure
{
    public interface INpgsqlConnectionFactory
    {
        NpgsqlConnection CreateConnection();
    }

    public class NpgsqlConnectionFactory : INpgsqlConnectionFactory
    {
        readonly DbConnectionStringBuilder connstringBuilder =
            new NpgsqlConnectionStringBuilder(new Settings().DbConnectionString)
            {
                KeepAlive = 15,
            };

        public NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(connstringBuilder.ConnectionString);
        }
    }
}
