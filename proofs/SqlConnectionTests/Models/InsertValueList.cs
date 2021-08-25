using Microsoft.Data.SqlClient;

namespace SqlConnectionTests.Models
{
    public class InsertValueList
    {
        public string Sql { get; set; }

        public SqlParameter[] Parameters { get; set; }
    }
}
