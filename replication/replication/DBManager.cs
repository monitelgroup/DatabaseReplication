using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace replication
{

    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    class DBManager
    {
        SqlConnection _connection;
        
        /// <summary>
        /// Имя базы данных
        /// </summary>
        private string _dbName;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="compName">
        /// Имя компьютера
        /// </param>
        /// <param name="dbName">
        /// Имя базы данных
        /// </param>
        public DBManager(string compName, string dbName) {
            this._dbName = dbName;
            this._connection = new SqlConnection(@"Data Source=" + compName + ";Initial Catalog=" + dbName + ";Integrated Security=True");
            this._connection.Open();
        }

        /// <summary>
        /// Закрыть соединение с БД
        /// </summary>
        public void CloseConnection()
        {
            _connection.Close();
        }

        /// <summary>
        /// Проверка соединения с БД.
        /// </summary>
        /// <returns>
        /// True - если соединено. False в противном случае.
        /// </returns>
        public bool isConnected()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Запуск произвального SQL запроса
        /// </summary>
        /// <param name="sqlCode">
        /// Код sql запроса в виде строки
        /// </param>
        /// <returns>
        /// Список строк полученных в результате выполнения запроса
        /// </returns>
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

        /// <summary>
        /// Читает первую строку (с минимальным значение Primary Key) в таблице
        /// </summary>
        /// <param name="tableName">
        /// Имя таблицы
        /// </param>
        /// <returns>
        /// Первая строка таблицы
        /// </returns>
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
            if (result.Count == 0)
            {
                return new SqlResult();
            }
            return result[0];
        }

        /// <summary>
        /// Удаляет первую строку из таблицы
        /// </summary>
        /// <param name="tableName">
        /// Имя таблицы
        /// </param>
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

        /// <summary>
        /// Позволяет получить имя первичного ключа таблицы
        /// </summary>
        /// <param name="tableName">
        /// Имя таблицы
        /// </param>
        /// <returns>
        /// Имя Primaty Key
        /// </returns>
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

        /// <summary>
        /// Позволяет получить порядковый номер первичного ключа (среди столбцов) в таблице
        /// </summary>
        /// <param name="tableName">
        /// Имя таблицы
        /// </param>
        /// <returns>
        /// Порядковый номер Primary Key
        /// </returns>
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

        /// <summary>
        /// Создает журнал(таблицу)
        /// </summary>
        /// <param name="tableName">
        /// Имя журнала
        /// </param>
        /// <param name="columnNames">
        /// Имена столбцов
        /// </param>
        /// <param name="columnTypes">
        /// Типы столбцов
        /// </param>
        public void CreateJournal(string tableName, List<string> columnNames, List<string> columnTypes)
        {
            string query = @"USE " + this._dbName + @"; CREATE TABLE " + tableName + @" ( 
                            id INT NOT NULL IDENTITY PRIMARY KEY,
	                        event_time DATETIME NOT NULL DEFAULT(CURRENT_TIMESTAMP),
	                        event_type NVARCHAR(10) NOT NULL,
	                        login_name SYSNAME NOT NULL DEFAULT(SUSER_SNAME())";
            for(int i=0; i<columnNames.Count; i++) {
                query += @" , " + columnNames[i] + @" " + columnTypes[i] + @" DEFAULT(NULL) ";
            }
            query += @");";

            this.runQuery(query); 
        }

        /// <summary>
        /// Удаляет журнал(таблицу)
        /// </summary>
        /// <param name="tableName">
        /// Имя журнала
        /// </param>
        public void DeleteJournal(string tableName)
        {
            this.runQuery(@"USE " + this._dbName + @"; DROP TABLE " + tableName + " ;");
        }

        /// <summary>
        /// Позволяет получить информацию о таблице
        /// </summary>
        /// <param name="schemaName">
        /// Имя схемы в которой расположена таблица
        /// </param>
        /// <param name="tableName">
        /// Имя таблицы
        /// </param>
        /// <returns>
        /// Имена столбцов таблицы и их типы данных
        /// </returns>
        public SqlTableStruct GetTableInfo(string schemaName, string tableName) {
            var result = this.runQuery(@"USE " + this._dbName + @" ; 
                            DECLARE @schemaid AS INT;
                            DECLARE @tableid AS INT;
                            SELECT @schemaid = schema_id
                            FROM sys.schemas
                            WHERE name = '" + schemaName + @"';
                            SELECT @tableid = object_id FROM sys.objects
                            WHERE type = 'U' AND schema_id = @schemaid AND name = '" + tableName + @"';
                            SELECT SC.name, ST.name, SC.max_length/2
                            FROM sys.columns AS SC
	                            JOIN sys.types AS ST
		                            ON SC.system_type_id = ST.system_type_id AND SC.user_type_id = ST.user_type_id
                            WHERE SC.object_id = @tableid;");
            
            SqlTableStruct retval = new SqlTableStruct();
            retval.ColumnCount = result.Count;
            
            for (int i = 0; i < result.Count; i++)
            {
                retval.ColumnNames.Add(result[i].Values[0].ToString());
                string columnType = result[i].Values[1].ToString();
                if (columnType == "varchar" || columnType == "nvarchar")
                {
                    columnType += "(" + result[i].Values[2].ToString() + ")";
                }
                retval.ColumnSqlTypes.Add(columnType);
            }
            
            return retval;
        }

        /// <summary>
        /// Генерирует и исполняет запрос, обрабатывающий запись в журнале на событие INSERTED
        /// </summary>
        /// <param name="journalName">
        /// Имя журнала
        /// </param>
        /// <param name="toReplicSchema">
        /// Имя схема в которо расположена таблица Slave
        /// </param>
        /// <param name="toReplicTable">
        /// Имя таблицы Slave
        /// </param>
        /// <param name="journalRowID">
        /// id в журнале, которое будет обработано
        /// </param>
        public void GenerateSqlOnInsert(string journalName, string toReplicSchema, string toReplicTable, string journalRowID)
        {
            string query = @"USE " + this._dbName + @" ; 
                            INSERT INTO "+ toReplicSchema + @"." + toReplicTable + @"(";
            SqlTableStruct replicStruct = this.GetTableInfo(toReplicSchema,toReplicTable);
            query += replicStruct.ColumnNames[0];
            for(int i=1; i<replicStruct.ColumnCount; i++) {
                query += @", " + replicStruct.ColumnNames[i];
            }
            query += @") SELECT " + replicStruct.ColumnNames[0] + @"_new ";
            for(int i=1; i<replicStruct.ColumnCount; i++) {
                query += @", " + replicStruct.ColumnNames[i] + @"_new ";
            }
            query += @" FROM " + journalName + @" WHERE id = " + journalRowID + @";";

            this.runQuery(query);
        }

        /// <summary>
        /// Генерирует и исполняет запрос, обрабатывающий запись в журнале на событие UPDATED
        /// </summary>
        /// <param name="journalName">
        /// Имя журнала
        /// </param>
        /// <param name="toReplicSchema">
        /// Имя схема в которо расположена таблица Slave
        /// </param>
        /// <param name="toReplicTable">
        /// Имя таблицы Slave
        /// </param>
        /// <param name="journalRowID">
        /// id в журнале, которое будет обработано
        /// </param>
        public void GenerateSqlOnUpdate(string journalName, string toReplicSchema, string toReplicTable, string journalRowID)
        {
            string query = @"USE " + this._dbName + @" ;
                            UPDATE " + toReplicSchema + @"." + toReplicTable + @" 
	                            SET ";
            SqlTableStruct replicStruct = this.GetTableInfo(toReplicSchema,toReplicTable);
            query += replicStruct.ColumnNames[0] + @" = " + @"Journal." + replicStruct.ColumnNames[0] + @"_new";
            for(int i=1; i<replicStruct.ColumnCount; i++) {
                query += @", " + replicStruct.ColumnNames[i] + @" = " + @"Journal." + replicStruct.ColumnNames[i] + @"_new";
            }
            string primaryName = this.GetPrimaryKeyName(toReplicSchema + "." + toReplicTable);
            query += @" FROM " + toReplicSchema + @"." + toReplicTable + @" AS ToTable
	                    JOIN " + journalName + @" AS Journal
	                    ON ToTable." + primaryName + @"= Journal." + primaryName + @"_old
                        WHERE Journal.id = " + journalRowID + @";";
            this.runQuery(query);
        }

        /// <summary>
        /// Генерирует и исполняет запрос, обрабатывающий запись в журнале на событие DELETED
        /// </summary>
        /// <param name="journalName">
        /// Имя журнала
        /// </param>
        /// <param name="toReplicSchema">
        /// Имя схема в которо расположена таблица Slave
        /// </param>
        /// <param name="toReplicTable">
        /// Имя таблицы Slave
        /// </param>
        /// <param name="journalRowID">
        /// id в журнале, которое будет обработано
        /// </param>
        public void GenerateSqlOnDelete(string journalName, string toReplicSchema, string toReplicTable, string journalRowID)
        {
            string primaryName = this.GetPrimaryKeyName(toReplicSchema + "." + toReplicTable);
            string query = @"USE " + this._dbName + @"; 
                            DELETE FROM NS 
                            FROM " + toReplicSchema + @"." + toReplicTable + @" AS NS 
	                            JOIN " + journalName + @" AS SR 
		                            ON NS." + primaryName + @" = SR." + primaryName + @"_old 
                            WHERE SR.id = " + journalRowID + @";";
            this.runQuery(query);
        }

    }
}
