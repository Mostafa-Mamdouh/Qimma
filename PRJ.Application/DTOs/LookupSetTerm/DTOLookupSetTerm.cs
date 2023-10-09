using PRJ.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.LookupSetTerm
{
    public class DTOLookupSetTerm : BasedDtoEntity
    {
        public int LookupSetId { get; set; }
        public string Value { get; set; }
        public string DisplayNameAr { get; set; }
        public string DisplayNameEn { get; set; }
        public string ExtraData1 { get; set; }
        public string ExtraData2 { get; set; }
        public bool IsActive { get; set; }
    }
}
