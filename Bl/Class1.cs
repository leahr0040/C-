using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Dto;
using Dal;
using System.IO;

namespace Bl
{
    public class Class1
    {
        public static string Mail(string subject, MailAddress mailAddress, string body)
        {
            MailAddress mail = new MailAddress("e@gmail.com", "השם של השולח/הפרויקט");
            string path = @"C:";
            using (MailMessage mailMessage = new MailMessage(mail, mailAddress))
            {
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Subject = subject;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Timeout = 30 * 1000;

                client.Credentials = new NetworkCredential("e@gmail.com", "סיסמת המייל");
                client.Port = 587;
                client.EnableSsl = true;
                Attachment att = new Attachment(path);
                mailMessage.Attachments.Add(att);
                try
                {
                    client.Send(mailMessage);
                    return "המייל נשלח בהצלחה";
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
    
