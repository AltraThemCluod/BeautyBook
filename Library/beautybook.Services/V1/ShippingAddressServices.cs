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
    public class ShippingAddressServices : AbstractShippingAddressServices
    {
        private AbstractShippingAddressDao abstractShippingAddressDao;

        public ShippingAddressServices(AbstractShippingAddressDao abstractShippingAddressDao)
        {
            this.abstractShippingAddressDao = abstractShippingAddressDao;
        }
        public override PagedList<AbstractShippingAddress> ShippingAddress_All(PageParam pageParam, string search,long SalonId)
        {
            return this.abstractShippingAddressDao.ShippingAddress_All(pageParam, search, SalonId);
        }       
        public override SuccessResult<AbstractShippingAddress> ShippingAddress_Upsert(AbstractShippingAddress abstractShippingAddress)
        {
            return this.abstractShippingAddressDao.ShippingAddress_Upsert(abstractShippingAddress);
        }
        public override SuccessResult<AbstractShippingAddress> ShippingAddress_Delete(long Id, long DeletedBy)
        {
            return this.abstractShippingAddressDao.ShippingAddress_Delete(Id, DeletedBy);
        }
        public override SuccessResult<AbstractShippingAddress> ShippingAddress_ById(long Id)
        {
            return this.abstractShippingAddressDao.ShippingAddress_ById(Id);
        }
    }

}