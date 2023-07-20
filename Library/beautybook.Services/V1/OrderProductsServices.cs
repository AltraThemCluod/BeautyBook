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
    public class OrderProductsServices : AbstractOrderProductsServices
    {
        private AbstractOrderProductsDao abstractOrderProductsDao;

        public OrderProductsServices(AbstractOrderProductsDao abstractOrderProductsDao)
        {
            this.abstractOrderProductsDao = abstractOrderProductsDao;
        }
        public override PagedList<AbstractOrderProducts> OrderProducts_All(PageParam pageParam, string search , long OrderId,long VendorId)
        {
            return this.abstractOrderProductsDao.OrderProducts_All(pageParam, search, OrderId, VendorId);
        }
        public override PagedList<AbstractOrderProducts> VendorProduct_ByVendorId(PageParam pageParam, string search, long VendorId, long SalonId)
        {
            return this.abstractOrderProductsDao.VendorProduct_ByVendorId(pageParam, search, VendorId, SalonId);
        }
        public override bool OrderProducts_Delete(long Id)
        {
            return this.abstractOrderProductsDao.OrderProducts_Delete(Id);
        }
        public override SuccessResult<AbstractOrderProducts> OrderProducts_Upsert(AbstractOrderProducts abstractOrderProducts)
        {
            return this.abstractOrderProductsDao.OrderProducts_Upsert(abstractOrderProducts);
        }

        public override SuccessResult<AbstractTotalNewOrder> SalonOwnerOrdersCount_VendorId(long VendorId)
        {
            return this.abstractOrderProductsDao.SalonOwnerOrdersCount_VendorId(VendorId);
        }

        public override SuccessResult<AbstractSalonOwnerOrdersCount> SalonOwnerOrdersCount_UpdateStatus(long OrderId)
        {
            return this.abstractOrderProductsDao.SalonOwnerOrdersCount_UpdateStatus(OrderId);
        }
    }

}