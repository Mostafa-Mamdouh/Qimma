using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rep = PRJ.Repository;
using ent = PRJ.Domain.Entities;
using lg = PRJ.GlobalComponents.Logger;
using wap = PRJ.Application.Warppers;
using dto = PRJ.Application.DTOs;
using Microsoft.Extensions.Logging;
using AutoMapper;


namespace PRJ.Business.ProfileOfUsers
{
	public class ProfileOfUserManager
	{
		private readonly rep.IUnitOfWork _manager;
		public readonly IMapper _mapper;

		public ProfileOfUserManager(rep.IUnitOfWork manager, IMapper mapper)
		{
			this._manager = manager;
			
			_mapper = mapper;
		}

		//public async Task<wap.Response<dto.ProfileOfUser.BasicInfoUsers>> GetItem(dto.GetByPost? gbp)
		//{
		//	if(gbp is not null)
		//	{ }
		//	if (gbp.EncId is not null && gbp.Id is not null)
		//	{
		//		var user = await _manager.ProfileOfUsers.GetEncryptByIdAsync(gbp.EncId);

		//		var dto = _mapper.Map<dto.ProfileOfUser.BasicInfoUsers>(user);

				
		//		lg.LoggerManager.Log("Done");

		//		return new wap.Response<dto.ProfileOfUser.BasicInfoUsers>("Done from Response Warppers")
		//		{
		//			Data = dto
		//		};
		//	}
		//	else
		//	{
		//		List<string> _error = new List<string>();
		//		_error.Add("No Parameters");
		//		return new wap.Response<dto.ProfileOfUser.BasicInfoUsers>()
		//		{
		//			Succeeded = false,
		//			Errors = _error
		//		};
		//	}
		//}

		//public async Task<wap.Response<List<dto.ProfileOfUser.BasicInfoUsers>>> GetAllAsync()
		//{
		//	var users = await _manager.ProfileOfUsers.GetAllAsync();

		//	var dto = _mapper.Map<List<dto.ProfileOfUser.BasicInfoUsers>>(users);

		//	lg.LoggerManager.Log("Done");

		//	return new wap.Response<List<dto.ProfileOfUser.BasicInfoUsers>>("Done from Response Warppers")
		//	{
		//		Data = dto
		//	};
		//}



		//public async Task<wap.Response<int>> CountAsync()
		//{
		//	var users = await _manager.ProfileOfUsers.CountAsync();

		//	lg.LoggerManager.Log("Function Logging:" + users);

		//	return new wap.Response<int>("Response Logging:" + users)
		//	{
		//		Data = users
		//	};
		//}
	}
}
