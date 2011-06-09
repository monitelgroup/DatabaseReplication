using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Replikator
{ 
    public partial class Replika : Form
    {
        string Config = "./ReplicationConfig.xml";

        public Replika()    // Инициализация
        {
            InitializeComponent();
            LoadGUIConfig(this.Config);
        }

        public void LoadGUIConfig(string Config)
        {
            try
            {
                ConfLoader CL = new ConfLoader(Config);
                this.MasterAutorization.Text = CL.LoadConfig("MasterAutorization");
                this.MasterServerName.Text = CL.LoadConfig("MasterServerName");
                this.MasterDBName.Text = CL.LoadConfig("MasterDBName");
                this.MasterUserName.Text = CL.LoadConfig("MasterDBUser");
                this.MasterPassword.Text = CL.LoadConfig("MasterDBPassword");

                this.SlaveAutorization.Text = CL.LoadConfig("SlaveAutorization");
                this.SlaveServerName.Text = CL.LoadConfig("SlaveServerName");
                this.SlaveDBName.Text = CL.LoadConfig("SlaveDBName");
                this.SlaveUserName.Text = CL.LoadConfig("SlaveDBUser");
                this.SlavePassword.Text = CL.LoadConfig("SlaveDBPassword");

                this.AdminEmail.Text = CL.LoadConfig("AdminEmail");
                this.SchemeName.Text = CL.LoadConfig("SchemeName");
                this.Timer.Text = CL.LoadConfig("Timer");
            }
            catch (System.IO.FileNotFoundException) { SaveConfig(this.Config); }
            catch (System.Xml.XmlException) { SaveConfig(this.Config); }
        }

        public void SaveConfig(string Config)    // Сохраняем конфигурацию
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            using (XmlWriter output = XmlWriter.Create(Config, settings))
            {

        /**
		public string smtpHost;
		public int smtpPort;
		public string smtpUser;
		public string smtpPassword;
		public string progMail;
        */
             output.WriteStartElement("ReplicationConfig");
             output.WriteAttributeString("MasterAutorization", this.MasterAutorization.Text);
             output.WriteAttributeString("MasterServerName", this.MasterServerName.Text);
             output.WriteAttributeString("MasterDBName", this.MasterDBName.Text);
             output.WriteAttributeString("MasterDBUser", this.MasterUserName.Text);
             output.WriteAttributeString("MasterDBPassword", this.MasterPassword.Text);

             output.WriteAttributeString("SlaveAutorization", this.SlaveAutorization.Text);
             output.WriteAttributeString("SlaveServerName", this.SlaveServerName.Text);
             output.WriteAttributeString("SlaveDBName", this.SlaveDBName.Text);
             output.WriteAttributeString("SlaveDBUser", this.SlaveUserName.Text);
             output.WriteAttributeString("SlaveDBPassword", this.SlavePassword.Text);

             output.WriteAttributeString("SchemeName", this.SchemeName.Text);
             output.WriteAttributeString("AdminEmail", this.AdminEmail.Text);
             output.WriteAttributeString("Timer", this.Timer.Text);
             output.WriteEndElement();
             output.Flush();
             output.Close();                  
            }
        }

        private void StartReplication_Click(object sender, EventArgs e)
        {
            SaveConfig(this.Config);
        }

        private void loadConfigToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Filter = "XML|*.xml";
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                this.Config = Dialog.FileName;
                LoadGUIConfig(this.Config);
            }
        }

        private void saveConfigToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog Dialog = new SaveFileDialog();
            Dialog.Filter = "XML|*.xml";
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                this.Config = Dialog.FileName;
                SaveConfig(this.Config);
            }
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Program for replication of database");
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartReplication_Click_1(object sender, EventArgs e)
        {

        }
    }

    public class ConfLoader {   // Получение значений конфига
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
