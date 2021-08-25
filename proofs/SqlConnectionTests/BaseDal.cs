using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;

namespace SqlConnectionTests
{
    public abstract class BaseDal
    {
        protected readonly string ConnectionString;

        protected BaseDal()
        {
            ConnectionString = LoadConnectionString();
        }

        private static string LoadConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("ScratchSpace");

            return connectionString;
        }

        protected string GetQuery()
        {
            var dtm = DateTime.UtcNow;

            var sql =
                $"INSERT INTO dbo.BulkCopyTest(CreatedOnUtc) VALUES ('{dtm:yyyy-MM-dd HH:mm:ss.fffffff}');";

            return sql;
        }

        public DateTime[] GetDummyValueList(int valueListSize, int spacingInMilliseconds = 0)
        {
            var arr = new DateTime[valueListSize];

            for (var i = 0; i < arr.Length; i++)
            {
                var dtm = DateTime.UtcNow;

                if (spacingInMilliseconds > 0)
                {
                    dtm = dtm.AddMilliseconds(spacingInMilliseconds);
                }

                arr[i] = dtm;
            }

            return arr;
        }

        protected void ExecuteNonQuery(string sql, SqlParameter[] parameters)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
