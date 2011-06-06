using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace replication
{
    class SqlDBStruct
    {
        /// <summary>
        /// Кол-во таблиц в БД
        /// </summary>
        int _tablesCount;

        /// <summary>
        /// Кол-во таблиц в БД
        /// </summary>
        public int TablesCount
        {
            get { return _tablesCount; }
            set { _tablesCount = value; }
        }

        /// <summary>
        /// Название схемы в которой расположена таблица
        /// </summary>
        List<string> _schemasNames;

        /// <summary>
        /// Название схемы в которой расположена таблица
        /// </summary>
        public List<string> SchemasNames
        {
            get { return _schemasNames; }
            set { _schemasNames = value; }
        }

        /// <summary>
        /// Название таблицы
        /// </summary>
        List<string> _tablesNames;

        /// <summary>
        /// Название таблицы
        /// </summary>
        public List<string> TablesNames
        {
            get { return _tablesNames; }
            set { _tablesNames = value; }
        }

        /// <summary>
        /// Идентификатор таблицы
        /// </summary>
        List<string> _objectsIDs;

        /// <summary>
        /// Идентификатор таблицы
        /// </summary>
        public List<string> ObjectsIDs
        {
            get { return _objectsIDs; }
            set { _objectsIDs = value; }
        }

        public SqlDBStruct()
        {
            this._tablesCount = 0;
            this._objectsIDs = new List<string>();
            this._schemasNames = new List<string>();
            this._tablesNames = new List<string>();
        }
    }
}
