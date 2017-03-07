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
    public static class Mail
    {

        /*******GMAIL*********/
        //https://www.google.com/settings/security/lesssecureapps a activé ! pour l'expediteur


        //private static string fromEmail = "martines.magnin@gmail.com";
        //private static string fromPassword = "stefanoadrien06";

        public static bool IsValidClient(mailconfiguration config)
        {
            return (Send(config, new List<string> { "martines.magnin@gmail.com" }, new List<string> { }, "Mail configuration - Validation test", new StringBuilder("This is a test"), null));
        }

        public static bool Send(mailconfiguration config, List<string> to, List<string> toCC, string subject, StringBuilder body, Attachment attachment)
        {
            try
            {
                /***** Configuration of the message to send *****/
                MailMessage mailMessage = new MailMessage();
                //Receiver
                mailMessage.From = new MailAddress(config.Email);
                //Sender
                foreach(string mailAddress in to)
                {
                    mailMessage.To.Add(mailAddress);
                }
                //Sender CC
                foreach (string mailAddress in toCC)
                {
                    mailMessage.CC.Add(mailAddress);
                }
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
                SmtpClient client = new SmtpClient(config.Smtp);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(config.Email, Encryption.decryptPswd(config.Password));
                client.Port = config.Port;
                client.EnableSsl = true;
                client.Send(mailMessage);
                return true;
            }
            catch (SmtpException e)
            {
                Console.Write(e);
                return false;
            }
        }

        //public static bool Send(string to, string subject, StringBuilder body, Attachment attachment)
        //{
        //    try
        //    {
        //        MailMessage mail = new MailMessage();
        //        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        //        mail.From = new MailAddress(fromEmail);
        //        mail.To.Add(to);
        //        mail.Subject = subject;
        //        mail.Body = body.ToString();

        //        if (attachment != null)
        //        {
        //            mail.Attachments.Add(attachment);
        //        }

        //        SmtpServer.UseDefaultCredentials = false;
        //        SmtpServer.Credentials = new NetworkCredential(fromEmail, fromPassword);
        //        SmtpServer.Port = 587;
        //        SmtpServer.EnableSsl = true;
        //        SmtpServer.Send(mail);
        //        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
        //        return true;
        //    }
        //    catch (SmtpException)
        //    {
        //        return false;
        //    }
        //}
    }
}
