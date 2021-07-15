using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MultipleMailSender
{
    class Mail_Sender
    {
        public  bool SendMail(string aliciMail,string konu, string mesaj,string smtpServerName,string gondericiMail,string sifre,int port)
        {
            try
            {

                SmtpClient SmtpServer = new SmtpClient(smtpServerName);
                var mail = new MailMessage();
                mail.From = new MailAddress(gondericiMail);
                mail.To.Add(aliciMail);
                mail.Subject = konu;
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = mesaj;
                mail.Body = htmlBody;
                SmtpServer.Port = port;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(gondericiMail, sifre);
                SmtpServer.EnableSsl = true;
                SmtpServer.SendMailAsync(mail);
                System.Threading.Thread.Sleep(500);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public  bool SendMailWithAttachment(string aliciMail, string konu, string mesaj, string smtpServerName, string gondericiMail, string sifre, int port,string attachmentPath)
        {
            try
            {

                SmtpClient SmtpServer = new SmtpClient(smtpServerName);
                var mail = new MailMessage();
                mail.From = new MailAddress(gondericiMail);
                mail.To.Add(aliciMail);
                mail.Subject = konu;
                mail.IsBodyHtml = true;
                mail.Attachments.Add(new Attachment(attachmentPath));
                string htmlBody;
                htmlBody = mesaj;
                mail.Body = htmlBody;
                SmtpServer.Port = port;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(gondericiMail, sifre);
                SmtpServer.EnableSsl = true;
                SmtpServer.Timeout = 2000;
                SmtpServer.SendMailAsync(mail);
                System.Threading.Thread.Sleep(500);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
