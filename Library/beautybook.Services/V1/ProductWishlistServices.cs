using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;
namespace BeautyBook.Services.V1
{
    public class ProductWishlistServices : AbstractProductWishlistServices
    {
        private AbstractProductWishlistDao abstractProductWishlistDao;

        public ProductWishlistServices(AbstractProductWishlistDao abstractProductWishlistDao)
        {
            this.abstractProductWishlistDao = abstractProductWishlistDao;
        }
        public override PagedList<AbstractProductWishlist> ProductWishlist_All(PageParam pageParam, string search,long SalonId)
        {
            return this.abstractProductWishlistDao.ProductWishlist_All(pageParam, search, SalonId);
        }

        

        public override SuccessResult<AbstractProductWishlist> ProductWishlist_Delete(long Id, long DeletedBy)
        {
            return this.abstractProductWishlistDao.ProductWishlist_Delete(Id, DeletedBy);
        }
        public override SuccessResult<AbstractProductWishlist> ProductWishlist_Upsert(AbstractProductWishlist abstractProductWishlist)
        {
            return this.abstractProductWishlistDao.ProductWishlist_Upsert(abstractProductWishlist);
        }

    }
}
