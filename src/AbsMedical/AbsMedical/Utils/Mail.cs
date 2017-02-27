using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AbsMedical.Data;

namespace AbsMedical.Utils
{
    public class Mail
    {

        /*******GMAIL*********/
        //https://www.google.com/settings/security/lesssecureapps a activé ! pour l'expediteur


        private static string fromEmail = "martines.magnin@gmail.com";
        private static string fromPassword = "stefanoadrien06";

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
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
        }


        public static bool Send(mail mail, string to, string subject, StringBuilder body, Attachment attachment)
        {
            try
            {
                /***** Configuration of the message to send *****/
                MailMessage mailMessage = new MailMessage();
                //Receiver
                mailMessage.From = new MailAddress(mail.Email);
                //Sender
                mailMessage.To.Add(to);
                //Subject
                mailMessage.Subject = subject;
                //Body
                mailMessage.Body = body.ToString();
                //Attachment
                if (attachment != null)
                {
                    mailMessage.Attachments.Add(attachment);
                }

                /***** Configuration of the Smtp Client *****/
                SmtpClient client = new SmtpClient(mail.Smtp);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mail.Email, mail.Password);
                client.Port = mail.Port;
                client.EnableSsl = true;
                client.Send(mailMessage);
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
        }
    }
}
