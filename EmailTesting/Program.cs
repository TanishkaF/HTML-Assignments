using System;

using System.Net;
using System.Net.Mail;

namespace TestClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            MailAddress to = new MailAddress("20051044@kiit.ac.in");
            MailAddress from = new MailAddress("foglatanishka@gmail.com");

            MailMessage email = new MailMessage(from, to);
            email.Subject = "Testing out email sending";
            email.Body = "Hello all the way from the land of C#";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "sandbox.smtp.mailtrap.io";
            smtp.Port = 2525;
            smtp.Credentials = new NetworkCredential("c463557db0809b", "b7e97f832d6546");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(email);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}