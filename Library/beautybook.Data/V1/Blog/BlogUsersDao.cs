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
    public class BlogUsersDao : AbstractBlogUsersDao
    {
        public override SuccessResult<AbstractBlogUsers> BlogUsers_ActInAct(int Id)
        {
            SuccessResult<AbstractBlogUsers> BlogUsers = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogUsers_ActInAct, param, commandType: CommandType.StoredProcedure);
                BlogUsers = task.Read<SuccessResult<AbstractBlogUsers>>().SingleOrDefault();
                BlogUsers.Item = task.Read<BlogUsers>().SingleOrDefault();
            }

            return BlogUsers;
        }

        public override PagedList<AbstractBlogUsers> BlogUsers_All(PageParam pageParam, string search)
        {
            PagedList<AbstractBlogUsers> BlogUsers = new PagedList<AbstractBlogUsers>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogUsers_All, param, commandType: CommandType.StoredProcedure);
                BlogUsers.Values.AddRange(task.Read<BlogUsers>());
                BlogUsers.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return BlogUsers;
        }
        public override SuccessResult<AbstractBlogUsers> BlogUsers_ById(int Id)
        {
            SuccessResult<AbstractBlogUsers> BlogUsers = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogUsers_ById, param, commandType: CommandType.StoredProcedure);
                BlogUsers = task.Read<SuccessResult<AbstractBlogUsers>>().SingleOrDefault();
                BlogUsers.Item = task.Read<BlogUsers>().SingleOrDefault();
            }

            return BlogUsers;
        }

        public override SuccessResult<AbstractBlogUsers> BlogUser_Login(AuthLogin authLogin)
        {
            SuccessResult<AbstractBlogUsers> BlogUsers = null;
            var param = new DynamicParameters();

            param.Add("@UserName", authLogin.UserName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Password", authLogin.Password, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogUser_Login, param, commandType: CommandType.StoredProcedure);
                BlogUsers = task.Read<SuccessResult<AbstractBlogUsers>>().SingleOrDefault();
                BlogUsers.Item = task.Read<BlogUsers>().SingleOrDefault();
            }

            return BlogUsers;
        }

        public override SuccessResult<AbstractBlogUsers> BlogUsers_ChangePassword(BlogUsersChangePassword blogUsersChangePassword)
        {
            SuccessResult<AbstractBlogUsers> BlogUsers = null;
            var param = new DynamicParameters();

            param.Add("@Id", blogUsersChangePassword.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@OldPassword", blogUsersChangePassword.OldPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@NewPassword", blogUsersChangePassword.NewPassword, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ConfirmPassword", blogUsersChangePassword.ConfirmPassword, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogUsers_ChangePassword, param, commandType: CommandType.StoredProcedure);
                BlogUsers = task.Read<SuccessResult<AbstractBlogUsers>>().SingleOrDefault();
                BlogUsers.Item = task.Read<BlogUsers>().SingleOrDefault();
            }

            return BlogUsers;
        }

        public override SuccessResult<AbstractBlogUsers> BlogUsers_Delete(int Id)
        {
            SuccessResult<AbstractBlogUsers> BlogUsers = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogUsers_Delete, param, commandType: CommandType.StoredProcedure);
                BlogUsers = task.Read<SuccessResult<AbstractBlogUsers>>().SingleOrDefault();
                BlogUsers.Item = task.Read<BlogUsers>().SingleOrDefault();
            }

            return BlogUsers;
        }

        public override SuccessResult<AbstractBlogUsers> BlogUsers_Upsert(AbstractBlogUsers abstractBlogUsers)
        {
            SuccessResult<AbstractBlogUsers> BlogUsers = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractBlogUsers.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@FirstName", abstractBlogUsers.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LastName", abstractBlogUsers.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Email", abstractBlogUsers.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Password", abstractBlogUsers.Password, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ContactNumber", abstractBlogUsers.ContactNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Gender", abstractBlogUsers.Gender, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ProfileUrl", abstractBlogUsers.ProfileUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UserTypeId", abstractBlogUsers.UserTypeId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogUsers_Upsert, param, commandType: CommandType.StoredProcedure);
                BlogUsers = task.Read<SuccessResult<AbstractBlogUsers>>().SingleOrDefault();
                BlogUsers.Item = task.Read<BlogUsers>().SingleOrDefault();
            }
            return BlogUsers;
        }
               
    }
}
