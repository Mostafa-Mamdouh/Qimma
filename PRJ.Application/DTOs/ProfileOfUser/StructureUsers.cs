using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.DTOs.ProfileOfUser
{
	public class StructureUsers
	{
        public long EmployeeNo { get; set; }
        public string? ImagePath { get; set; }
        public string? FirstManagerName { get; set; }
        public string? SecondManagerName { get; set; }
        public string? ThirdManagerName { get; set; }
        public string? FourthManagerName { get; set; }
        public Nullable<long> FirstManagerId { get; set; }
        public Nullable<long> SecondManagerId { get; set; }
        public Nullable<long> ThirdManagerId { get; set; }
        public Nullable<long> FourthManagerId { get; set; }
        public string? FirstManagerImagePath { get; set; }
        public string? SecondManagerImagePath { get; set; }
        public string? ThirdManagerImagePath { get; set; }
        public string? FourthManagerImagePath { get; set; }
        public string? GeneralDepartmentCode { get; set; }
        public string? DepartmentCode { get; set; }
        public string? GeneralDepartment { get; set; }
    }
}
