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
    public class UserWorkSheetDao : AbstractUserWorkSheetDao
    {
        public override SuccessResult<AbstractUserWorkSheet> UserWorkSheet_ById(long Id)
        {
            SuccessResult<AbstractUserWorkSheet> UserWorkSheet = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserWorkSheet_ById, param, commandType: CommandType.StoredProcedure);
                UserWorkSheet = task.Read<SuccessResult<AbstractUserWorkSheet>>().SingleOrDefault();
                UserWorkSheet.Item = task.Read<UserWorkSheet>().SingleOrDefault();
            }
            return UserWorkSheet;
        }
       
        public override PagedList<AbstractUserWorkSheet> UserWorkSheet_All(PageParam pageParam, string search, AbstractUserWorkSheet abstractUserWorkSheet)
        {
            PagedList<AbstractUserWorkSheet> UserWorkSheet = new PagedList<AbstractUserWorkSheet>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EmployeesId", abstractUserWorkSheet.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractUserWorkSheet.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@AttendanceDate", abstractUserWorkSheet.AttendanceDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", abstractUserWorkSheet.LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpEmployeeRolesId", abstractUserWorkSheet.LookUpEmployeeRolesId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserWorkSheet_All, param, commandType: CommandType.StoredProcedure);
                UserWorkSheet.Values.AddRange(task.Read<UserWorkSheet>());
                UserWorkSheet.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserWorkSheet;
        }

        public override SuccessResult<AbstractUserWorkSheet> UserWorkSheet_Upsert(AbstractUserWorkSheet abstractUserWorkSheet)
        {
            SuccessResult<AbstractUserWorkSheet> UserWorkSheet = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractUserWorkSheet.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractUserWorkSheet.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractUserWorkSheet.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", abstractUserWorkSheet.LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@InTime", abstractUserWorkSheet.InTime, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@OutTime", abstractUserWorkSheet.OutTime, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Break", abstractUserWorkSheet.Break, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ShortLeave", abstractUserWorkSheet.ShortLeave, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractUserWorkSheet.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractUserWorkSheet.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserWorkSheet_Upsert, param, commandType: CommandType.StoredProcedure);
                UserWorkSheet = task.Read<SuccessResult<AbstractUserWorkSheet>>().SingleOrDefault();
                UserWorkSheet.Item = task.Read<UserWorkSheet>().SingleOrDefault();
            }
            return UserWorkSheet;
        }
       
    }
}
