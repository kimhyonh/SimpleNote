using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SimpleNote.Infrastructures.SqlContext
{
    public abstract class SimpleNoteSqlContext : IDisposable
    {
        protected readonly IDbConnection Db;

        protected SimpleNoteSqlContext(IConfiguration config)
        {
            if (config is null)
                throw new ArgumentNullException(nameof(config));

            Db = new SqlConnection(config["ConnectionString"]);
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
