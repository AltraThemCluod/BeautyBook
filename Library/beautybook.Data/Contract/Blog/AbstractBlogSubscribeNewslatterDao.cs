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
    public abstract class AbstractBlogSubscribeNewslatterDao : AbstractBaseDao
    {
        public abstract PagedList<AbstractBlogSubscribeNewslatter> BlogSubscribeNewslatter_All(PageParam pageParam, string search);
        public abstract SuccessResult<AbstractBlogSubscribeNewslatter> BlogSubscribeNewslatter_Insert(AbstractBlogSubscribeNewslatter abstractBlogSubscribeNewslatter);
    }
}
