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
        public string MasterAuthorization;
		
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
        public string SlaveAuthorization;

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
		public string SchemaName;

        /// <summary>
        /// E-mail адресс администратора
        /// </summary>
		public string AdminEMail;

        /// <summary>
        /// адрес SMTP сервера для отправки почты
        /// </summary>
		public string SmtpHost;

        /// <summary>
        /// порт SMTP сервера для отправки почты
        /// </summary>
		public int SmtpPort;

        /// <summary>
        /// имя пользователя SMTP сервера для отправки почты
        /// </summary>
		public string SmtpUser;

        /// <summary>
        /// пароь пользователя SMTP сервера для отправки почты
        /// </summary>
		public string SmtpPassword;

        /// <summary>
        /// E-mail адресс от которого будет отправлено сообщение
        /// </summary>
		public string ProgMail;

        /// <summary>
        /// Максимально кол-во ошибок при подключении к БД.
        /// </summary>
        public int MaxDBErrorCount;
        
        /// <summary>
        /// Время через которое будет выполнено повторное соединение с БД
        /// </summary>
        public int SecondaryTimer;
        
        /// <summary>
        /// True - отправлять сообщения об ошибках на e-mail администратора. False - не отправлять
        /// </summary>
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
                //Получаем настройки БД Master
                this.MasterAuthorization = CL.LoadConfig("MasterAutorization");
                this.MasterServerName = CL.LoadConfig("MasterServerName");
                this.MasterDBName = CL.LoadConfig("MasterDBName");
                this.MasterDBUser = CL.LoadConfig("MasterDBUser");
                this.MasterDBPassword = CL.LoadConfig("MasterDBPassword");

                //Получаем настройки БД Slave
                this.SlaveAuthorization = CL.LoadConfig("SlaveAutorization");
                this.SlaveServerName = CL.LoadConfig("SlaveServerName");
                this.SlaveDBName = CL.LoadConfig("SlaveDBName");
                this.SlaveDBUser = CL.LoadConfig("SlaveDBUser");
                this.SlaveDBPassword = CL.LoadConfig("SlaveDBPassword");

                //Получаем дополнительные настройки программы
                this.SchemaName = CL.LoadConfig("SchemaName");
                this.MainTimerValue = CL.LoadIntConfig("MainTimerValue");
                this.MaxDBErrorCount = CL.LoadIntConfig("MaxDBErrorCount");
                this.SecondaryTimer = CL.LoadIntConfig("SecondaryTimer");
                
                //Получаем настройки почты
                this.AdminEMail = CL.LoadConfig("AdminEmail");
                this.SmtpHost = CL.LoadConfig("SmtpHost");
                this.SmtpPort = CL.LoadIntConfig("SmtpPort");
                this.SmtpUser = CL.LoadConfig("SmtpUser");
                this.SmtpPassword = CL.LoadConfig("SmtpPassword");
                this.ProgMail = CL.LoadConfig("ProgMail");
                int send = CL.LoadIntConfig("SendMail");
                if (send == 1)
                {
                    this.SendMail = true;
                }
                else
                {
                    this.SendMail = false;
                }
            }
            catch (System.IO.FileNotFoundException exp) {
                string errorMsg = String.Format("File Not Found. Check file address. \n Error details:\n {0}", exp.Message);
                Console.WriteLine(errorMsg);
                _log.ErrorFormat(errorMsg);
            }
            catch (System.Xml.XmlException exp) {
                string errorMsg = String.Format("Error in XML file. \n Error details:\n {0}", exp.Message);
                Console.WriteLine(errorMsg);
                _log.ErrorFormat(errorMsg);
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

