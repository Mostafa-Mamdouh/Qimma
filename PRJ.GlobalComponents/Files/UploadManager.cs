using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.GlobalComponents.Files
{
	public class UploadManager : IUploadManager
	{
		private readonly string _contentRootPath;
		private readonly string _sectionValue;

		public UploadManager(string contentRootPath, string sectionValue)
		{
			_contentRootPath = contentRootPath;
			_sectionValue = sectionValue;
		}
		public string UploadDocument(string? strBase64, string? fileName)
		{
			string fileLocation = "";
			try
			{
				byte[] Bytes = DecodeUrlBase64(strBase64);
				string fullPath = _contentRootPath + _sectionValue;

				if (!Directory.Exists(fullPath))
					Directory.CreateDirectory(fullPath);

				string toUpload = Path.Combine(_contentRootPath, fullPath + fileName);
				File.WriteAllBytes(toUpload, Bytes);

				fileLocation = _sectionValue + fileName;
			}
			catch (Exception exp)
			{
				var err = exp.Message;
				fileLocation = "";
			}

			return fileLocation;
		}
		public static string GetDocumentName(string? fileName)
		{
			Random rnd = new Random();

			DateTime _dtName = DateTime.Now;
			return _dtName.Year + "" + _dtName.Month + "" + _dtName.Day + "" + _dtName.Minute + "" + _dtName.Second + "_" + GetRandomNumber() + Path.GetExtension(fileName);
		}
		private static int GetRandomNumber()
		{
			Random rnd = new Random();

			return rnd.Next(100, 999);
		}
		private static byte[] DecodeUrlBase64(string? s)
		{
			s = s.Replace('-', '+').Replace('_', '/').PadRight(4 * ((s.Length + 3) / 4), '=');
			return Convert.FromBase64String(s);
		}
		
	}
}
