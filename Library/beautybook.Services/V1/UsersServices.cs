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
    public class UsersServices : AbstractUsersServices
    {
        private AbstractUsersDao abstractUsersDao;
        private AbstractSalonsDao abstractSalonsDao;

        public UsersServices(AbstractUsersDao abstractUsersDao , AbstractSalonsDao abstractSalonsDao)
        {
            this.abstractUsersDao = abstractUsersDao;
            this.abstractSalonsDao = abstractSalonsDao;
        }

        public override PagedList<AbstractUsers> Users_All(PageParam pageParam, string search , AbstractUsers abstractUsers)
        {
            return this.abstractUsersDao.Users_All(pageParam, search, abstractUsers);
        }

        public override SuccessResult<AbstractUsers> Users_ActInact(long Id, long LookUpStatusId, long LookUpStatusChangedBy)
        {
            return this.abstractUsersDao.Users_ActInact(Id, LookUpStatusId, LookUpStatusChangedBy);
        }
        public override SuccessResult<AbstractUsers> Users_ById(long Id,string IsLanguage)
        {
            return this.abstractUsersDao.Users_ById(Id, IsLanguage);
        }

        public override SuccessResult<AbstractUsers> Employee_IsCreate(long SalonId)
        {
            return this.abstractUsersDao.Employee_IsCreate(SalonId);
        }

        public override SuccessResult<AbstractUsers> Users_Login(string Email, string PasswordHash, string DeviceToken, long LoginType , long LookUpUserTypeId)
        {
            SuccessResult<AbstractUsers> response = this.abstractUsersDao.Users_Login(Email, PasswordHash, DeviceToken, LoginType , LookUpUserTypeId);
            if (response.Code == 200 && LoginType == 3)
            {
                var verifiedurl = "/Authentication/ResetPassword?Id=" + ConvertTo.Base64Encode(response.Item.Id.ToString());

                if(LookUpUserTypeId == 2)
                {
                    EmailHelper.SendEmail(response.Item.Email, response.Item.FirstName + ' ' + response.Item.SecondName, "Your reset password link <a href=" + Configurations.SalonOwnerUrl + verifiedurl + ">Click Here</a>");
                }
                else if (LookUpUserTypeId == 5)
                {
                    EmailHelper.SendEmail(response.Item.Email, response.Item.FirstName + ' ' + response.Item.SecondName, "Your reset password link <a href=" + Configurations.VendorAdminUrl + verifiedurl + ">Click Here</a>");
                }

            }
            return response;
        }

        public override bool Users_MobileLogout(long Id)
        {
            return this.abstractUsersDao.Users_MobileLogout(Id);
        }

        public override bool Users_Logout(long Id)
        {
            return this.abstractUsersDao.Users_Logout(Id);
        }

        public override SuccessResult<AbstractUsers> Users_ResetPassword(long Id, string OldPassword, string NewPassword, string ConfirmPassword, long Type)
        {
            return this.abstractUsersDao.Users_ResetPassword(Id, OldPassword, NewPassword, ConfirmPassword, Type);
        }

        public override SuccessResult<AbstractUsers> Users_Upsert(AbstractUsers abstractUsers)
        {

            if (abstractUsers.LookUpUserTypeId == 2 && abstractUsers.Id == 0)
            {
                EmailHelper.CreateSalonOwner(abstractUsers.FirstName, abstractUsers.SecondName, abstractUsers.Email , "New salon registered");
            }
            else if (abstractUsers.LookUpUserTypeId == 5 && abstractUsers.Id == 0)
            {
                EmailHelper.CreateSalonOwner(abstractUsers.FirstName, abstractUsers.SecondName, abstractUsers.Email, "New supplier registered");
            }
            
            var response = this.abstractUsersDao.Users_Upsert(abstractUsers);

            if (response.Code == 200)
            {
                if (response.Item.LookUpUserTypeId == 3 && response.Item.IsEmployeeSendEmail == false)
                {
                    var salonresponse = abstractSalonsDao.Salons_ById(abstractUsers.SalonId);

                    if (response.Item != null && response.IsValid == true)
                    {
                        EmailHelper.CreateEmployee(response.Item.FirstName, response.Item.SecondName, response.Item.Email, response.Item.PasswordHash, salonresponse.Item.SalonName);
                    }
                }
            }

            return response;
        }

        public override SuccessResult<AbstractUsers> BlankUsers_Create(AbstractUsers abstractUsers)
        {
            return this.abstractUsersDao.BlankUsers_Create(abstractUsers);
        }

        public override SuccessResult<AbstractUsers> Users_VerifyEmail(long Id, string EmailVerificationCode)
        {
            return this.abstractUsersDao.Users_VerifyEmail(Id, EmailVerificationCode);
        }

        public override SuccessResult<AbstractUsers> Users_VerifyCode(long Id)
        {
            SuccessResult<AbstractUsers> response = this.abstractUsersDao.Users_VerifyCode(Id);
            if (response.Code == 200)
            {
                EmailHelper.SendEmail(response.Item.Email, response.Item.FirstName + ' ' + response.Item.SecondName, "To continue with your email verification, please enter the following code: <br/> The verification code is: <b>" + response.Item.EmailVerificationCode + "</b>");
            }
            return response;
        }

        public override SuccessResult<AbstractUsers> Users_Delete(long Id, long DeletedBy)
        {
            return this.abstractUsersDao.Users_Delete(Id, DeletedBy);
        }
        public override SuccessResult<AbstractUsers> User_ByEmail(string Email,long IsSeller)
        {
            return this.abstractUsersDao.User_ByEmail(Email, IsSeller);
        }
    }

}