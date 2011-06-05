using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace replication
{
    /// <summary>
    /// Класс обеспечивающий хранение одной строки
    /// полученной в результате выполнения запроса
    /// </summary>
    class SqlResult
    {
        /// <summary>
        /// Кол-во столбцов
        /// </summary>
        private int _columnCount;

        /// <summary>
        /// Кол-во столбцов
        /// </summary>
        public int ColumnCount
        {
            get { return _columnCount; }
            set { _columnCount = value; }
        }

        /// <summary>
        /// Имена столбцов
        /// </summary>
        private List<string> _columnNames;

        /// <summary>
        /// Имена столбцов
        /// </summary>
        public List<string> ColumnNames
        {
            get { return _columnNames; }
            set { _columnNames = value; }
        }

        /// <summary>
        /// Типы столбцов
        /// </summary>
        private List<System.Type> _types;

        /// <summary>
        /// Типы столбцов
        /// </summary>
        public List<System.Type> Types
        {
            get { return _types; }
            set { _types = value; }
        }

        /// <summary>
        /// Значения в ячейках
        /// </summary>
        private ArrayList _values;

        /// <summary>
        /// Значения в ячейках
        /// </summary>
        public ArrayList Values
        {
            get { return _values; }
            set { _values = value; }
        }

        public SqlResult()
        {
            this._columnNames = new List<string>();
            this._types = new List<Type>();
            this._values = new ArrayList();
        }

    }
}
