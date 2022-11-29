using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MailAutoProcess
{
	internal class Program
	{

		static void Main (string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.InputEncoding = System.Text.Encoding.UTF8;
			Console.WriteLine ("Current Input Encoding Scheme: {0}", Console.InputEncoding);
			Console.WriteLine ("Старт");
			while (true) {
				ConfigGet configGet = new ConfigGet ();
				SendMail mail = new SendMail ();
				ReportInfo reportInfo = new ReportInfo ();
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
						reportInfo.showReportInfoAdres (configGet, i);
						for (int j = 0; j < configGet.AttachmentFile[i].Length; j++)
							File.Delete (configGet.AttachmentFile[i][j]);
					}
				}
				
				Thread.Sleep (configGet.TimeSleep);
			}
		}
	}
}
