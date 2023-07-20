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
    public abstract class AbstractVendorApprovelDao : AbstractBaseDao
    {
        public abstract SuccessResult<AbstractVendorApprovel> VendorApprovel_IsApproved(int Id);
        public abstract PagedList<AbstractVendorApprovel> VendorApprovel_All(PageParam pageParam, string search,int IsApprove , int LookUpUserTypeId);
        
    }
}
