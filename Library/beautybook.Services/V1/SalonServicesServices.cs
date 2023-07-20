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
    public class SalonServicesServices : AbstractSalonServicesServices
    {
        private AbstractSalonServicesDao abstractSalonServicesDao;

        public SalonServicesServices(AbstractSalonServicesDao abstractSalonServicesDao)
        {
            this.abstractSalonServicesDao = abstractSalonServicesDao;
        }
        public override PagedList<AbstractSalonServices> SalonServices_All(PageParam pageParam, string search, long SalonsId, long LookUpServicesId, long LookUpStatusId , long LookUpCategoryId)
        {
            return this.abstractSalonServicesDao.SalonServices_All(pageParam, search, SalonsId, LookUpServicesId, LookUpStatusId , LookUpCategoryId);
        }
        public override SuccessResult<AbstractSalonServices> SalonServices_ActInact(long Id,long LookUpStatusId, long SalonId,long LookUpStatusChangedBy)
        {
            return this.abstractSalonServicesDao.SalonServices_ActInact(Id,LookUpStatusId, SalonId, LookUpStatusChangedBy);
        }
        public override SuccessResult<AbstractSalonServices> SalonServices_ById(long Id)
        {
            return this.abstractSalonServicesDao.SalonServices_ById(Id);
        }
        public override SuccessResult<AbstractSalonServices> SalonServices_Upsert(AbstractSalonServices abstractSalonServices)
        {
            return this.abstractSalonServicesDao.SalonServices_Upsert(abstractSalonServices);
        }
        public override SuccessResult<AbstractSalonServices> BlankSalonServices_Upsert(AbstractSalonServices abstractSalonServices)
        {
            return this.abstractSalonServicesDao.BlankSalonServices_Upsert(abstractSalonServices);
        }
        public override SuccessResult<AbstractSalonServices> SalonServices_Delete(long Id, long DeletedBy)
        {
            return this.abstractSalonServicesDao.SalonServices_Delete(Id, DeletedBy);
        }
    }

}