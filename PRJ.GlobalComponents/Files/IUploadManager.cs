using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.GlobalComponents.Files
{
	public interface IUploadManager
	{
		string UploadDocument(string? strBase64, string? docExt);	
	}
}
