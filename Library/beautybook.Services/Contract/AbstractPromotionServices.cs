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
    public abstract class AbstractPromotionServices
    {
        public abstract SuccessResult<AbstractPromotion> Promotion_Upsert(AbstractPromotion abstractPromotion);
        public abstract SuccessResult<AbstractPromotion> Promotion_ById(int Id);
        public abstract PagedList<AbstractPromotion> Promotion_All(PageParam pageParam, string search, int VendorId, int ProductId, int ProductTypeId, int ProductBrandId);
    }
}
