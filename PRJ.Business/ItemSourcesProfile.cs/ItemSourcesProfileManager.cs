using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRJ.Application.DTOs;
using PRJ.Application.Enums;
using PRJ.Domain.Entities;
using System.Reflection;
using bus = PRJ.Business;
using cns = PRJ.GlobalComponents.Const;
using dto = PRJ.Application.DTOs;
using ent = PRJ.Domain.Entities;
using rep = PRJ.Repository;
using wap = PRJ.Application.Warppers;
namespace PRJ.Business.ItemSourceProfile
{
    public class ItemSourceProfileManager
    {
        private readonly rep.IUnitOfWork _manager;
        public readonly IMapper _mapper;

        public ItemSourceProfileManager(rep.IUnitOfWork manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        //public async Task<wap.Response<DTOItemSourceEditor>> SaveTransactionItemSource(DTOItemSourceEditor body)
        //{
        //    try
        //    {
        //        var data = _mapper.Map<ent.ItemSourceProfile>(body);
        //        if (String.IsNullOrEmpty(body.Id))
        //        {

        //            // source

        //            _manager.ItemSourceProfile.Add(data);

        //            //radio
        //            foreach (var item in body.Radionuclides)
        //            {
        //                var radioData = _mapper.Map<ent.ItemSourceRadionulcides>(item);
        //                radioData.ItemSourceProfileId = data.SourceId;

        //                await _manager.ItemSourceRadionulcides.AddAsync(radioData);
        //            }

        //            //Attachment
        //            foreach (var item in body.SourceAttachments)
        //            {
        //                var trnSourceAttachmenData = _mapper.Map<ent.TransactionAttactcment>(item);
        //                trnSourceAttachmenData.FileSourceID = data.SourceId;
        //                await _manager.TrnItemSourcesFiles.AddAsync(trnSourceAttachmenData);
        //            }
        //            foreach (var item in body.ShieldAttachments)
        //            {
        //                var trnShieldAttachmentData = _mapper.Map<ent.TransactionAttactcment>(body);
        //                trnShieldAttachmentData.FileSourceID = data.SourceId;
        //                await _manager.TrnItemSourcesFiles.AddAsync(trnShieldAttachmentData);
        //            }

        //            // Status History
        //            var status = new ItemSourcesStatusHistory();
        //            status.StatusId = (int)SourceStatus.InitiatedByFacility;
        //            status.SourceId = data.SourceId;
        //            status.CreatedOn = DateTime.Now;
        //            status.ModifiedOn = DateTime.Now;
        //            await _manager.ItemSourceStatusHistory.AddAsync(status);



        //        }
        //        else
        //        {

        //        }

        //        return new wap.Response<DTOItemSourceEditor>(body);
        //    }
        //    catch (Exception exp)
        //    {
        //        List<string> _error = new List<string>() { this.GetType().Name + " : " + MethodBase.GetCurrentMethod().Name, exp.Message };

        //        return new wap.Response<DTOItemSourceEditor>(string.Join("=>", _error))
        //        {
        //            Succeeded = false,
        //            Errors = _error
        //        };
        //    }
        //}

    }
}
