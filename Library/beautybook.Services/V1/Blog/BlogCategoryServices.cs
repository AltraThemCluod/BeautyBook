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
    public class BlogCategoryServices : AbstractBlogCategoryServices
    {
        private AbstractBlogCategoryDao abstractBlogCategoryDao;

        public BlogCategoryServices(AbstractBlogCategoryDao abstractBlogCategoryDao)
        {
            this.abstractBlogCategoryDao = abstractBlogCategoryDao;
        }

        public override PagedList<AbstractBlogCategory> BlogCategory_All(PageParam pageParam, string search)
        {
            return this.abstractBlogCategoryDao.BlogCategory_All(pageParam, search);
        }

        public override SuccessResult<AbstractBlogCategory> BlogCategory_ActInAct(int Id)
        {
            return this.abstractBlogCategoryDao.BlogCategory_ActInAct(Id);
        }
        public override SuccessResult<AbstractBlogCategory> BlogCategory_ById(int Id)
        {
            return this.abstractBlogCategoryDao.BlogCategory_ById(Id);
        }
        public override SuccessResult<AbstractBlogCategory> BlogCategory_Delete(int Id)
        {
            return this.abstractBlogCategoryDao.BlogCategory_Delete(Id);
        }
        public override SuccessResult<AbstractBlogCategory> BlogCategory_Upsert(AbstractBlogCategory abstractBlogCategory)
        {
            return this.abstractBlogCategoryDao.BlogCategory_Upsert(abstractBlogCategory); ;
        }
    }

}