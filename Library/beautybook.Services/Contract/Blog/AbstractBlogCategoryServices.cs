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
    public abstract class AbstractBlogCategoryServices
    {
        public abstract SuccessResult<AbstractBlogCategory> BlogCategory_ActInAct(int Id);
        public abstract PagedList<AbstractBlogCategory> BlogCategory_All(PageParam pageParam, string search);
        public abstract SuccessResult<AbstractBlogCategory> BlogCategory_ById(int Id);
        public abstract SuccessResult<AbstractBlogCategory> BlogCategory_Delete(int Id);
        public abstract SuccessResult<AbstractBlogCategory> BlogCategory_Upsert(AbstractBlogCategory abstractBlogCategory);
    }
}
