using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.Workers
{
    public class DTOUpdateStatusWorker : BasedDtoEntity
    {
        public string Remarks { get; set; }
        public DateTime TransferDate { get; set; }
        public virtual string Status { get; set; }

    }
}
