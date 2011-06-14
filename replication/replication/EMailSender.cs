using System;
using System.Net;
using System.Net.Mail;
using log4net;
using log4net.Config;

namespace replication
{
    /// <summary>
    /// ����� �������� Email ���������
    /// </summary>
	public class EMailSender
	{
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(this._emailAdress);
                msg.To.Add(new MailAddress(adminEMail));
                msg.Subject = "DataBase Replication: Error!";
                msg.Body = message;
                _smtp.Send(msg);
            }
            catch (System.Net.Mail.SmtpException exp)
            {
                string errorMsg = String.Format("Error send message. \n Error details: \n {0}", exp.Message);
                Console.WriteLine(errorMsg);
                _log.ErrorFormat(errorMsg);
            }
            catch (System.ArgumentNullException exp)
            {
                string errorMsg = String.Format("Error in config. \n Error details: \n {0}", exp.Message);
                Console.WriteLine(errorMsg);
                _log.ErrorFormat(errorMsg);
            }
            catch (System.ArgumentException exp)
            {
                string errorMsg = String.Format("Error in config. \n Error details: \n {0}", exp.Message);
                Console.WriteLine(errorMsg);
                _log.ErrorFormat(errorMsg);
            }
		}
		
	}
}