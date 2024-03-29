﻿using Microsoft.Data.SqlClient;
using SqlConnectionTests.Models;
using System;
using System.Collections.Generic;

namespace SqlConnectionTests
{
    public class AutoBuildUpdate
        : AutoBuildSql
    {
        public SqlParamList GenerateSql<T>(T target, string fullTableName, string primaryKey)
            where T : new()
        {
            var t = target.GetType();

            var properties = GetProperties(t, primaryKey);

            //Column set count plus PK
            var arr = new SqlParameter[properties.Length + 1];

            var lstSetCols = new List<string>(properties.Length);
            
            string sqlVariable;

            for (var c = 0; c < properties.Length; c++)
            {
                sqlVariable = $"@c{c}";

                var colProperty = properties[c];

                lstSetCols.Add($"{colProperty.Name} = {sqlVariable}");

                var p = GetParam(colProperty, sqlVariable, target);

                arr[c] = p;
            }

            sqlVariable = "@pk";

            var pk = GetPrimaryKey(t, primaryKey);

            arr[properties.Length] = GetParam(pk, sqlVariable, target);

            var sets = string.Join("," + Environment.NewLine, lstSetCols);

            var sql = 
                $"UPDATE {fullTableName} SET " + Environment.NewLine + 
                sets + Environment.NewLine + 
                $"WHERE {pk.Name} = {sqlVariable}";

            var values = new SqlParamList
            {
                Sql = sql,
                Parameters = arr
            };

            return values;
        }
    }
}
