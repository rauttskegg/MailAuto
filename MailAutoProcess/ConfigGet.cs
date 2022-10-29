using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.Data.SqlTypes;


namespace MailAutoProcess
{
	public class ConfigGet
	{
		private string nameAdresFrom;
		private string adressFrom;
		private string nameAdresTo;
		private string[] adresTo;
		private string[] title;
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
		private string AddTextToTitle;
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
		public string[] Title {
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
		public string[][] AttachmentFile {
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
				for (int j = 0; j < attachmentFile[i].Length; j++) {
					attachmentFile[i][j] = attachmentFile[i][j];
				}
			}
			return attachmentFile;
		}
		private string[][] FileGet (string[] adress, string folderOut)
		{
			string[][] files = new string[adress.Length][];
			for (int i = 0; i < adress.Length; i++) {
				files[i] =  Directory.GetFiles (adress[i]);
			}
			return files;
		}
		private string[] GetNameAdreTo (string[] adresTo)
		{
			string[] adresToName = adresTo;
			for (int i = 0; i < adresTo.Length; i++) {
				adresToName[i] = new DirectoryInfo (adresToName[i]).Name;
			}
			return adresToName;
		}
		private string[] GetTitle (string[][] file, string AddTextToTitle)
		{
			string[][] fileName = new string[file.Length][];
			for (int i = 0; i < file.Length; i++) 
			{
				fileName[i] = new string[file[i].Length];
				for (int j = 0; j < file[i].Length; j++) 
				{
					fileName[i][j] = file[i][j];
				}
			}
			for (int i = 0; i < fileName.Length; i++) {
				for (int j = 0; j < fileName[i].Length; j++) {
					fileName[i][j] = new DirectoryInfo (fileName[i][j]).Name;
				}
			}
			string[] title = new string[fileName.Length];
			for (int i = 0; i < fileName.Length; i++) 
			{
				for (int j = 0; j < fileName[i].Length && j != 3; j++) 
				{
					if (j == fileName[i].Length - 1 && fileName[i].Length != 0 && j != 3) {
						title[i] += fileName[i][j] + ".";
					} else if (j != 2) 
					{
						title[i] += fileName[i][j] + ", ";
					} 
					else {
						title[i] += fileName[i][j] + "...";
					}
				}
			}
			for (int i = 0; i < title.Length; i++) 
			{
				title[i] = AddTextToTitle + title[i];
			}
			return title;
		}
		private NameValueCollection ConfigFileGetData () {
			NameValueCollection sAll;
			sAll = ConfigurationManager.AppSettings;
			return sAll;
		}
		public ConfigGet ()
		{
			NameValueCollection config = ConfigFileGetData ();
			AddTextToTitle = config.Get ("AddTextToTitle");
			nameAdresFrom = config.Get("nameAdresFrom");
			adressFrom = config.Get("adressFrom");
			nameAdresTo = "";
			textBody = config.Get("textBody");
			server = config.Get ("server");
			login = config.Get ("login");
			pass = config.Get ("pass");
			folderOut = config.Get ("folderOut");
			timeSleep = Convert.ToInt32(config.Get ("timeSleep"));
			adresToFullPath = Directory.GetDirectories (folderOut, config.Get ("addressMask"));
			adresTo = GetNameAdreTo (Directory.GetDirectories (folderOut, config.Get("addressMask")));
			files = FileGet (adresToFullPath, folderOut);
			title = GetTitle (files, AddTextToTitle);
			attachmentFile = files;
			port = Convert.ToInt32(config.Get ("port"));
		}
	}
}
