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
    public class AddToCartServices : AbstractAddToCartServices
    {
        private AbstractAddToCartDao abstractAddToCartDao;

        public AddToCartServices(AbstractAddToCartDao abstractAddToCartDao)
        {
            this.abstractAddToCartDao = abstractAddToCartDao;
        }

        public override SuccessResult<AbstractAddToCart> AddToCart_Upsert(AbstractAddToCart abstractAddToCart)
        {
            return this.abstractAddToCartDao.AddToCart_Upsert(abstractAddToCart);
        }


        public override SuccessResult<AbstractAddToCart> AddToCart_UpdateQty(string ProductIds, string Qtys)
        {
            return this.abstractAddToCartDao.AddToCart_UpdateQty(ProductIds, Qtys);
        }


        public override PagedList<AbstractAddToCart> AddToCart_All(PageParam pageParam, string search, long SalonId)
        {
            return this.abstractAddToCartDao.AddToCart_All(pageParam, search, SalonId);

        }
       
        public override SuccessResult<AbstractAddToCart> AddToCart_Delete(long Id, long DeletedBy)
        {
            return this.abstractAddToCartDao.AddToCart_Delete(Id, DeletedBy);
        }

    }
}
