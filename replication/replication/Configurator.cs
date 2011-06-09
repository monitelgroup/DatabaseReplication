using System;


namespace replication
{
    /// <summary>
    /// Заглушка для конфигуратора
    /// </summary>
	public class Configurator
	{

        public string MasterAutorization;
		public string MasterDBName;
        public string MasterDBUser;
        public string MasterDBPassword;
        public string MasterServerName;

        public string SlaveAutorization;
		public string SlaveDBName;
        public string SlaveDBUser;
        public string SlaveDBPassword;
        public string SlaveServerName;
		
        public int MainTimerValue;

		public string SchemeName;
		public string AdminEMail;
		public string smtpHost;
		public int smtpPort;
		public string smtpUser;
		public string smtpPassword;
		public string progMail;

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
            }
            catch (System.IO.FileNotFoundException exp) {
                Console.WriteLine("File Not Found. Check file address. \n Error details:\n {0}",exp.Message);
            }
            catch (System.Xml.XmlException exp) { 
                Console.WriteLine("Error in XML file. \n Error details:\n {0}", exp.Message);
            }
        }
	}
}

