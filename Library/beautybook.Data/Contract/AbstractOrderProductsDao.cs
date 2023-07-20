using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Data.Contract
{
    public abstract class AbstractOrderProductsDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractOrderProducts> OrderProducts_All(PageParam pageParam, string search, long OrderId,long VendorId);
        public abstract PagedList<AbstractOrderProducts> VendorProduct_ByVendorId(PageParam pageParam, string search, long VendorId, long SalonId);

        public abstract bool OrderProducts_Delete(long Id);
        public abstract SuccessResult<AbstractOrderProducts> OrderProducts_Upsert(AbstractOrderProducts abstractOrderProducts);       
        public abstract SuccessResult<AbstractTotalNewOrder> SalonOwnerOrdersCount_VendorId(long VendorId);       
        public abstract SuccessResult<AbstractSalonOwnerOrdersCount> SalonOwnerOrdersCount_UpdateStatus(long OrderId);       
    }
}
