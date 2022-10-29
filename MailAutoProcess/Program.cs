using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using System.Threading;
using System.IO;

namespace MailAutoProcess
{
	internal class Program
	{

		static void Main (string[] args)
		{
			while (true) {
				ConfigGet configGet = new ConfigGet ();
				SendMail mail = new SendMail ();
				for (int i = 0; i < configGet.AdresTo.Length; i++) {
					if (configGet.AttachmentFile[i].Length > 0) {
						mail.Send (configGet.NameAdresFrom,
								   configGet.AdressFrom,
								   configGet.NameAdresTo,
								   configGet.AdresTo[i],
								   configGet.Title[i],
								   configGet.TextBody,
								   configGet.AttachmentFile[i],
								   configGet.Server,
								   configGet.Login,
								   configGet.Pass,
								   configGet.Port);
						for (int j = 0; j < configGet.AttachmentFile[i].Length; j++)
							File.Delete (configGet.AttachmentFile[i][j]);
					}
				}
				Console.WriteLine ("Sent");
				Thread.Sleep (configGet.TimeSleep);
			}
			Console.ReadKey ();
		}
	}
}
