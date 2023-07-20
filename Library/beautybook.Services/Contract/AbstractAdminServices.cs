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
    public abstract class AbstractAdminServices
    {
        public abstract SuccessResult<AbstractAdmin> Admin_ActInact(int Id);
        public abstract PagedList<AbstractAdmin> Admin_All(PageParam pageParam, string search);
        public abstract SuccessResult<AbstractAdmin> Admin_ById(long Id);
        public abstract SuccessResult<AbstractAdmin> Admin_ChangePassword(long Id, string OldPassword, string NewPassword, string ConfirmPassword);
        public abstract SuccessResult<AbstractAdmin> Admin_Login(string Username, string Password);
        public abstract bool Admin_Logout(long Id);
        public abstract SuccessResult<AbstractAdmin> Admin_Upsert(AbstractAdmin abstractAdmin);
    }
}
