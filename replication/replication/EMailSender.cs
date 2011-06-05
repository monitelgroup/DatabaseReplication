using System;
using System.Net;
using System.Net.Mail;

namespace replication
{
    /// <summary>
    /// Класс отправки Email сообщений
    /// </summary>
	public class EMailSender
	{
        /// <summary>
        /// Адресс от которого будет отправлено сообщение
        /// </summary>
		string _emailAdress;

        /// <summary>
        /// Адрес smtp сервера
        /// </summary>
		string _smtpHost;

        /// <summary>
        /// Имя пользователя на сервере smtp
        /// </summary>
		string _userName;
		
        /// <summary>
        /// Пароль пользователя на сервере smtp
        /// </summary>
        string _userPassword;
		
        /// <summary>
        /// Порт сервера smtp
        /// </summary>
        int _smtpPort;
		
		SmtpClient _smtp;
		
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="emailAddress">
        /// Адресс от которого будет отправлено сообщение
        /// </param>
        /// <param name="smtpHost">
        /// Адрес smtp сервера
        /// </param>
        /// <param name="userName">
        /// Имя пользователя на сервере smtp
        /// </param>
        /// <param name="userPassword">
        /// Пароль пользователя на сервере smtp
        /// </param>
        /// <param name="smtpPort">
        /// Порт сервера smtp
        /// </param>
		public EMailSender(string emailAddress, string smtpHost, string userName, string userPassword, int smtpPort){
			this._emailAdress = emailAddress;
			this._smtpHost = smtpHost;
			this._smtpPort = smtpPort;
			this._userName = userName;
			this._userPassword = userPassword;
			this._smtp = new SmtpClient(this._smtpHost, this._smtpPort);
			this._smtp.Credentials = new NetworkCredential(this._userName, this._userPassword);
		}
		
        /// <summary>
        /// Отправляет сообщение
        /// </summary>
        /// <param name="message">
        /// Текст сообщения
        /// </param>
        /// <param name="adminEMail">
        /// Адресс куда будет отправлено сообщение
        /// </param>
		public void SendMessage(string message, string adminEMail) {
			MailMessage msg = new MailMessage();
			msg.From = new MailAddress(this._emailAdress);
			msg.To.Add(new MailAddress(adminEMail));
			msg.Subject = "DataBase Replication: Error!";
			msg.Body = message;
			_smtp.Send(msg);
		}
		
	}
}