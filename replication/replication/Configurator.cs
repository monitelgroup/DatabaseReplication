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
        public string MasterAuthorization;
		
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
        public string SlaveAuthorization;

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
		public string SchemaName;

        /// <summary>
        /// E-mail ������ ��������������
        /// </summary>
		public string AdminEMail;

        /// <summary>
        /// ����� SMTP ������� ��� �������� �����
        /// </summary>
		public string SmtpHost;

        /// <summary>
        /// ���� SMTP ������� ��� �������� �����
        /// </summary>
		public int SmtpPort;

        /// <summary>
        /// ��� ������������ SMTP ������� ��� �������� �����
        /// </summary>
		public string SmtpUser;

        /// <summary>
        /// ����� ������������ SMTP ������� ��� �������� �����
        /// </summary>
		public string SmtpPassword;

        /// <summary>
        /// E-mail ������ �� �������� ����� ���������� ���������
        /// </summary>
		public string ProgMail;

        /// <summary>
        /// ����������� ���-�� ������ ��� ����������� � ��.
        /// </summary>
        public int MaxDBErrorCount;
        
        /// <summary>
        /// ����� ����� ������� ����� ��������� ��������� ���������� � ��
        /// </summary>
        public int SecondaryTimer;
        
        /// <summary>
        /// True - ���������� ��������� �� ������� �� e-mail ��������������. False - �� ����������
        /// </summary>
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
                //�������� ��������� �� Master
                this.MasterAuthorization = CL.LoadConfig("MasterAutorization");
                this.MasterServerName = CL.LoadConfig("MasterServerName");
                this.MasterDBName = CL.LoadConfig("MasterDBName");
                this.MasterDBUser = CL.LoadConfig("MasterDBUser");
                this.MasterDBPassword = CL.LoadConfig("MasterDBPassword");

                //�������� ��������� �� Slave
                this.SlaveAuthorization = CL.LoadConfig("SlaveAutorization");
                this.SlaveServerName = CL.LoadConfig("SlaveServerName");
                this.SlaveDBName = CL.LoadConfig("SlaveDBName");
                this.SlaveDBUser = CL.LoadConfig("SlaveDBUser");
                this.SlaveDBPassword = CL.LoadConfig("SlaveDBPassword");

                //�������� �������������� ��������� ���������
                this.SchemaName = CL.LoadConfig("SchemaName");
                this.MainTimerValue = CL.LoadIntConfig("MainTimerValue");
                this.MaxDBErrorCount = CL.LoadIntConfig("MaxDBErrorCount");
                this.SecondaryTimer = CL.LoadIntConfig("SecondaryTimer");
                
                //�������� ��������� �����
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

