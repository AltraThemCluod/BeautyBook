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
    public class BlogContentServices : AbstractBlogContentServices
    {
        private AbstractBlogContentDao abstractBlogContentDao;

        public BlogContentServices(AbstractBlogContentDao abstractBlogContentDao)
        {
            this.abstractBlogContentDao = abstractBlogContentDao;
        }

        public override SuccessResult<AbstractBlogContent> BlogContent_Upsert(AbstractBlogContent abstractBlogContent)
        {
            return this.abstractBlogContentDao.BlogContent_Upsert(abstractBlogContent); ;
        }
        public override SuccessResult<AbstractBlogContent> BlogContent_Delete(int Id)
        {
            return this.abstractBlogContentDao.BlogContent_Delete(Id);
        }
    }
}