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
    public class SalonVendorDashboardServices : AbstractSalonVendorDashboardServices
    {
        private AbstractSalonVendorDashboardDao abstractSalonVendorDashboardDao;

        public SalonVendorDashboardServices(AbstractSalonVendorDashboardDao abstractSalonVendorDashboardDao)
        {
            this.abstractSalonVendorDashboardDao = abstractSalonVendorDashboardDao;
        }

        public override SuccessResult<AbstractVendorSalesAndRating> VendorSalesAndRating_VendorId(long VendorId)
        {
            return this.abstractSalonVendorDashboardDao.VendorSalesAndRating_VendorId(VendorId);
        }

        public override PagedList<AbstractVendorTopCustomer> VendorTopCustomer_All(PageParam pageParam, string search, long VendorId, string FromDate, string ToDate,long Type)
        {
            return this.abstractSalonVendorDashboardDao.VendorTopCustomer_All(pageParam, search, VendorId, FromDate, ToDate,Type);
        }

        public override PagedList<AbstractVendorTopProduct> VendorTopProduct_All(PageParam pageParam, string search, long VendorId, string FromDate, string ToDate, long Type)
        {
            return this.abstractSalonVendorDashboardDao.VendorTopProduct_All(pageParam, search, VendorId, FromDate, ToDate, Type);
        }

    }
}