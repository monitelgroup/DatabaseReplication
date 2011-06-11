using System;
using log4net;
using log4net.Config;

namespace replication
{
    /// <summary>
    /// Класс осуществляющий логику репликации
    /// </summary>
	public class ReplicationProcess
	{

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Конфигурации программы
        /// </summary>
		private Configurator _config;
		
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="config">
        /// Конфигурация программы
        /// </param>
        public ReplicationProcess(Configurator config) {
			this._config = config;
		}
		
        /// <summary>
        /// Запуск чтения журнала и обработка найденных в нем записей
        /// </summary>
		public void ReplicateOne(string journalSchema, string journalName, string slaveSchema, string slaveTable) {
            
            DBManager dbmSlave = new DBManager(_config.SlaveServerName, _config.SlaveDBName);

            if (!dbmSlave.IsConnected())
            {
                Console.WriteLine("Slave DB is not working");
                _log.Error("Slave DB is not working");
                return;
            }

            SqlResult firstRow = dbmSlave.ReadFirst(journalSchema, journalName);

            while (firstRow.ColumnCount != 0)
            {
                if (firstRow.Values[2].ToString() == "INSERTED")
                {
                    Console.WriteLine("Processing INSERTED event...");
                    _log.Info("Processing INSERTED event...");
                    dbmSlave.GenerateSqlOnInsert(journalSchema, journalName, slaveSchema, slaveTable, firstRow.Values[0].ToString());
                }

                if (firstRow.Values[2].ToString() == "UPDATED")
                {
                    Console.WriteLine("Processing UPDATED event...");
                    _log.Info("Processing UPDATED event...");
                    dbmSlave.GenerateSqlOnUpdate(journalSchema, journalName, slaveSchema, slaveTable, firstRow.Values[0].ToString());
                }

                if (firstRow.Values[2].ToString() == "DELETED")
                {
                    Console.WriteLine("Processing DELETED event...");
                    _log.Info("Processing DELETED event...");
                    dbmSlave.GenerateSqlOnDelete(journalSchema, journalName, slaveSchema, slaveTable, firstRow.Values[0].ToString());
                }
                dbmSlave.RemoveFirst(journalSchema, journalName);
                firstRow = dbmSlave.ReadFirst(journalSchema, journalName);
            }
            dbmSlave.CloseConnection();
		}
		
        /// <summary>
        /// CallBack функция которая запускается по событию таймера
        /// Запускает чтение журнала и обработку записей в нем
        /// </summary>
        /// <param name="stateInfo"></param>
		public void OnTimedEvent(Object stateInfo) {
        	Console.WriteLine("Timer is started");
            _log.Info("Timer is started");
            this.ReplicateAll();
    	}

        /// <summary>
        /// Запуск чтения всех журналов и обработка всех записей
        /// </summary>
        public void ReplicateAll()
        {
            DBManager dbmMaster = new DBManager(this._config.MasterServerName, this._config.MasterDBName);
            if (!dbmMaster.IsConnected())
            {
                Console.WriteLine("Master DB is not working");
                _log.Error("Master DB is not working");
                return;
            }
            SqlDBStruct masterStruct = dbmMaster.GetDBInfo();

            for (int i = 0; i < masterStruct.TablesCount; i++)
            {
                this.ReplicateOne(this._config.SchemeName, masterStruct.SchemasNames[i] + masterStruct.TablesNames[i], masterStruct.SchemasNames[i], masterStruct.TablesNames[i]);
            }
        }

        /// <summary>
        /// Метод создающий журналы и триггеры
        /// </summary>
        public void OnStart()
        {
            DBManager dbmMaster = new DBManager(this._config.MasterServerName, this._config.MasterDBName);
            if (!dbmMaster.IsConnected())
            {
                Console.WriteLine("Master DB is not working");
                _log.Error("Master DB is not working");
                return;
            }

            DBManager dbmSlave = new DBManager(this._config.SlaveServerName, this._config.SlaveDBName);
            if (!dbmSlave.IsConnected())
            {
                Console.WriteLine("Slave DB is not working");
                _log.Error("Slave DB is not working");
                return;
            }
            dbmSlave.CreateSchema(this._config.SchemeName);
            
            SqlDBStruct masterStruct = dbmMaster.GetDBInfo();
            for (int i = 0; i < masterStruct.TablesCount; i++)
            {
                SqlTableStruct tempStruct = dbmMaster.GetTableInfo(masterStruct.SchemasNames[i], masterStruct.TablesNames[i]);
                dbmSlave.CreateJournal(this._config.SchemeName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], tempStruct);
                dbmMaster.CreateTriggerOnInsert(this._config.SchemeName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName);
                dbmMaster.CreateTriggerOnUpdate(this._config.SchemeName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName);
                dbmMaster.CreateTriggerOnDelete(this._config.SchemeName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName);
                dbmSlave.CreateSchema(masterStruct.SchemasNames[i]);
                dbmSlave.CreateTable(masterStruct.SchemasNames[i], masterStruct.TablesNames[i], tempStruct);
                dbmSlave.MergeTables(this._config.MasterDBName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i]);
            }
        }
		
	}
}

