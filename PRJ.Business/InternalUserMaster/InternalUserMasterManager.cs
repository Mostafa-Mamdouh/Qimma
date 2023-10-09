using System.Threading.Tasks;
using rep = PRJ.Repository;
using ent = PRJ.Domain.Entities;
using lg = PRJ.GlobalComponents.Logger;
using cns = PRJ.GlobalComponents.Const;
using wap = PRJ.Application.Warppers;
using dto = PRJ.Application.DTOs;

using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using PRJ.GlobalComponents.Encryption;
using System.Runtime.InteropServices;

namespace PRJ.Business.InternalUserMaster
{
	public class InternalUserMasterManager
	{
		public readonly IMapper _mapper;

		public InternalUserMasterManager(IMapper mapper)
		{

			_mapper = mapper;
		}

		public wap.Response<dto.InternalMasterUser.DTOInternalMasterUserBasic> GetUserInfo(string Username)
		{
			
				return new wap.Response<dto.InternalMasterUser.DTOInternalMasterUserBasic>()
				{
					Data = new dto.InternalMasterUser.DTOInternalMasterUserBasic 
					{ 
						Username = "raedshabatat", 
						Office = "RUH", 
						EmailId = "raedshabatat@gmail.com", 
						EmployeeName = "raed hussien alshabatat", 
						MobileNo = "0594647699",
						EmployeeNo = 999014,
						JobTitle = "Development Manager",
						Department = "IT",
						WorkPhoneNo = "9823",
						CreatedOn = DateTime.Now,
						ModifiedOn = DateTime.Now,
						CreatedBy = "Active Directory",
						ModifiedBy = "Active Directory"
					}
				};
			
			
		}
	}
}
