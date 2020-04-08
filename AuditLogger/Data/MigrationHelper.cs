using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace TwoWayRelation.Data
{
    public class MigrationHelper : IDesignTimeDbContextFactory<Db>
    {
        private const string dataSourceVariable = "AuditLogger_DataSource";
        private const string initialCatalogVariable = "AuditLogger_InitialCatalog";
        private readonly string dataSource = Environment.GetEnvironmentVariable(dataSourceVariable);
        private readonly string initialCatalog = Environment.GetEnvironmentVariable(initialCatalogVariable);
        private void AssertEnvVariables()
        {
            if (string.IsNullOrWhiteSpace(dataSource))
            {
                throw new ArgumentException($"Environment variable ({dataSourceVariable}) must have a value");
            }

            if (string.IsNullOrWhiteSpace(initialCatalog))
            {
                throw new ArgumentException($"Environment variable ({initialCatalogVariable}) must have a value");
            }
        }

        public Db CreateDbContext(string[] args)
        {
            AssertEnvVariables();
            var connectionString = new SqlConnectionStringBuilder
            {
                InitialCatalog = initialCatalog,
                DataSource = dataSource,
                IntegratedSecurity = true,
                MultipleActiveResultSets = true
            }.ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlServer(connectionString);
            return new Db(optionsBuilder.Options);
        }
    }
}
