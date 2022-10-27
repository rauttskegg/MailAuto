using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;

using static System.Net.Mime.MediaTypeNames;

namespace MailAuto
{
	public class SentMail
	{
		public void Sent (string nameAdresFrom, string adressFrom, 
					     string nameAdresTo, string adresTo, string title,
						 string textBody, string server, string login,
						 string pass, int port)
		{
			var message = new MimeMessage ();

			message.From.Add (new MailboxAddress (nameAdresFrom, adressFrom));
			message.To.Add (new MailboxAddress (nameAdresTo, adresTo));
			message.Subject = title;

			message.Body = new TextPart ("plain") {
				Text = textBody
			};

			using (var client = new SmtpClient ()) {
				client.CheckCertificateRevocation = false;
				client.Connect (server, port, SecureSocketOptions.None);

				// Note: only needed if the SMTP server requires authentication
				client.Authenticate (login, pass);

				client.Send (message);
				client.Disconnect (true);
			}
		}
	}
}
