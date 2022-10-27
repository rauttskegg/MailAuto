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
			string nameAdresFrom = "Anton", adressFrom = "a.kadantsev@mtsr.krasnodar.ru",
				   nameAdresTo = "Mrs. Chanandler Bong", adresTo = "kadantsev.anton@yandex.ru",
	               title = "Test", 
				   textBody = @"Hey Chandler,I just wanted to let you know that Monica and 
							    I were going to go play some paintball, you in?
								--Joey",
				   server = "m.krasnodar.ru", login = "MTSR/a.kadantsev", pass = "Vbhnhelvfq1";
			int port = 465;
			string[] attachmentFile = { "file.txt", "file1.txt" };
			SendMail mail = new SendMail ();
			mail.Send (nameAdresFrom, adressFrom, nameAdresTo, adresTo, 
				       title, textBody, attachmentFile, server, login, pass, port);
			Console.WriteLine ("Sent");
			Console.ReadKey ();
		}
	}
}
