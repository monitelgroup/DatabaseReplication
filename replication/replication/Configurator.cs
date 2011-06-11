using System;
using log4net;
using log4net.Config;

namespace replication
{
    /// <summary>
    /// Класс предоставляющий настройки программы
    /// </summary>
	public class Configurator
	{

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Тип авторизации MS SQL (Windows || Sql) для БД Master
        /// </summary>
        public string MasterAutorization;
		
        /// <summary>
        /// Имя базы данных master
        /// </summary>
        public string MasterDBName;

        /// <summary>
        /// Имя пользователя базы даннх master (при авторизации sql)
        /// </summary>
        public string MasterDBUser;

        /// <summary>
        /// пароль пользователя базы даннх master (при авторизации sql)
        /// </summary>
        public string MasterDBPassword;

        /// <summary>
        /// Имя сервера MS SQL для БД master (при авторизации windows)
        /// </summary>
        public string MasterServerName;

        /// <summary>
        /// Тип авторизации MS SQL (Windows || Sql) для БД Slave
        /// </summary>
        public string SlaveAutorization;

        /// <summary>
        /// Имя базы данных slave
        /// </summary>
        public string SlaveDBName;

        /// <summary>
        /// Имя пользователя базы даннх slave (при авторизации sql)
        /// </summary>
        public string SlaveDBUser;

        /// <summary>
        /// пароль пользователя базы даннх slave (при авторизации sql)
        /// </summary>
        public string SlaveDBPassword;

        /// <summary>
        /// Имя сервера MS SQL для БД slave (при авторизации windows)
        /// </summary>
        public string SlaveServerName;
		
        /// <summary>
        /// Интервал срабатывания таймера, по которому будет происходить проверка журнала
        /// </summary>
        public int MainTimerValue;

        /// <summary>
        /// Имя схемы в которой будут располагаться журналы
        /// </summary>
		public string SchemeName;

        /// <summary>
        /// E-mail адресс администратора
        /// </summary>
		public string AdminEMail;

        /// <summary>
        /// адрес SMTP сервера для отправки почты
        /// </summary>
		public string smtpHost;

        /// <summary>
        /// порт SMTP сервера для отправки почты
        /// </summary>
		public int smtpPort;

        /// <summary>
        /// имя пользователя SMTP сервера для отправки почты
        /// </summary>
		public string smtpUser;

        /// <summary>
        /// пароь пользователя SMTP сервера для отправки почты
        /// </summary>
		public string smtpPassword;

        /// <summary>
        /// E-mail адресс от которого будет отправлено сообщение
        /// </summary>
		public string progMail;

        public int maxDBErrorCount;
        public int secondaryTimer;
        public bool SendMail;

        /// <summary>
        /// Конструктор класса. Получает настройки из файла конфигурации
        /// </summary>
        /// <param name="configFileName">
        /// Имя конфигурации
        /// </param>
        public Configurator(string configFileName) 
        {
            try
            {
                var CL = new ConfLoader(configFileName);
                this.MasterAutorization = CL.LoadConfig("MasterAutorization");
                this.MasterServerName = CL.LoadConfig("MasterServerName");
                this.MasterDBName = CL.LoadConfig("MasterDBName");
                this.MasterDBUser = CL.LoadConfig("MasterDBUser");
                this.MasterDBPassword = CL.LoadConfig("MasterDBPassword");

                this.SlaveAutorization = CL.LoadConfig("SlaveAutorization");
                this.SlaveServerName = CL.LoadConfig("SlaveServerName");
                this.SlaveDBName = CL.LoadConfig("SlaveDBName");
                this.SlaveDBUser = CL.LoadConfig("SlaveDBUser");
                this.SlaveDBPassword = CL.LoadConfig("SlaveDBPassword");

                this.AdminEMail = CL.LoadConfig("AdminEmail");
                this.SchemeName = CL.LoadConfig("SchemeName");
                this.MainTimerValue = CL.LoadIntConfig("Timer");
                this.maxDBErrorCount = 1;
                this.secondaryTimer = 10000;
                this.SendMail = true;
            }
            catch (System.IO.FileNotFoundException exp) {
                Console.WriteLine("File Not Found. Check file address. \n Error details:\n {0}",exp.Message);
                _log.ErrorFormat("File Not Found. Check file address. \n Error details:\n {0}", exp.Message);
            }
            catch (System.Xml.XmlException exp) { 
                Console.WriteLine("Error in XML file. \n Error details:\n {0}", exp.Message);
                _log.ErrorFormat("Error in XML file. \n Error details:\n {0}", exp.Message);
            }
        }

        /// <summary>
        /// Проверяет тип соединения
        /// </summary>
        /// <param name="authType">
        /// Тип соединения в виде строки: Windows или Sql
        /// </param>
        /// <returns>
        /// True - если Windows. Иначе False
        /// </returns>
        public bool IsWindowsAuth(string authType)
        {
            if (authType == "Windows")
            {
                return true;
            }
            return false;
        }
	}
}

