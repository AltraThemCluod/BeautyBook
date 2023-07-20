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
    public class AdminServices : AbstractAdminServices
    {
        private AbstractAdminDao abstractAdminDao;

        public AdminServices(AbstractAdminDao abstractAdminDao)
        {
            this.abstractAdminDao = abstractAdminDao;
        }

        public override PagedList<AbstractAdmin> Admin_All(PageParam pageParam, string search)
        {
            return this.abstractAdminDao.Admin_All(pageParam, search);
        }

        public override SuccessResult<AbstractAdmin> Admin_ActInact(int Id)
        {
            return this.abstractAdminDao.Admin_ActInact(Id);
        }
        public override SuccessResult<AbstractAdmin> Admin_ById(long Id)
        {
            return this.abstractAdminDao.Admin_ById(Id);
        }

        public override SuccessResult<AbstractAdmin> Admin_Login(string Username, string Password)
        {
            return this.abstractAdminDao.Admin_Login(Username, Password);
        }

        public override bool Admin_Logout(long Id)
        {
            return this.abstractAdminDao.Admin_Logout(Id);
        }

        public override SuccessResult<AbstractAdmin> Admin_ChangePassword(long Id, string OldPassword, string NewPassword, string ConfirmPassword)
        {
            return this.abstractAdminDao.Admin_ChangePassword(Id, OldPassword, NewPassword, ConfirmPassword);
        }

        public override SuccessResult<AbstractAdmin> Admin_Upsert(AbstractAdmin abstractAdmin)
        {
            return this.abstractAdminDao.Admin_Upsert(abstractAdmin); ;
        }
        
    }

}