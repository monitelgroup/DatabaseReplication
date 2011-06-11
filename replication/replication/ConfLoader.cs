using System;
using System.Xml;

namespace replication
{
    /// <summary>
    /// Класс позволяющий считать настройки из XML файла
    /// </summary>
    class ConfLoader
    {

        XmlDocument _xmlDoc;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="ConfXML">
        /// Имя конфигурационного файла
        /// </param>
        public ConfLoader(string ConfXML)
        {
            this._xmlDoc = new XmlDocument();
            this._xmlDoc.Load(ConfXML);
        }

        /// <summary>
        /// Получение значения параметра по его имени в виде строки
        /// </summary>
        /// <param name="param">
        /// Имя параметра
        /// </param>
        /// <returns>
        /// Значение параметра, либо пустая строка, если он отсутствует
        /// </returns>
        public string LoadConfig(string param)
        {
            foreach (XmlNode attr in this._xmlDoc.DocumentElement.Attributes)
            {
                if (param == attr.Name) { return attr.Value; }
            }
            return "";
        }

        /// <summary>
        /// Получение значения параметра по его имени в виде числа
        /// </summary>
        /// <param name="param">
        /// Имя параметра
        /// </param>
        /// <returns>
        /// Значение параметра
        /// </returns>
        public int LoadIntConfig(string param)  // Пытаемся преобразовать параметр в число
        {
            return System.Convert.ToInt32(LoadConfig(param));
        }
    }
}
