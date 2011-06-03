using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace replication
{

    class DBManager
    {
        SqlConnection _connection;
        private string _dbName;

        public DBManager(string compName, string dbName) {
            this._dbName = dbName;
            this._connection = new SqlConnection(@"Data Source=" + compName + ";Initial Catalog=" + dbName + ";Integrated Security=True");
            this._connection.Open();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public List<SqlResult> runQuery(string sqlCode)
        {
            var result = new List<SqlResult>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = sqlCode;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    SqlResult res = new SqlResult();
                    res.ColumnCount = reader.FieldCount;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {                        
                        res.ColumnNames.Add(reader.GetName(i));
                        res.Types.Add(reader.GetFieldType(i));
                        res.Values.Add(reader.GetValue(i));
                    }
                    result.Add(res);
                }
            }
            return result;
        }

        public SqlResult ReadFirst(string tableName)
        {
            string primaryKey = this.GetPrimaryKeyName(tableName);
            var result = this.runQuery(@"USE " + this._dbName + @";
                            DECLARE @min AS INT;
                            SELECT @min=MIN(" + primaryKey + @") 
                            FROM " + tableName + @";
                            SELECT *
                            FROM " + tableName + @"
                            WHERE " + primaryKey + @"=@min;");
            return result[0];
        }

        public void RemoveFirst(string tableName)
        {
            string primaryKey = this.GetPrimaryKeyName(tableName);
            this.runQuery(@"USE " + this._dbName + @";
                            DECLARE @min AS INT;
                            SELECT @min=MIN(" + primaryKey + @") 
                            FROM " + tableName + @";
                            DELETE
                            FROM " + tableName + @"
                            WHERE " + primaryKey + @"=@min;");
        }

        public string GetPrimaryKeyName(string tableName)
        {
            var result = this.runQuery(@"USE BasicTSQL;
                                        SELECT c.name AS column_name
                                        FROM sys.indexes AS i
                                        INNER JOIN sys.index_columns AS ic 
                                            ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                                        INNER JOIN sys.columns AS c 
                                            ON ic.object_id = c.object_id AND c.column_id = ic.column_id
                                        WHERE i.is_primary_key = 1 
                                            AND i.object_id = OBJECT_ID('" + tableName + @"');");
            return result[0].Values[0].ToString();
        }

        public int GetPrimaryKeyId(string tableName)
        {
            var result = this.runQuery(@"USE BasicTSQL;
                                        SELECT ic.index_column_id
                                        FROM sys.indexes AS i
                                        INNER JOIN sys.index_columns AS ic 
                                            ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                                        INNER JOIN sys.columns AS c 
                                            ON ic.object_id = c.object_id AND c.column_id = ic.column_id
                                        WHERE i.is_primary_key = 1 
                                            AND i.object_id = OBJECT_ID('" + tableName + @"');");
            return (int)result[0].Values[0];
        }
        
    }
}
