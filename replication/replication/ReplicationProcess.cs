using System;

namespace replication
{
    /// <summary>
    /// ����� �������������� ������ ����������
    /// </summary>
	public class ReplicationProcess
	{
		
        /// <summary>
        /// ������������ ���������
        /// </summary>
		private Configurator _config;
		
        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="config">
        /// ������������ ���������
        /// </param>
        public ReplicationProcess(Configurator config) {
			this._config = config;
		}
		
        /// <summary>
        /// ������ ������ ������� � ��������� ��������� � ��� �������
        /// </summary>
		public void ReplicateOne(string journalSchema, string journalName, string slaveSchema, string slaveTable) {
            
            DBManager dbmSlave = new DBManager(_config.SlaveCompName, _config.SlaveDBName);

            if (!dbmSlave.IsConnected())
            {
                Console.WriteLine("Slave DB is not working");
                return;
            }

            SqlResult firstRow = dbmSlave.ReadFirst(journalSchema, journalName);

            while (firstRow.ColumnCount != 0)
            {
                if (firstRow.Values[2].ToString() == "INSERTED")
                {
                    Console.WriteLine("Processing INSERTED event...");
                    dbmSlave.GenerateSqlOnInsert(journalSchema, journalName, slaveSchema, slaveTable, firstRow.Values[0].ToString());
                }

                if (firstRow.Values[2].ToString() == "UPDATED")
                {
                    Console.WriteLine("Processing UPDATED event...");
                    dbmSlave.GenerateSqlOnUpdate(journalSchema, journalName, slaveSchema, slaveTable, firstRow.Values[0].ToString());
                }

                if (firstRow.Values[2].ToString() == "DELETED")
                {
                    Console.WriteLine("Processing DELETED event...");
                    dbmSlave.GenerateSqlOnDelete(journalSchema, journalName, slaveSchema, slaveTable, firstRow.Values[0].ToString());
                }
                dbmSlave.RemoveFirst(journalSchema, journalName);
                firstRow = dbmSlave.ReadFirst(journalSchema, journalName);
            }
            dbmSlave.CloseConnection();
		}
		
        /// <summary>
        /// CallBack ������� ������� ����������� �� ������� �������
        /// ��������� ������ ������� � ��������� ������� � ���
        /// </summary>
        /// <param name="stateInfo"></param>
		public void OnTimedEvent(Object stateInfo) {
        	Console.WriteLine("Timer is started");
            this.ReplicateAll();
    	}

        public void ReplicateAll()
        {
            DBManager dbmMaster = new DBManager(this._config.MasterCompName, this._config.MasterDBName);
            SqlDBStruct masterStruct = dbmMaster.GetDBInfo();

            for (int i = 0; i < masterStruct.TablesCount; i++)
            {
                this.ReplicateOne("ReplicJournals", masterStruct.SchemasNames[i] + masterStruct.TablesNames[i], masterStruct.SchemasNames[i], masterStruct.TablesNames[i]);
            }
        }

        public void OnStart()
        {
            DBManager dbmMaster = new DBManager(this._config.MasterCompName, this._config.MasterDBName);
            DBManager dbmSlave = new DBManager(this._config.SlaveCompName, this._config.SlaveDBName);
            dbmSlave.CreateSchema("ReplicJournals");
            
            SqlDBStruct masterStruct = dbmMaster.GetDBInfo();
            for (int i = 0; i < masterStruct.TablesCount; i++)
            {
                SqlTableStruct tempStruct = dbmMaster.GetTableInfo(masterStruct.SchemasNames[i], masterStruct.TablesNames[i]);
                dbmSlave.CreateJournal(masterStruct.SchemasNames[i], masterStruct.TablesNames[i], tempStruct);
                dbmMaster.CreateTriggerOnInsert(masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName);
                dbmMaster.CreateTriggerOnUpdate(masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName);
                dbmMaster.CreateTriggerOnDelete(masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName);
                dbmSlave.CreateSchema(masterStruct.SchemasNames[i]);
                dbmSlave.CreateTable(masterStruct.SchemasNames[i], masterStruct.TablesNames[i], tempStruct);
                dbmSlave.MergeTables(this._config.MasterDBName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i]);
            }
        }
		
	}
}

