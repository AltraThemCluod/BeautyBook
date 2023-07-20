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
    public class BlogServices : AbstractBlogServices
    {
        private AbstractBlogDao abstractBlogDao;

        public BlogServices(AbstractBlogDao abstractBlogDao)
        {
            this.abstractBlogDao = abstractBlogDao;
        }

        public override PagedList<AbstractBlog> Blog_All(PageParam pageParam, string search,int CreatedUserId, int CategoryId)
        {
            return this.abstractBlogDao.Blog_All(pageParam, search, CreatedUserId, CategoryId);
        }

        public override PagedList<AbstractBlog> BlogTopArticles_All()
        {
            return this.abstractBlogDao.BlogTopArticles_All();
        }

        public override PagedList<AbstractBlog> BlogTopPost_All()
        {
            return this.abstractBlogDao.BlogTopPost_All();
        }

        public override SuccessResult<AbstractBlog> Blog_ActInAct(int Id)
        {
            return this.abstractBlogDao.Blog_ActInAct(Id);
        }
        public override SuccessResult<AbstractBlog> Blog_ById(int Id)
        {
            return this.abstractBlogDao.Blog_ById(Id);
        }
        public override SuccessResult<AbstractBlog> SocialShare_Insert(int BlogId,int Type)
        {
            return this.abstractBlogDao.SocialShare_Insert(BlogId,Type);
        }

        public override SuccessResult<AbstractBlog> BlogLike_Insert(int BlogId, bool IsLike)
        {
            return this.abstractBlogDao.BlogLike_Insert(BlogId, IsLike);
        }

        public override SuccessResult<AbstractBlog> BlogSharedCount()
        {
            return this.abstractBlogDao.BlogSharedCount();
        }
        public override SuccessResult<AbstractBlog> Blog_Delete(int Id)
        {
            return this.abstractBlogDao.Blog_Delete(Id);
        }
        public override SuccessResult<AbstractBlog> Blog_Upsert(AbstractBlog abstractBlog)
        {
            return this.abstractBlogDao.Blog_Upsert(abstractBlog); ;
        }

        ///// Blog images ///////////////////////////////////
        
        public override PagedList<AbstractBlogImages> BlogImages_All(PageParam pageParam, string search, int UserId)
        {
            return this.abstractBlogDao.BlogImages_All(pageParam, search, UserId);
        }

        public override SuccessResult<AbstractBlogImages> BlogImages_Delete(int Id)
        {
            return this.abstractBlogDao.BlogImages_Delete(Id);
        }
        public override SuccessResult<AbstractBlogImages> BlogImages_Upsert(AbstractBlogImages abstractBlogImages)
        {
            return this.abstractBlogDao.BlogImages_Upsert(abstractBlogImages); ;
        }

    }

}