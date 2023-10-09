using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.Workers
{
    public class DTOFileInfo
    {
        public string ColName { get; set; }
        public int quarter { get; set; }
        public string Remarks { get; set; }
        public string year { get; set; }
        public IFormFile formFile { get; set; }
    }
}
