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
    public class AdminDao : AbstractAdminDao
    {
        public override SuccessResult<AbstractAdmin> Admin_ActInact(int Id)
        {
            SuccessResult<AbstractAdmin> Admin = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Admin_ActInact, param, commandType: CommandType.StoredProcedure);
                Admin = task.Read<SuccessResult<AbstractAdmin>>().SingleOrDefault();
                Admin.Item = task.Read<Admin>().SingleOrDefault();
            }

            return Admin;
        }
        public override PagedList<AbstractAdmin> Admin_All(PageParam pageParam, string search)
        {
            PagedList<AbstractAdmin> Admin = new PagedList<AbstractAdmin>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Admin_All, param, commandType: CommandType.StoredProcedure);
                Admin.Values.AddRange(task.Read<Admin>());
                Admin.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Admin;
        }
        public override SuccessResult<AbstractAdmin> Admin_ById(long Id)
        {
            SuccessResult<AbstractAdmin> Admin = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Admin_ById, param, commandType: CommandType.StoredProcedure);
                Admin = task.Read<SuccessResult<AbstractAdmin>>().SingleOrDefault();
                Admin.Item = task.Read<Admin>().SingleOrDefault();
            }

            return Admin;
        }       
        public override SuccessResult<AbstractAdmin> Admin_Login(string Username, string Password)
        {
            SuccessResult<AbstractAdmin> Admin = null;
            var param = new DynamicParameters();

            param.Add("@Username", Username, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Password", Password, dbType: DbType.String, direction: ParameterDirection.Input);
          
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Admin_Login, param, commandType: CommandType.StoredProcedure);
                Admin = task.Read<SuccessResult<AbstractAdmin>>().SingleOrDefault();
                Admin.Item = task.Read<Admin>().SingleOrDefault();
            }

            return Admin;
        }
        public override bool Admin_Logout(long Id)
        {
            bool result = false;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.Admin_Logout, param, commandType: CommandType.StoredProcedure);
                result = task.SingleOrDefault<bool>();
            }
            return result;

        }       
        public override SuccessResult<AbstractAdmin> Admin_ChangePassword(long Id, string OldPassword, string NewPassword, string ConfirmPassword)
        {
            SuccessResult<AbstractAdmin> Admin = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@OldPassword", OldPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@NewPassword", NewPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ConfirmPassword", ConfirmPassword, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Admin_ChangePassword, param, commandType: CommandType.StoredProcedure);
                Admin = task.Read<SuccessResult<AbstractAdmin>>().SingleOrDefault();
                Admin.Item = task.Read<Admin>().SingleOrDefault();
            }

            return Admin;
        }
        public override SuccessResult<AbstractAdmin> Admin_Upsert(AbstractAdmin abstractAdmin)
        {
            SuccessResult<AbstractAdmin> Admin = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractAdmin.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FirstName", abstractAdmin.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LastName", abstractAdmin.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Username", abstractAdmin.Username, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Password", abstractAdmin.Password, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpUserType", abstractAdmin.LookUpUserType, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractAdmin.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractAdmin.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Admin_Upsert, param, commandType: CommandType.StoredProcedure);
                Admin = task.Read<SuccessResult<AbstractAdmin>>().SingleOrDefault();
                Admin.Item = task.Read<Admin>().SingleOrDefault();
            }

            return Admin;
        }
               
    }
}
