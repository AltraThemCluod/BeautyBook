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
    public class UserServicesServices : AbstractUserServicesServices
    {
        private AbstractUserServicesDao abstractUserServicesDao;

        public UserServicesServices(AbstractUserServicesDao abstractUserServicesDao)
        {
            this.abstractUserServicesDao = abstractUserServicesDao;
        }
        public override PagedList<AbstractUserServices> UserServices_SalonServicesById(PageParam pageParam, string search,long SalonServicesById)
        {
            return this.abstractUserServicesDao.UserServices_SalonServicesById(pageParam, search, SalonServicesById);
        }
        public override SuccessResult<AbstractUserServices> UserServices_Upsert(AbstractUserServices abstractUserServices)
        {
            return this.abstractUserServicesDao.UserServices_Upsert(abstractUserServices); ;
        }
        public override SuccessResult<AbstractUserServices> UserServices_Delete(long Id, long DeletedBy)
        {
            return this.abstractUserServicesDao.UserServices_Delete(Id, DeletedBy);
        }

    }

}