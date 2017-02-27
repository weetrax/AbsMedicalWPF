using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AbsMedical.Utils
{
    public class Mail
    {
        private static string fromEmail = "martines.magnin@gmail.com";
        private static string fromPassword = "stefanoadrien06";
        //mail address: martines.magnin@gmail.com
        //password: stefanoadrien06
        public static bool Send(string to, string subject, StringBuilder body, Attachment attachment)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(fromEmail);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body.ToString();

                if (attachment != null)
                {
                    mail.Attachments.Add(attachment);
                }

                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential(fromEmail, fromPassword);
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (SmtpException e)
            {
                return false;
                throw e;
            }
        }
    }
}
