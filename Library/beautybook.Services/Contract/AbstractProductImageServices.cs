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
    public abstract class AbstractProductImageServices
    {
        public abstract SuccessResult<AbstractProductImage> ProductImage_Upsert(AbstractProductImage abstractProductImage);
    }
}
