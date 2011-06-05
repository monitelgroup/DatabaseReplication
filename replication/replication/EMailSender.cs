using System;
using System.Net;
using System.Net.Mail;

namespace replication
{
    /// <summary>
    /// ����� �������� Email ���������
    /// </summary>
	public class EMailSender
	{
        /// <summary>
        /// ������ �� �������� ����� ���������� ���������
        /// </summary>
		string _emailAdress;

        /// <summary>
        /// ����� smtp �������
        /// </summary>
		string _smtpHost;

        /// <summary>
        /// ��� ������������ �� ������� smtp
        /// </summary>
		string _userName;
		
        /// <summary>
        /// ������ ������������ �� ������� smtp
        /// </summary>
        string _userPassword;
		
        /// <summary>
        /// ���� ������� smtp
        /// </summary>
        int _smtpPort;
		
		SmtpClient _smtp;
		
        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="emailAddress">
        /// ������ �� �������� ����� ���������� ���������
        /// </param>
        /// <param name="smtpHost">
        /// ����� smtp �������
        /// </param>
        /// <param name="userName">
        /// ��� ������������ �� ������� smtp
        /// </param>
        /// <param name="userPassword">
        /// ������ ������������ �� ������� smtp
        /// </param>
        /// <param name="smtpPort">
        /// ���� ������� smtp
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
        /// ���������� ���������
        /// </summary>
        /// <param name="message">
        /// ����� ���������
        /// </param>
        /// <param name="adminEMail">
        /// ������ ���� ����� ���������� ���������
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