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
        /// ��� ������� � ��������  
        /// </summary>
        private string _journalName;

        /// <summary>
        /// ������� Slave
        /// </summary>
        private string _replicTable;

        /// <summary>
        /// ����� � ������� ����������� ������� Slave
        /// </summary>
        private string _replicSchema;
		
        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="config">
        /// ������������ ���������
        /// </param>
        public ReplicationProcess(Configurator config) {
			this._config = config;
            this._journalName = "Sales.ShippersReplication";
            this._replicSchema = "Sales";
            this._replicTable = "NewShippers";
		}
		
        /// <summary>
        /// ������ ������ ������� � ��������� ��������� � ��� �������
        /// </summary>
		public void StartReplication() {
            
            DBManager dbmMaster = new DBManager(_config.MasterCompName, _config.MasterDBName);
            DBManager dbmSlave = new DBManager(_config.SlaveCompName, _config.SlaveDBName);
            
            if (!dbmMaster.isConnected())
            {
                Console.WriteLine("Master DB is not working");
                return;
            }

            if (!dbmSlave.isConnected())
            {
                Console.WriteLine("Slave DB is not working");
                return;
            }

            SqlResult firstRow = dbmMaster.ReadFirst(this._journalName);

            while (firstRow.ColumnCount != 0)
            {
                if (firstRow.Values[2].ToString() == "INSERTED")
                {
                    Console.WriteLine("Processing INSERTED event...");
                    dbmSlave.GenerateSqlOnInsert(this._journalName, this._replicSchema, this._replicTable, firstRow.Values[0].ToString());
                }

                if (firstRow.Values[2].ToString() == "UPDATED")
                {
                    Console.WriteLine("Processing UPDATED event...");
                    dbmSlave.GenerateSqlOnUpdate(this._journalName, this._replicSchema, this._replicTable, firstRow.Values[0].ToString());
                }

                if (firstRow.Values[2].ToString() == "DELETED")
                {
                    Console.WriteLine("Processing DELETED event...");
                    dbmSlave.GenerateSqlOnDelete(this._journalName, this._replicSchema, this._replicTable, firstRow.Values[0].ToString());
                }
                dbmMaster.RemoveFirst(this._journalName);
                firstRow = dbmMaster.ReadFirst(this._journalName);
            }
            dbmMaster.CloseConnection();
            dbmSlave.CloseConnection();
		}
		
        /// <summary>
        /// CallBack ������� ������� ����������� �� ������� �������
        /// ��������� ������ ������� � ��������� ������� � ���
        /// </summary>
        /// <param name="stateInfo"></param>
		public void OnTimedEvent(Object stateInfo) {
        	Console.WriteLine("Timer is started");
            this.StartReplication();
    	}
		
	}
}