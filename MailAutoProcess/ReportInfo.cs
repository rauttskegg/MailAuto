using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAutoProcess
{
	internal class ReportInfo
	{
		public void showReportInfoAdres (ConfigGet configGet, int i)
		{
			Console.WriteLine ("Письмо отправлено");
			Console.WriteLine ($"Получатель: {configGet.AdresTo[i]}") ;
			Console.WriteLine ($"Вложение: {configGet.FilesNames[i].Length}") ;
			for (int j = 0; j < configGet.FilesNames[i].Length; j++) 
			{
				Console.WriteLine (configGet.FilesNames[i][j]);
			}
			Console.WriteLine ();
		}
	}
}
