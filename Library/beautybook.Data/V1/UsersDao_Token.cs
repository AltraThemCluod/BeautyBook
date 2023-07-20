using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using Dapper;

namespace BeautyBook.Data.V1
{
    public class UsersDao_Token
    {

        public SuccessResult<AbstractUsers> Users_Login(string Email, string PasswordHash, string DeviceToken, long LoginType , long LookUpUserTypeId)
        {
            SuccessResult<AbstractUsers> Users = null;
            var param = new DynamicParameters();

            param.Add("@Email", Email, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PasswordHash", PasswordHash, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@DeviceToken", DeviceToken, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LoginType", LoginType, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpUserTypeId", LookUpUserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Users_Login, param, commandType: CommandType.StoredProcedure);
                Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
                Users.Item = task.Read<Users>().SingleOrDefault();
            }

            return Users;
        }

    }
}
