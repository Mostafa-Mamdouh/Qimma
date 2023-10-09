using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PRJ.Application.DTOs.Common
{
	public class MailRequest
	{
		
		public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public string Id { get; set; }


    }
}
