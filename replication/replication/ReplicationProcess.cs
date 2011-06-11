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
        private DBManager _dbmMaster;
        private DBManager _dbmSlave;
		
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="config">
        /// Конфигурация программы
        /// </param>
        public ReplicationProcess(Configurator config) {
			this._config = config;
            this.ConnectMaster(this._config.MaxDBErrorCount);
            this.ConnectSlave(this._config.MaxDBErrorCount);
		}

        /// <summary>
        /// Соединение с БД Master
        /// </summary>
        /// <param name="maxErrorCount">
        /// Максимальное кол-во ошибок при соединении.
        /// После превышения заданного числа программа будет остановлена
        /// </param>
        public void ConnectMaster(int maxErrorCount)
        {
            if (_config.IsWindowsAuth(_config.MasterAuthorization))
            {
                this._dbmMaster = new DBManager(this._config.MasterServerName, this._config.MasterDBName);
            }
            else
            {
                this._dbmMaster = new DBManager(this._config.MasterServerName, this._config.MasterDBUser, this._config.MasterDBPassword, this._config.MasterDBName);
            }

            if (maxErrorCount == 0)
            {
                string errorMsg = String.Format("Reached the limit of connections to the database Master. DB server name: {0}. Auth type:{1}.", this._config.MasterServerName, this._config.MasterAuthorization);
                Console.WriteLine(errorMsg);
                _log.ErrorFormat(errorMsg);
                if (this._config.SendMail)
                {
                    EMailSender smtp = new EMailSender(this._config.ProgMail, this._config.SmtpHost, this._config.SmtpUser, this._config.SmtpPassword, this._config.SmtpPort);
                    smtp.SendMessage(errorMsg,this._config.AdminEMail);
                }
                System.Environment.Exit(0);
            }

            if (!this._dbmMaster.IsConnected())
            {
                string errorMsg = String.Format("Unable to connect to database Master. DB server name: {0}. Auth type:{1}.", this._config.MasterServerName, this._config.MasterAuthorization);
                Console.WriteLine(errorMsg);
                _log.ErrorFormat(errorMsg);
                System.Threading.Thread.Sleep(this._config.SecondaryTimer);
                ConnectMaster(maxErrorCount-1);
            }
        }

        /// <summary>
        /// Соединение с БД Slave
        /// </summary>
        /// <param name="maxErrorCount">
        /// Максимальное кол-во ошибок при соединении.
        /// После превышения заданного числа программа будет остановлена
        /// </param>
        public void ConnectSlave(int maxErrorCount)
        {
            if (_config.IsWindowsAuth(_config.SlaveAuthorization))
            {
                this._dbmSlave = new DBManager(this._config.SlaveServerName, this._config.SlaveDBName);
            }
            else
            {
                this._dbmSlave = new DBManager(this._config.SlaveServerName, this._config.SlaveDBUser, this._config.SlaveDBPassword, this._config.SlaveDBName);
            }

            if (maxErrorCount == 0)
            {
                string errorMsg = String.Format("Reached the limit of connections to the database Slave. DB server name: {0}. Auth type:{1}.", this._config.SlaveServerName, this._config.SlaveAuthorization);
                Console.WriteLine(errorMsg);
                _log.ErrorFormat(errorMsg);
                if (this._config.SendMail)
                {
                    EMailSender smtp = new EMailSender(this._config.ProgMail, this._config.SmtpHost, this._config.SmtpUser, this._config.SmtpPassword, this._config.SmtpPort);
                    smtp.SendMessage(errorMsg, this._config.AdminEMail);
                }
                System.Environment.Exit(0);
            }

            if (!this._dbmSlave.IsConnected())
            {
                string errorMsg = String.Format("Unable to connect to database Slave. DB server name: {0}. Auth type:{1}.", this._config.SlaveServerName, this._config.SlaveAuthorization);
                Console.WriteLine(errorMsg);
                _log.ErrorFormat(errorMsg);
                System.Threading.Thread.Sleep(this._config.SecondaryTimer);
                ConnectSlave(maxErrorCount-1);
            }
        }
		
        /// <summary>
        /// Запуск чтения журнала и обработка найденных в нем записей
        /// </summary>
		public void ReplicateOne(string journalSchema, string journalName, string slaveSchema, string slaveTable) {
  
            if (!this._dbmSlave.IsConnected())
            {
                this.ConnectSlave(this._config.MaxDBErrorCount);
            }

            SqlResult firstRow = this._dbmSlave.ReadFirst(journalSchema, journalName);

            while (firstRow.ColumnCount != 0)
            {
                if (firstRow.Values[2].ToString() == "INSERTED")
                {
                    string eventMsg = "Processing INSERTED event...";
                    Console.WriteLine(eventMsg);
                    _log.Info(eventMsg);
                    this._dbmSlave.GenerateSqlOnInsert(journalSchema, journalName, slaveSchema, slaveTable, firstRow.Values[0].ToString());
                }

                if (firstRow.Values[2].ToString() == "UPDATED")
                {
                    string eventMsg = "Processing UPDATED event...";
                    Console.WriteLine(eventMsg);
                    _log.Info(eventMsg);
                    this._dbmSlave.GenerateSqlOnUpdate(journalSchema, journalName, slaveSchema, slaveTable, firstRow.Values[0].ToString());
                }

                if (firstRow.Values[2].ToString() == "DELETED")
                {
                    string eventMsg = "Processing DELETED event...";
                    Console.WriteLine(eventMsg);
                    _log.Info(eventMsg);
                    this._dbmSlave.GenerateSqlOnDelete(journalSchema, journalName, slaveSchema, slaveTable, firstRow.Values[0].ToString());
                }
                this._dbmSlave.RemoveFirst(journalSchema, journalName);
                firstRow = this._dbmSlave.ReadFirst(journalSchema, journalName);
            }
		}
		
        /// <summary>
        /// CallBack функция которая запускается по событию таймера
        /// Запускает чтение журнала и обработку записей в нем
        /// </summary>
        /// <param name="stateInfo"></param>
		public void OnTimedEvent(Object stateInfo) {
        	Console.WriteLine("Timer is started");
            this.ReplicateAll();
    	}

        /// <summary>
        /// Запуск чтения всех журналов и обработка всех записей
        /// </summary>
        public void ReplicateAll()
        {

            if (!this._dbmMaster.IsConnected())
            {
                this.ConnectMaster(this._config.MaxDBErrorCount);
            }
            SqlDBStruct masterStruct = this._dbmMaster.GetDBInfo();

            for (int i = 0; i < masterStruct.TablesCount; i++)
            {
                this.ReplicateOne(this._config.SchemaName, masterStruct.SchemasNames[i] + masterStruct.TablesNames[i], masterStruct.SchemasNames[i], masterStruct.TablesNames[i]);
            }
        }

        /// <summary>
        /// Метод создающий журналы и триггеры
        /// </summary>
        public void OnStart()
        {

            if (!this._dbmMaster.IsConnected())
            {
                this.ConnectMaster(this._config.MaxDBErrorCount);
            }

            if (!this._dbmSlave.IsConnected())
            {
                this.ConnectSlave(this._config.MaxDBErrorCount);
            }
            this._dbmSlave.CreateDB();
            this._dbmSlave.CreateSchema(this._config.SchemaName);

            SqlDBStruct masterStruct = this._dbmMaster.GetDBInfo();
            for (int i = 0; i < masterStruct.TablesCount; i++)
            {
                SqlTableStruct tempStruct = this._dbmMaster.GetTableInfo(masterStruct.SchemasNames[i], masterStruct.TablesNames[i]);
                this._dbmSlave.CreateJournal(this._config.SchemaName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], tempStruct);
                this._dbmMaster.CreateTriggerOnInsert(this._config.SchemaName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName);
                this._dbmMaster.CreateTriggerOnUpdate(this._config.SchemaName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName);
                this._dbmMaster.CreateTriggerOnDelete(this._config.SchemaName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName);
                this._dbmSlave.CreateSchema(masterStruct.SchemasNames[i]);
                this._dbmSlave.CreateTable(masterStruct.SchemasNames[i], masterStruct.TablesNames[i], tempStruct);
                this._dbmSlave.MergeTables(this._config.MasterDBName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i], this._config.SlaveDBName, masterStruct.SchemasNames[i], masterStruct.TablesNames[i]);
            }
        }
		
	}
}

