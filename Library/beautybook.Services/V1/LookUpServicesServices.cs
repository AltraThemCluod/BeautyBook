using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;

namespace BeautyBook.Services.V1
{
    public class LookUpServicesServices : AbstractLookUpServicesServices
    {
        private AbstractLookUpServicesDao abstractLookUpServicesDao;

        public LookUpServicesServices(AbstractLookUpServicesDao abstractLookUpServicesDao)
        {
            this.abstractLookUpServicesDao = abstractLookUpServicesDao;
        }
        public override PagedList<AbstractLookUpServices> LookUpServices_All(PageParam pageParam, string search, long ParentId,long SalonId,long UserId)
        {
            return this.abstractLookUpServicesDao.LookUpServices_All(pageParam, search, ParentId, SalonId, UserId);
        }
        public override PagedList<AbstractLookUpServices> CustomCategory_All(PageParam pageParam, string search, long ParentId, long SalonId, long UserId)
        {
            return this.abstractLookUpServicesDao.CustomCategory_All(pageParam, search, ParentId, SalonId, UserId);
        }
        public override PagedList<AbstractLookUpServices> CustomServices_All(PageParam pageParam, string search, long SalonId, long UserId)
        {
            return this.abstractLookUpServicesDao.CustomServices_All(pageParam, search, SalonId, UserId);
        }
        public override PagedList<AbstractLookUpServices> LookUpServices_ParentId(PageParam pageParam, string search, AbstractLookUpServices abstractLookUpServices)
        {
            return this.abstractLookUpServicesDao.LookUpServices_ParentId(pageParam, search, abstractLookUpServices);
        }
        public override SuccessResult<AbstractLookUpServices> LookUpServices_ActInact(long Id,long LookUpStatusId,long LookUpStatusChangedBy)
        {
            return this.abstractLookUpServicesDao.LookUpServices_ActInact(Id, LookUpStatusId, LookUpStatusChangedBy);
        }
        public override SuccessResult<AbstractLookUpServices> LookUpServices_ById(long Id)
        {
            return this.abstractLookUpServicesDao.LookUpServices_ById(Id);
        }
        public override SuccessResult<AbstractLookUpServices> LookUpServices_Upsert(AbstractLookUpServices abstractLookUpServices)
        {
            return this.abstractLookUpServicesDao.LookUpServices_Upsert(abstractLookUpServices);
        }
        public override SuccessResult<AbstractLookUpServices> LookUpServices_Delete(long Id, long DeletedBy)
        {
            return this.abstractLookUpServicesDao.LookUpServices_Delete(Id, DeletedBy);
        }
        public override PagedList<AbstractLookUpServices> LookUpServices_AllServices(long SalonId, long PackageId)
        {
            return this.abstractLookUpServicesDao.LookUpServices_AllServices(SalonId, PackageId);
        }
    }

}