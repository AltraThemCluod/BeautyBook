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
    public class BlogUsersServices : AbstractBlogUsersServices
    {
        private AbstractBlogUsersDao abstractBlogUsersDao;

        public BlogUsersServices(AbstractBlogUsersDao abstractBlogUsersDao)
        {
            this.abstractBlogUsersDao = abstractBlogUsersDao;
        }

        public override PagedList<AbstractBlogUsers> BlogUsers_All(PageParam pageParam, string search)
        {
            return this.abstractBlogUsersDao.BlogUsers_All(pageParam, search);
        }
        public override SuccessResult<AbstractBlogUsers> BlogUsers_ActInAct(int Id)
        {
            return this.abstractBlogUsersDao.BlogUsers_ActInAct(Id);
        }
        public override SuccessResult<AbstractBlogUsers> BlogUsers_ById(int Id)
        {
            return this.abstractBlogUsersDao.BlogUsers_ById(Id);
        }
        public override SuccessResult<AbstractBlogUsers> BlogUser_Login(AuthLogin authLogin)
        {
            return this.abstractBlogUsersDao.BlogUser_Login(authLogin);
        }
        public override SuccessResult<AbstractBlogUsers> BlogUsers_ChangePassword(BlogUsersChangePassword blogUsersChangePassword)
        {
            return this.abstractBlogUsersDao.BlogUsers_ChangePassword(blogUsersChangePassword);
        }
        public override SuccessResult<AbstractBlogUsers> BlogUsers_Delete(int Id)
        {
            return this.abstractBlogUsersDao.BlogUsers_Delete(Id);
        }

        public override SuccessResult<AbstractBlogUsers> BlogUsers_Upsert(AbstractBlogUsers abstractBlogUsers)
        {
            SuccessResult<AbstractBlogUsers> response = this.abstractBlogUsersDao.BlogUsers_Upsert(abstractBlogUsers); ;
            if (response.Code == 200 && abstractBlogUsers.Id == 0)
            {
                if (abstractBlogUsers.Email != null && abstractBlogUsers.Email != "")
                {
                    EmailHelper.CreateBlogUsers(abstractBlogUsers.FirstName + ' ' + abstractBlogUsers.LastName, abstractBlogUsers.Password, abstractBlogUsers.Email);
                }
            }
            return response;
        }
    }

}