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
    public class ProductImageServices : AbstractProductImageServices
    {
        private AbstractProductImageDao abstractProductImageDao = null;

        public ProductImageServices(AbstractProductImageDao abstractProductImageDao)
        {
            this.abstractProductImageDao = abstractProductImageDao;
        }
        public override SuccessResult<AbstractProductImage> ProductImage_Upsert(AbstractProductImage abstractProductImage)
        {
            return abstractProductImageDao.ProductImage_Upsert(abstractProductImage);
        }
    }
}
