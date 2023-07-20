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
    public abstract class AbstractProductWishlistDao
    {
        public abstract PagedList<AbstractProductWishlist> ProductWishlist_All(PageParam pageParam, string search,long SalonId);
        public abstract SuccessResult<AbstractProductWishlist> ProductWishlist_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractProductWishlist> ProductWishlist_Upsert(AbstractProductWishlist abstractProductWishlist);

    }
}
