using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Data.V1;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.Contract;

namespace BeautyBook.Services.V1
{
    public class UsersServices_Token
    {
        private UsersDao_Token usersDao_Token = new UsersDao_Token();

        public async Task<SuccessResult<AbstractUsers>> Users_Login(string Email, string PasswordHash, string DeviceToken, long LoginType, long LookUpUserTypeId)
        {
            return this.usersDao_Token.Users_Login(Email, PasswordHash, DeviceToken, LoginType, LookUpUserTypeId);
        }
        
    }

}