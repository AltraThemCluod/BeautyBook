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
    public class TechnicalSupportServices : AbstractTechnicalSupportServices
    {
        private AbstractTechnicalSupportDao abstractTechnicalSupportDao;

        public TechnicalSupportServices(AbstractTechnicalSupportDao abstractTechnicalSupportDao)
        {
            this.abstractTechnicalSupportDao = abstractTechnicalSupportDao;
        }

        public override SuccessResult<AbstractTechnicalSupport> TechnicalSupport_Upsert(AbstractTechnicalSupport abstractTechnicalSupport)
        {
            return this.abstractTechnicalSupportDao.TechnicalSupport_Upsert(abstractTechnicalSupport);
        }

        public override PagedList<AbstractTechnicalSupport> TechnicalSupport_All(PageParam pageParam, string search, long SalonId,long UserId,long UserTypeId)
        {
            return this.abstractTechnicalSupportDao.TechnicalSupport_All(pageParam, search, SalonId, UserId, UserTypeId);

        }
       
        public override SuccessResult<AbstractTechnicalSupport> TechnicalSupport_Delete(long Id, long DeletedBy)
        {
            return this.abstractTechnicalSupportDao.TechnicalSupport_Delete(Id, DeletedBy);
        }

        public override SuccessResult<AbstractTechnicalSupport> TechnicalSupport_ById(long Id)
        {
            return this.abstractTechnicalSupportDao.TechnicalSupport_ById(Id);
        }

    }
}
