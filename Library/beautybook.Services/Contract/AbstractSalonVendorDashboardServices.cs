using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Services.Contract
{
    public abstract class AbstractSalonVendorDashboardServices
    {
        public abstract SuccessResult<AbstractVendorSalesAndRating> VendorSalesAndRating_VendorId(long VendorId);
        public abstract PagedList<AbstractVendorTopCustomer> VendorTopCustomer_All(PageParam pageParam, string search, long VendorId, string FromDate, string ToDate,long Type);
        public abstract PagedList<AbstractVendorTopProduct> VendorTopProduct_All(PageParam pageParam, string search, long VendorId, string FromDate, string ToDate,long Type);
    }
}
