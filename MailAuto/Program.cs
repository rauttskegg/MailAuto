using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;

namespace MailAuto
{
	internal class Program
	{
		static void Main (string[] args)
		{
			var message = new MimeMessage ();
			message.From.Add (new MailboxAddress ("Anton", "a.kadantsev@mtsr.krasnodar.ru"));
			message.To.Add (new MailboxAddress ("Mrs. Chanandler Bong", "kadantsev.anton@yandex.ru"));
			message.Subject = "Test";

			message.Body = new TextPart ("plain") {
				Text = @"Hey Chandler,
				I just wanted to let you know that Monica and I were going to go play some paintball, you in?
				-- Joey"
			};

			using (var client = new SmtpClient ()) {
				client.Connect ("m.krasnodar.ru", 465, SecureSocketOptions.None);

				// Note: only needed if the SMTP server requires authentication
				client.Authenticate ("MTSR/a.kadantsev", "Vbhnhelvfq1");

				client.Send (message);
				client.Disconnect (true);
			}
			Console.ReadKey ();
		}
	}
}
