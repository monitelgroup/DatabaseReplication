using System;
using System.Xml;

namespace replication
{
    /// <summary>
    /// Заглушка для конфигуратора
    /// </summary>
	public class Configurator
	{
		public string MasterDBName;
        public string MasterDBUser;
        public string MasterDBPassword;
        public string MasterServerName;
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
                CL.LoadConfig("MasterAutorization");
                CL.LoadConfig("MasterServerName");
                CL.LoadConfig("MasterDBName");
                CL.LoadConfig("MasterDBUser");
                CL.LoadConfig("MasterDBPassword");

                CL.LoadConfig("SlaveAutorization");
                CL.LoadConfig("SlaveServerName");
                CL.LoadConfig("SlaveDBName");
                CL.LoadConfig("SlaveDBUser");
                CL.LoadConfig("SlaveDBPassword");

                CL.LoadConfig("AdminEmail");
                CL.LoadConfig("SchemeName");
                CL.LoadConfig("Timer");
            }
            catch (System.IO.FileNotFoundException) { }
            catch (System.Xml.XmlException) { }
        }

            public class ConfLoader
                {   // Получение значений конфига
                     XmlDocument xmlDoc;
                    
            public ConfLoader(string ConfXML)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfXML);
                this.xmlDoc = xmlDoc;
            }

            public string LoadConfig(string param)    // Читеем параметр из конфига, если его нет возвращеем пустую строку
            {
                foreach (XmlNode attr in this.xmlDoc.DocumentElement.Attributes)
                {
                    if (param == attr.Name) { return attr.Value; }
                }
                return "";
            }

            public int LoadIntConfig(string param)  // Пытаемся преобразовать параметр в число
            {
                return System.Convert.ToInt32(LoadConfig(param));
            }
        }

	}
}

