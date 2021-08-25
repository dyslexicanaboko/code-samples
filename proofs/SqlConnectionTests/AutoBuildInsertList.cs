using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SqlConnectionTests.Models;

namespace SqlConnectionTests
{
    public class AutoBuildInsertList
        : BaseDal
    {
        private static readonly Dictionary<Type, SqlDbType> TypeMap = new Dictionary<Type, SqlDbType>
        {
            {typeof(string), SqlDbType.NVarChar},
            {typeof(char[]), SqlDbType.NVarChar},
            {typeof(int), SqlDbType.Int},
            {typeof(short), SqlDbType.SmallInt},
            {typeof(long), SqlDbType.BigInt},
            {typeof(byte[]), SqlDbType.VarBinary},
            {typeof(bool), SqlDbType.Bit},
            {typeof(DateTime), SqlDbType.DateTime2},
            {typeof(DateTimeOffset), SqlDbType.DateTimeOffset},
            {typeof(decimal), SqlDbType.Decimal},
            {typeof(double), SqlDbType.Float},
            {typeof(byte), SqlDbType.TinyInt},
            {typeof(TimeSpan), SqlDbType.Time}
        };

        public InsertValueList GenerateSql<T>(IList<T> target, string fullTableName, string primaryKey)
            where T : new()
        {
            var t = target.First().GetType();

            var properties = GetProperties(t, primaryKey);

            var columnNames = properties.Select(x => x.Name).ToArray();

            var parameterList = string.Join(", ", columnNames);

            var arr = new SqlParameter[target.Count * columnNames.Length];

            var sb = new StringBuilder();

            sb.AppendLine($"INSERT INTO {fullTableName} ({parameterList}) VALUES ");

            var pCount = 0;

            for (var r = 0; r < target.Count; r++)
            {
                var row = target[r];

                var rowProperties = GetProperties(row.GetType(), primaryKey);

                sb.Append("(");

                for (var c = 0; c < properties.Count; c++)
                {
                    var colProperty = rowProperties[c];
                    var colType = colProperty.PropertyType;

                    var sqlVariable = $"@r{r}c{c}";

                    var p = new SqlParameter();
                    p.ParameterName = sqlVariable;
                    p.SqlDbType = TypeMap[colType];
                    p.Value = colProperty.GetValue(row);

                    sb.Append(sqlVariable).Append(",");

                    arr[pCount] = p;

                    pCount++;
                }
                
                //Trim trailing comma
                sb.Remove(sb.Length - 1, 1);

                sb.AppendLine("),");
            }

            //Trim trailing comma and newline \r\n
            sb.Remove(sb.Length - 3, 3);

            var sql = sb.ToString();

            var values = new InsertValueList
            {
                Sql = sql,
                Parameters = arr
            };

            return values;
        }

        public void ExecuteInsertValueList(InsertValueList values)
        {
            ExecuteNonQuery(values.Sql, values.Parameters);
        }

        private List<PropertyInfo> GetProperties(Type type, string primaryKey)
        {
            var properties = type
                .GetProperties()
                .Where(x => !x.Name.Equals(primaryKey, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(x => x.Name)
                .ToList();

            return properties;
        }
    }
}
