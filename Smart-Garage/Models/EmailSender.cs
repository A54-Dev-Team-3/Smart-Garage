using Smart_Garage.Models.Contracts;
using System.Net;
using System.Net.Mail;

namespace Smart_Garage.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string reciever, string subject, string message)
        {
            var mail = "hristian.kr.nikolov.spam@gmail.com";
            var pw = "";

			var client = new SmtpClient("smtp.gmail.com", 587)
			{
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            var mailMessage = new MailMessage(from: mail, to: reciever, subject, message)
            {
                IsBodyHtml = true // Set IsBodyHtml to true to indicate that the message body contains HTML
            };

            return client.SendMailAsync(mailMessage);
        }
    }
}
