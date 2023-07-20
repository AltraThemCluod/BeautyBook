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
    public class OfflineVendorServices : AbstractOfflineVendorServices
    {
        private AbstractOfflineVendorDao abstractOfflineVendorDao;

        public OfflineVendorServices(AbstractOfflineVendorDao abstractOfflineVendorDao)
        {
            this.abstractOfflineVendorDao = abstractOfflineVendorDao;
        }

        public override SuccessResult<AbstractOfflineVendor> OfflineVendor_Upsert(AbstractOfflineVendor abstractOfflineVendor)
        {
            return this.abstractOfflineVendorDao.OfflineVendor_Upsert(abstractOfflineVendor);
        }

        public override SuccessResult<AbstractOfflineVendor> OfflineVendor_ById(long Id)
        {
            return this.abstractOfflineVendorDao.OfflineVendor_ById(Id);
        }

        public override PagedList<AbstractOfflineVendor> OfflineVendor_All(PageParam pageParam, string search, long SalonId)
        {
            return this.abstractOfflineVendorDao.OfflineVendor_All(pageParam, search, SalonId);
        }




    }
}
