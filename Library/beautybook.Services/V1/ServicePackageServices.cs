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
    public class ServicePackageServices : AbstractServicePackageServices
    {
        private AbstractServicePackageDao abstractServicePackageDao;

        public ServicePackageServices(AbstractServicePackageDao abstractServicePackageDao)
        {
            this.abstractServicePackageDao = abstractServicePackageDao;
        }

        public override PagedList<AbstractServicePackage> ServicePackage_All(PageParam pageParam, string search, long SalonId)
        {
            return this.abstractServicePackageDao.ServicePackage_All(pageParam, search, SalonId);
        }

        public override SuccessResult<AbstractServicePackage> ServicePackage_ActInAct(long Id)
        {
            return this.abstractServicePackageDao.ServicePackage_ActInAct(Id);
        }
        public override SuccessResult<AbstractServicePackage> ServicePackage_ById(long Id)
        {
            return this.abstractServicePackageDao.ServicePackage_ById(Id);
        }

        
        public override SuccessResult<AbstractServicePackage> ServicePackage_Upsert(AbstractServicePackage abstractServicePackage)
        {
            return this.abstractServicePackageDao.ServicePackage_Upsert(abstractServicePackage); ;
        }

    }

}