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
    public class VendorApprovelServices : AbstractVendorApprovelServices
    {
        private AbstractVendorApprovelDao abstractVendorApprovelDao;

        public VendorApprovelServices(AbstractVendorApprovelDao abstractVendorApprovelDao)
        {
            this.abstractVendorApprovelDao = abstractVendorApprovelDao;
        }

        public override PagedList<AbstractVendorApprovel> VendorApprovel_All(PageParam pageParam, string search , int IsApprove,int LookUpUserTypeId)
        {
            return this.abstractVendorApprovelDao.VendorApprovel_All(pageParam, search , IsApprove, LookUpUserTypeId);
        }

        public override SuccessResult<AbstractVendorApprovel> VendorApprovel_IsApproved(int Id)
        {
            return this.abstractVendorApprovelDao.VendorApprovel_IsApproved(Id);
        }
        
    }

}
