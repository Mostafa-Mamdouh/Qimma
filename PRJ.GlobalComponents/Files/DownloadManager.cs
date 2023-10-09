using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.GlobalComponents.Files
{
	public class DownloadManager : IDownloadManager
	{
		private readonly string _contentRootPath;
		private readonly string _sectionValue;

		public DownloadManager(string contentRootPath, string sectionValue)
		{
			_contentRootPath = contentRootPath;
			_sectionValue = sectionValue;
		}

		public byte[] GetDocumnetBytes(string? fileName)
		{
			string fullPath = _contentRootPath + _sectionValue + fileName;
			byte[] bytes = File.ReadAllBytes(fullPath);

			return bytes;
		}
	}
}
