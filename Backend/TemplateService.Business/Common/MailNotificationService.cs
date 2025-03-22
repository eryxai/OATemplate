#region Using ...
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Core.Common;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
#endregion

/*
 
 
 */
namespace TemplateService.Business.Common
{
    public class MailNotificationService : IMailNotification
    {
		#region Data Members
		private IConfiguration _iConfiguration;
		private int _port = 0;
		private string _senderEmailHost = "";
		private string _senderEmail = "";
		private string _emailPassword = "";
		private string _senderName = "";
		#endregion

		#region Constructors
		public MailNotificationService(IConfiguration config)
		{
			_iConfiguration = config;

			this.GetAllEmailSettings();
		} 
		#endregion

		private void GetAllEmailSettings()
        {
            this.GetEmailPortFromSettings();
            this.GetEmailHostFromSettings();
            this.GetSenderEmailFromSettings();
            this.GetEmailPasswordFromSettings();
            this.GetSenderNameFromSettings();
        }

        private void GetEmailPortFromSettings()
        {
            try
            {
                string port = _iConfiguration
                    .GetSection("CommonSettings")
                    .GetSection("SenderEmailPort")
                    .Value;

                if (string.IsNullOrEmpty(port))
                {
                    this._port = int.Parse(port);
                }
            }
            catch
            {

            }
        }

        private void GetEmailHostFromSettings()
        {
            try
            {
                this._senderEmailHost = _iConfiguration
                    .GetSection("CommonSettings")
                    .GetSection("SenderEmailHost")
                    .Value;
            }
            catch
            {

            }
        }

        private void GetSenderEmailFromSettings()
        {
            try
            {
                this._senderEmail = _iConfiguration
                    .GetSection("CommonSettings")
                    .GetSection("SenderEmail")
                    .Value;
            }
            catch
            {

            }
        }

        private void GetEmailPasswordFromSettings()
        {
            try
            {
                this._emailPassword = _iConfiguration
                    .GetSection("CommonSettings")
                    .GetSection("EmailPassword")
                    .Value;
            }
            catch
            {

            }
        }

        private void GetSenderNameFromSettings()
        {
            try
            {
                this._senderName = _iConfiguration
                    .GetSection("CommonSettings")
                    .GetSection("SenderName")
                    .Value;
            }
            catch
            {

            }
        }

        public async Task<bool> SendMail(string to, string cc, string bcc, string subject, string body)
        {
            try
            {
                List<string> toEmails = new List<string>();
                if (to != null)
                    toEmails.Add(to);

                List<string> ccList = new List<string>();
                if (cc != null)
                    ccList.Add(cc);

                List<string> bccList = new List<string>();
                if (bcc != null)
                    bccList.Add(bcc);

                await SendMail(toEmails, ccList, bccList, subject, body);
                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> SendMail(List<string> to, List<string> cc, List<string> bcc, string subject, string body)
        {
            try
            {

                var mailMessage = new MimeMessage();
                mailMessage.From.Add(new MailboxAddress(this._senderName, this._senderEmail));

                #region Set To
                if (to != null)
                {
                    foreach (var email in to)
                    {
                        mailMessage.To.Add(new MailboxAddress(email, email));
                    }
                }
                #endregion

                #region Set CC
                if (cc != null)
                {
                    foreach (var email in cc)
                    {
                        mailMessage.Cc.Add(new MailboxAddress(email, email));
                    }
                }
                #endregion

                #region Set BCC
                if (bcc != null)
                {
                    foreach (var email in bcc)
                    {
                        mailMessage.Bcc.Add(new MailboxAddress(email, email));
                    }
                }
                #endregion

                mailMessage.Subject = subject;
                mailMessage.Body = new TextPart("html")
                {
                    Text = body
                };

                //mailMessage.Body = new TextPart("html")
                //{
                //    Text = @"<p>Hey!</p><img src=""http://34.91.31.136:34544/TenantCustomization/GetTenantLogo?skin=light&tenantId=1&id=78cdf77a-8eb5-fe36-3e85-39f8f5cd93d2"">"

                //};

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect(this._senderEmailHost, this._port, true);
                    smtpClient.Authenticate(_senderEmail, _emailPassword);

                    // https://accounts.zoho.com/home#security/app_password

                    try
                    {
                        await smtpClient.SendAsync(mailMessage);
                        smtpClient.Disconnect(true);
                        return true;
                    }
                    catch (Exception )
                    {
                        return false;
                    }


                }

            }

            catch (Exception)
            {
                return false;
                throw;
            }
        }

    }
}