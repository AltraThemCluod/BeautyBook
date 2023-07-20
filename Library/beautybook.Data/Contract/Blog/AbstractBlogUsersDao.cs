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
    public abstract class AbstractBlogUsersDao : AbstractBaseDao
    {
        public abstract SuccessResult<AbstractBlogUsers> BlogUsers_ActInAct(int Id);
        public abstract PagedList<AbstractBlogUsers> BlogUsers_All(PageParam pageParam, string search);
        public abstract SuccessResult<AbstractBlogUsers> BlogUsers_ById(int Id);
        public abstract SuccessResult<AbstractBlogUsers> BlogUser_Login(AuthLogin authLogin);
        public abstract SuccessResult<AbstractBlogUsers> BlogUsers_ChangePassword(BlogUsersChangePassword blogUsersChangePassword);
        public abstract SuccessResult<AbstractBlogUsers> BlogUsers_Delete(int Id);
        public abstract SuccessResult<AbstractBlogUsers> BlogUsers_Upsert(AbstractBlogUsers abstractBlogUsers);
    }
}
