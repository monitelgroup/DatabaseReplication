using System;
using System.Xml;

namespace replication
{
    class ConfLoader
    {
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
