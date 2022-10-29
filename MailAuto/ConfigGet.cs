using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MailAuto
{
	public class ConfigGet
	{
		private string nameAdresFrom;
		private string adressFrom;
		private string nameAdresTo;
		private string[] adresTo;
		private string title;
		private string textBody;
		private string server;
		private string login;
		private string pass;
		private string[][] attachmentFile;
		private string[][] files;
		private int port;
		private string folderOut;
		private string[] adresToFullPath;
		private int timeSleep;
		public int TimeSleep {
			get => timeSleep;
			set => timeSleep = value;
		}
		public string NameAdresFrom {
			get => nameAdresFrom;
			set => nameAdresFrom = value;
		}
		public string AdressFrom {
			get => adressFrom;
			set => adressFrom = value;
		}
		public string NameAdresTo {
			get => nameAdresTo;
			set => nameAdresTo = value;
		}
		public string[] AdresTo {
			get => adresTo;
			set => adresTo = value;
		}
		public string Title {
			get => title;
			set => title = value;
		}
		public string TextBody {
			get => textBody;
			set => textBody = value;
		}
		public string Server {
			get => server;
			set => server = value;
		}
		public string Login {
			get => login;
			set => login = value;
		}
		public string Pass {
			get => pass;
			set => pass = value;
		}
		public int Port {
			get => port;
			set => port = value;
		}
		public string FolderOut {
			get => folderOut;
			set => folderOut = value;
		}
		public string[][] AttachmentFile
		{
			get => attachmentFile;
			set => attachmentFile = value;
		}
		public string[][] Files {
			get => files;
			set => files = value;
		}
		private string[][] AddPath (string[][] files, string folderOut, string[] adresTo)
		{
			string[][] attachmentFile = files;
			for (int i = 0; i < attachmentFile.Length; i++) {
				for (int j = 0; j < attachmentFile[i].Length; j++) 
				{
					attachmentFile[i][j] = attachmentFile[i][j];
				}
			}
			return attachmentFile;
		}
		private string[][] FileGet (string[] adress, string folderOut)
		{
			string[][] files = new string [adress.Length][];
			for (int i = 0; i < adress.Length; i++) 
			{
				files[i] = Directory.GetFiles (adress[i]);
			}
			return files;
		}
		private string[] GetNameAdreTo (string[] adresTo)
		{
			string[] adresToName = adresTo;
			for (int i = 0; i < adresTo.Length; i++) 
			{
				adresToName[i] = new DirectoryInfo(adresToName[i]).Name;
			}
			return adresToName;
		}
		public ConfigGet ()
		{
			nameAdresFrom = "Anton";
			adressFrom = "a.kadantsev@mtsr.krasnodar.ru";
			nameAdresTo = "Mrs. Chanandler Bong";
			title = "Test";
			textBody = @"Hey Chandler,I just wanted to let you know that Monica";
			server = "m.krasnodar.ru";
			login = "MTSR/a.kadantsev";
			pass = "Vbhnhelvfq1";
			folderOut = "out";
			timeSleep = 3000;
			adresToFullPath = Directory.GetDirectories (folderOut, "*@*");
			adresTo = GetNameAdreTo (Directory.GetDirectories (folderOut, "*@*"));
			files = FileGet(adresToFullPath, folderOut);
			attachmentFile = AddPath (files, folderOut, adresTo);
			port = 465;
		}
	}
}
