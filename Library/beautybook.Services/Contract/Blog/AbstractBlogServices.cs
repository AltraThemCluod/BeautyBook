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
    public abstract class AbstractBlogServices
    {
        public abstract SuccessResult<AbstractBlog> Blog_ActInAct(int Id);
        public abstract PagedList<AbstractBlog> Blog_All(PageParam pageParam, string search, int CreatedUserId,int CategoryId);
        public abstract PagedList<AbstractBlog> BlogTopArticles_All();
        public abstract PagedList<AbstractBlog> BlogTopPost_All();
        public abstract SuccessResult<AbstractBlog> Blog_ById(int Id);
        public abstract SuccessResult<AbstractBlog> SocialShare_Insert(int BlogId,int Type);
        public abstract SuccessResult<AbstractBlog> BlogLike_Insert(int BlogId,bool IsLike);
        public abstract SuccessResult<AbstractBlog> BlogSharedCount();
        public abstract SuccessResult<AbstractBlog> Blog_Delete(int Id);
        public abstract SuccessResult<AbstractBlog> Blog_Upsert(AbstractBlog abstractBlog);

        ///// Blog images ///////////////////////////////////

        public abstract PagedList<AbstractBlogImages> BlogImages_All(PageParam pageParam, string search, int UserId);
        public abstract SuccessResult<AbstractBlogImages> BlogImages_Delete(int Id);
        public abstract SuccessResult<AbstractBlogImages> BlogImages_Upsert(AbstractBlogImages abstractBlogImages);
    }
}
