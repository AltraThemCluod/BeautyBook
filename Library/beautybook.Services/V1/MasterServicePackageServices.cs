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
    public class MasterServicePackageServices : AbstractMasterServicePackageServices
    {
        private AbstractMasterServicePackageDao abstractMasterServicePackageDao;

        public MasterServicePackageServices(AbstractMasterServicePackageDao abstractMasterServicePackageDao)
        {
            this.abstractMasterServicePackageDao = abstractMasterServicePackageDao;
        }

        public override PagedList<AbstractMasterServicePackage> MasterServicePackage_All(PageParam pageParam, string search, long ServicePackageId)
        {
            return this.abstractMasterServicePackageDao.MasterServicePackage_All(pageParam, search, ServicePackageId);
        }
    }

}