using System;

namespace replication
{
    /// <summary>
    /// Заглушка для конфигуратора
    /// </summary>
	public class Configurator
	{
		public string MasterDBName;
		public string SlaveDBName;
		public int MainTimerValue;
		public int SecondaryTimerValue;
		public int MaxDBErrorCount;
		public string JournalName;
		public string MasterDBUser;
		public string MasterDBPassword;
		public string MasterCompName;
		public string SlaveDBUser;
		public string SlaveDBPassword;
		public string SlaveCompName;
		public string AdminEMail;
		public string smtpHost;
		public int smtpPort;
		public string smtpUser;
		public string smtpPassword;
		public string progMail;
		
		public Configurator()
		{
			this.AdminEMail = "ra6fho@yandex.ru";
			this.JournalName = "testjournal";
			this.MainTimerValue = 2000;
			this.MasterCompName = @"anton-laptop\anton";
            this.MasterDBName = "BasicTSQL";
			this.MasterDBPassword = "password";
			this.MasterDBUser = "ANTON";
			this.MaxDBErrorCount = 20;
			this.progMail = "ra6fho@yandex.ru";
			this.SecondaryTimerValue = 10000;
            this.SlaveCompName = @"anton-laptop\anton";
            this.SlaveDBName = "BasicTSQL";
			this.SlaveDBPassword = "password";
			this.SlaveDBUser = "ANTON";
			this.smtpHost = "smtp.yandex.ru";
			this.smtpPassword = "password";
			this.smtpPort = 587;
			this.smtpUser = "ra6fho";
		}
	}
}

