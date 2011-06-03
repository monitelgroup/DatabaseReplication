using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace replication
{
    class SqlResult
    {
        private int _columnCount;

        public int ColumnCount
        {
            get { return _columnCount; }
            set { _columnCount = value; }
        }
        private List<string> _columnNames;

        public List<string> ColumnNames
        {
            get { return _columnNames; }
            set { _columnNames = value; }
        }
        private List<System.Type> _types;

        public List<System.Type> Types
        {
            get { return _types; }
            set { _types = value; }
        }
        private ArrayList _values;

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
