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
        string Config = "./Replikator.xml";

        public Replika()    // Инициализация
        {
            InitializeComponent();
            LoadGUIConfig(this.Config);
        }

        private void LoadGUIConfig(string Config)
        {
            try
            {
                ConfLoader CL = new ConfLoader(Config);
                this.MasterAutorization.Text = CL.LoadConfig("MasterAutorization");
                this.MasterServerName.Text = CL.LoadConfig("MasterServerName");
                this.MasterUserName.Text = CL.LoadConfig("MasterUserName");
                this.MasterPassword.Text = CL.LoadConfig("MasterPassword");
                this.SlaveAutorization.Text = CL.LoadConfig("SlaveAutorization");
                this.SlaveServerName.Text = CL.LoadConfig("SlaveServerName");
                this.SlaveUserName.Text = CL.LoadConfig("SlaveUserName");
                this.SlavePassword.Text = CL.LoadConfig("SlavePassword");
                this.AdminEmail.Text = CL.LoadConfig("AdminEmail");
                this.JournalName.Text = CL.LoadConfig("JournalName");
                this.Timer.Text = CL.LoadConfig("Timer");
                this.MaxBDError.Text = CL.LoadConfig("MaxBDError");
            }
            catch (System.IO.FileNotFoundException) { SaveConfig(this.Config); }
            catch (System.Xml.XmlException) { SaveConfig(this.Config); }
        }

        private void SaveConfig(string Config)    // Сохраняем конфигурацию
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            using (XmlWriter output = XmlWriter.Create(Config, settings))
            {
             output.WriteStartElement("Replicator");
             output.WriteAttributeString("MasterAutorization", this.MasterAutorization.Text);
             output.WriteAttributeString("MasterServerName", this.MasterServerName.Text);
             output.WriteAttributeString("MasterUserName", this.MasterUserName.Text);
             output.WriteAttributeString("MasterPassword", this.MasterPassword.Text);
             output.WriteAttributeString("SlaveAutorization", this.SlaveAutorization.Text);
             output.WriteAttributeString("SlaveServerName", this.SlaveServerName.Text);
             output.WriteAttributeString("SlaveUserName", this.SlaveUserName.Text);
             output.WriteAttributeString("SlavePassword", this.SlavePassword.Text);
             output.WriteAttributeString("AdminEmail", this.AdminEmail.Text);
             output.WriteAttributeString("JournalName", this.JournalName.Text);
             output.WriteAttributeString("Timer", this.Timer.Text);
             output.WriteAttributeString("MaxBDError", this.MaxBDError.Text);
             output.WriteEndElement();
             output.Flush();
             output.Close();                  
            }
        }

        private void StartReplication_Click(object sender, EventArgs e)
        {
            SaveConfig(this.Config);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog  Dialog = new OpenFileDialog ();
            Dialog.Filter = "XML|*.xml";
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                this.Config = Dialog.FileName;
                LoadGUIConfig(this.Config);
            }
        }

        private void saveConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Dialog = new SaveFileDialog();
            Dialog.Filter = "XML|*.xml";
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                this.Config = Dialog.FileName;
                SaveConfig(this.Config);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
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
