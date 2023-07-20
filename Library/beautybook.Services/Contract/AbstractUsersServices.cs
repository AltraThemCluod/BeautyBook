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
    public abstract class AbstractUsersServices
    {
        public abstract SuccessResult<AbstractUsers> Users_ActInact(long Id, long LookUpStatusId, long LookUpStatusChangedBy);
        public abstract PagedList<AbstractUsers> Users_All(PageParam pageParam, string search, AbstractUsers abstractUsers);
        public abstract SuccessResult<AbstractUsers> Users_ById(long Id,string IsLanguage);
        public abstract SuccessResult<AbstractUsers> Employee_IsCreate(long SalonId);
        public abstract SuccessResult<AbstractUsers> Users_Delete(long Id, long DeletedBy);
        public abstract SuccessResult<AbstractUsers> Users_Login(string Email, string PasswordHash, string DeviceToken, long LoginType,long LookUpUserTypeId);
        public abstract bool Users_Logout(long Id);
        public abstract bool Users_MobileLogout(long Id);
        public abstract SuccessResult<AbstractUsers> Users_ResetPassword(long Id, string OldPassword, string NewPassword, string ConfirmPassword, long Type);
        public abstract SuccessResult<AbstractUsers> Users_Upsert(AbstractUsers abstractUsers);
        public abstract SuccessResult<AbstractUsers> BlankUsers_Create(AbstractUsers abstractUsers);
        public abstract SuccessResult<AbstractUsers> Users_VerifyEmail(long Id, string EmailVerificationCode);
        public abstract SuccessResult<AbstractUsers> Users_VerifyCode(long Id);
        public abstract SuccessResult<AbstractUsers> User_ByEmail(string Id,long IsSeller);
    }
}
