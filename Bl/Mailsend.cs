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
    public class Mailsend
    {
        public static void Mailnewuser(UserDTO ud)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {

                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("argamanexpress@gmail.com");
                    mail.To.Add(ud.Email);
                    // mail.Bcc.Add(mail1);
                    mail.Subject = "כניסת משתמש לאתר ארגמן אקספרס";
                    mail.IsBodyHtml = true;

                    string ht = @"<html>
                    <body style='color:blueviolet;font-size:150%'>
          <a style='font-size:120%;font-family:'Gill Sans','Gill Sans MT',Calibri,'Trebuchet MS',sans-serif;text-align:left'> ברוכים הבאים " + ud.FirstName + " " + ud.LastName + @" </a><br/>שם המשתמש שלך: " + ud.UserName + @"<br/>הסיסמה שלך: " + ud.Password + @"
                <br/><a href='http://ArgamanExpress.co.il/'>כניסה לאתר</a></body></html>";

                    // < input type = 'date' id = 'date1' onchange = 'document.getElementById('link').href+=document.getElementById('date1').value' >
                    mail.Body = ht;
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("argamanexpress", "es211860663");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }
        public static void Mailforgotpasword(User ud)
        {
            using (ArgamanExpressEntities db = new ArgamanExpressEntities())
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("argamanexpress@gmail.com");
                    mail.To.Add(ud.Email);
                    // mail.Bcc.Add(mail1);
                    mail.Subject = "כניסת משתמש לאתר ארגמן אקספרס";
                    mail.IsBodyHtml = true;

                    string ht = @"<html>
                    <body style='color:blueviolet;font-size:150%'>
          <a style='font-size:120%;font-family:'Gill Sans','Gill Sans MT',Calibri,'Trebuchet MS',sans-serif;text-align:left'> ברוכים הבאים" + ud.FirstName + " " + ud.LastName + @" </a><br/>הסיסמה שלך: " + ud.Password + @"
                <br/><a href='http://ArgamanExpress.co.il/'>כניסה לאתר</a></body></html>";

                    // < input type = 'date' id = 'date1' onchange = 'document.getElementById('link').href+=document.getElementById('date1').value' >
                    mail.Body = ht;
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("argamanexpress", "es211860663");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }

    }


}


















//            MailAddress mail = new MailAddress("e@gmail.com", "השם של השולח/הפרויקט");
//            string path = @"C:";
//            using (MailMessage mailMessage = new MailMessage(mail1,))
//            {
//                mailMessage.IsBodyHtml = true;
//                mailMessage.Body = body;
//                mailMessage.Subject = subject;
//                SmtpClient client = new SmtpClient("smtp.gmail.com");
//                client.EnableSsl = true;
//                client.DeliveryMethod = SmtpDeliveryMethod.Network;
//                client.UseDefaultCredentials = false;
//                client.Timeout = 30 * 1000;

//                client.Credentials = new NetworkCredential("e@gmail.com", "סיסמת המייל");
//                client.Port = 587;
//                client.EnableSsl = true;
//                Attachment att = new Attachment(path);
//                mailMessage.Attachments.Add(att);
//                try
//                {
//                    client.Send(mailMessage);
//                    return "המייל נשלח בהצלחה";
//                }
//                catch
//                {
//                    return null;
//                }
//            }
//        }
//    }
//}

