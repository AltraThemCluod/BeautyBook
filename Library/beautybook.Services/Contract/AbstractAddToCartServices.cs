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
    public abstract class AbstractAddToCartServices
    {
        public abstract SuccessResult<AbstractAddToCart> AddToCart_Upsert(AbstractAddToCart abstractAddToCart);
        public abstract SuccessResult<AbstractAddToCart> AddToCart_UpdateQty(string ProductIds, string Qtys);
        public abstract PagedList<AbstractAddToCart> AddToCart_All(PageParam pageParam, string search, long SalonId);
        public abstract SuccessResult<AbstractAddToCart> AddToCart_Delete(long Id, long DeletedBy);


    }
}
