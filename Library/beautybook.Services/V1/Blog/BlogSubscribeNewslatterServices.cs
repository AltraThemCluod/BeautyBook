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
    public class BlogSubscribeNewslatterServices : AbstractBlogSubscribeNewslatterServices
    {
        private AbstractBlogSubscribeNewslatterDao abstractBlogSubscribeNewslatterDao;

        public BlogSubscribeNewslatterServices(AbstractBlogSubscribeNewslatterDao abstractBlogSubscribeNewslatterDao)
        {
            this.abstractBlogSubscribeNewslatterDao = abstractBlogSubscribeNewslatterDao;
        }

        public override PagedList<AbstractBlogSubscribeNewslatter> BlogSubscribeNewslatter_All(PageParam pageParam, string search)
        {
            return this.abstractBlogSubscribeNewslatterDao.BlogSubscribeNewslatter_All(pageParam, search);
        }
        
        public override SuccessResult<AbstractBlogSubscribeNewslatter> BlogSubscribeNewslatter_Insert(AbstractBlogSubscribeNewslatter abstractBlogSubscribeNewslatter)
        {
            return this.abstractBlogSubscribeNewslatterDao.BlogSubscribeNewslatter_Insert(abstractBlogSubscribeNewslatter); ;
        }
    }

}