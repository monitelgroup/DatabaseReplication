using System;
using System.Collections.Generic;

namespace replication
{
    /// <summary>
    /// Класс обеспечивающий хранение данных о структуре таблицы
    /// </summary>
    class SqlTableStruct
    {
        /// <summary>
        /// Количество столбцов в таблице
        /// </summary>
        private int _columnCount;
        
        /// <summary>
        /// Количество столбцов в таблице
        /// </summary>
        public int ColumnCount
        {
            get { return _columnCount; }
            set { _columnCount = value; }
        }

        /// <summary>
        /// Имена столбцов в таблице
        /// </summary>
        private List<String> _columnNames;

        /// <summary>
        /// Имена столбцов в таблице
        /// </summary>
        public List<String> ColumnNames
        {
            get { return _columnNames; }
            set { _columnNames = value; }
        }

        /// <summary>
        /// Типы данных столбцов в таблице (sql-типы)
        /// </summary>
        private List<String> _columnSqlTypes;

        /// <summary>
        /// Типы данных столбцов в таблице (sql-типы)
        /// </summary>
        public List<String> ColumnSqlTypes
        {
            get { return _columnSqlTypes; }
            set { _columnSqlTypes = value; }
        }

        /// <summary>
        /// Является ли столбец первичным ключом
        /// </summary>
        private List<bool> _isPrimaryKey;

        /// <summary>
        /// Является ли столбец первичным ключом
        /// </summary>
        public List<bool> IsPrimaryKey
        {
            get { return _isPrimaryKey; }
            set { _isPrimaryKey = value; }
        }
 
        
        public SqlTableStruct()
        {
            this._columnCount = 0;
            this._columnNames = new List<string>();
            this._columnSqlTypes = new List<string>();
            this._isPrimaryKey = new List<bool>();
        }
    }
}
