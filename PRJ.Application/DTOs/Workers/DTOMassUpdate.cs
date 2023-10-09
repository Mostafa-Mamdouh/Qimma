using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.Workers
{
    public class DTOMassUpdate
    { 
        public int WorkerId { get; set; }
        public decimal Quartervalue{ get; set; }
        public string DosimeterType { get; set; }
    }
}
