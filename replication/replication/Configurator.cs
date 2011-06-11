using System;
using log4net;
using log4net.Config;

namespace replication
{
    /// <summary>
    /// ����� ��������������� ��������� ���������
    /// </summary>
	public class Configurator
	{

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// ��� ����������� MS SQL (Windows || Sql) ��� �� Master
        /// </summary>
        public string MasterAutorization;
		
        /// <summary>
        /// ��� ���� ������ master
        /// </summary>
        public string MasterDBName;

        /// <summary>
        /// ��� ������������ ���� ����� master (��� ����������� sql)
        /// </summary>
        public string MasterDBUser;

        /// <summary>
        /// ������ ������������ ���� ����� master (��� ����������� sql)
        /// </summary>
        public string MasterDBPassword;

        /// <summary>
        /// ��� ������� MS SQL ��� �� master (��� ����������� windows)
        /// </summary>
        public string MasterServerName;

        /// <summary>
        /// ��� ����������� MS SQL (Windows || Sql) ��� �� Slave
        /// </summary>
        public string SlaveAutorization;

        /// <summary>
        /// ��� ���� ������ slave
        /// </summary>
        public string SlaveDBName;

        /// <summary>
        /// ��� ������������ ���� ����� slave (��� ����������� sql)
        /// </summary>
        public string SlaveDBUser;

        /// <summary>
        /// ������ ������������ ���� ����� slave (��� ����������� sql)
        /// </summary>
        public string SlaveDBPassword;

        /// <summary>
        /// ��� ������� MS SQL ��� �� slave (��� ����������� windows)
        /// </summary>
        public string SlaveServerName;
		
        /// <summary>
        /// �������� ������������ �������, �� �������� ����� ����������� �������� �������
        /// </summary>
        public int MainTimerValue;

        /// <summary>
        /// ��� ����� � ������� ����� ������������� �������
        /// </summary>
		public string SchemeName;

        /// <summary>
        /// E-mail ������ ��������������
        /// </summary>
		public string AdminEMail;

        /// <summary>
        /// ����� SMTP ������� ��� �������� �����
        /// </summary>
		public string smtpHost;

        /// <summary>
        /// ���� SMTP ������� ��� �������� �����
        /// </summary>
		public int smtpPort;

        /// <summary>
        /// ��� ������������ SMTP ������� ��� �������� �����
        /// </summary>
		public string smtpUser;

        /// <summary>
        /// ����� ������������ SMTP ������� ��� �������� �����
        /// </summary>
		public string smtpPassword;

        /// <summary>
        /// E-mail ������ �� �������� ����� ���������� ���������
        /// </summary>
		public string progMail;

        public int maxDBErrorCount;
        public int secondaryTimer;
        public bool SendMail;

        /// <summary>
        /// ����������� ������. �������� ��������� �� ����� ������������
        /// </summary>
        /// <param name="configFileName">
        /// ��� ������������
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
        /// ��������� ��� ����������
        /// </summary>
        /// <param name="authType">
        /// ��� ���������� � ���� ������: Windows ��� Sql
        /// </param>
        /// <returns>
        /// True - ���� Windows. ����� False
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

