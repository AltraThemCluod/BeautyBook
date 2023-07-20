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
    public class PromotionServices : AbstractPromotionServices
    {

        private AbstractPromotionDao abstractPromotionDao;

        public PromotionServices(AbstractPromotionDao abstractPromotionDao)
        {
            this.abstractPromotionDao = abstractPromotionDao;
        }

        public override PagedList<AbstractPromotion> Promotion_All(PageParam pageParam, string search, int VendorId, int ProductId, int ProductTypeId, int ProductBrandId)
        {
            return this.abstractPromotionDao.Promotion_All(pageParam, search, VendorId, ProductId, ProductTypeId, ProductBrandId);
        }

        public override SuccessResult<AbstractPromotion> Promotion_ById(int Id)
        {
            return this.abstractPromotionDao.Promotion_ById(Id);
        }

        public override SuccessResult<AbstractPromotion> Promotion_Upsert(AbstractPromotion abstractPromotion)
        {
            return this.abstractPromotionDao.Promotion_Upsert(abstractPromotion);
        }
    }
}
