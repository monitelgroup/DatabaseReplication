using System;
using System.Net;
using System.Net.Mail;

namespace replication
{
	public class EMailSender
	{
		string _emailAdress;
		string _smtpHost;
		string _userName;
		string _userPassword;
		int _smtpPort;
		
		SmtpClient _smtp;
		
		public EMailSender(string emailAddress, string smtpHost, string userName, string userPassword, int smtpPort){
			this._emailAdress = emailAddress;
			this._smtpHost = smtpHost;
			this._smtpPort = smtpPort;
			this._userName = userName;
			this._userPassword = userPassword;
			this._smtp = new SmtpClient(this._smtpHost, this._smtpPort);
			this._smtp.Credentials = new NetworkCredential(this._userName, this._userPassword);
		}
		
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