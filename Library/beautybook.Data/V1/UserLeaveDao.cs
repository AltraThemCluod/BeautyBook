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
    public class UserLeaveDao : AbstractUserLeaveDao
    {
        public override SuccessResult<AbstractUserLeave> UserLeave_ById(long Id)
        {
            SuccessResult<AbstractUserLeave> UserLeave = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserLeave_ById, param, commandType: CommandType.StoredProcedure);
                UserLeave = task.Read<SuccessResult<AbstractUserLeave>>().SingleOrDefault();
                UserLeave.Item = task.Read<UserLeave>().SingleOrDefault();
            }

            return UserLeave;
        }
        public override SuccessResult<AbstractUserLeave> UserLeave_ChangeStatus(long Id, long LookUpStatusId,long LookUpStatusChangedBy)
        {
            SuccessResult<AbstractUserLeave> UserLeave = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusChangedBy", LookUpStatusChangedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserLeave_ChangeStatus, param, commandType: CommandType.StoredProcedure);
                UserLeave = task.Read<SuccessResult<AbstractUserLeave>>().SingleOrDefault();
                UserLeave.Item = task.Read<UserLeave>().SingleOrDefault();
            }

            return UserLeave;
        }
        public override PagedList<AbstractUserLeave> UserLeave_All(PageParam pageParam, string search, AbstractUserLeave abstractUserLeave)
        {
            PagedList<AbstractUserLeave> UserLeave = new PagedList<AbstractUserLeave>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EmployeesId", abstractUserLeave.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractUserLeave.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpLeaveTypeId", abstractUserLeave.LookUpLeaveTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FromDate", abstractUserLeave.FromDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ToDate", abstractUserLeave.ToDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", abstractUserLeave.LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpEmployeeRolesId", abstractUserLeave.LookUpEmployeeRolesId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserLeave_All, param, commandType: CommandType.StoredProcedure);
                UserLeave.Values.AddRange(task.Read<UserLeave>());
                UserLeave.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserLeave;
        }

        public override SuccessResult<AbstractUserLeave> UserLeave_Upsert(AbstractUserLeave abstractUserLeave)
        {
            SuccessResult<AbstractUserLeave> UserLeave = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractUserLeave.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractUserLeave.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractUserLeave.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpLeaveTypeId", abstractUserLeave.LookUpLeaveTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FromDate", abstractUserLeave.FromDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ToDate", abstractUserLeave.ToDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@NoOfDays", abstractUserLeave.NoOfDays, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Reason", abstractUserLeave.Reason, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractUserLeave.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractUserLeave.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserLeave_Upsert, param, commandType: CommandType.StoredProcedure);
                UserLeave = task.Read<SuccessResult<AbstractUserLeave>>().SingleOrDefault();
                UserLeave.Item = task.Read<UserLeave>().SingleOrDefault();
            }
            return UserLeave;
        }
        public override SuccessResult<AbstractUserLeave> UserLeave_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractUserLeave> UserLeave = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserLeave_Delete, param, commandType: CommandType.StoredProcedure);
                UserLeave = task.Read<SuccessResult<AbstractUserLeave>>().SingleOrDefault();
                UserLeave.Item = task.Read<UserLeave>().SingleOrDefault();
            }
            return UserLeave;
        }

        public override PagedList<AbstractUserLeave> UserLeave_LeaveTypeCount(PageParam pageParam, AbstractUserLeave abstractUserLeave)
        {
            PagedList<AbstractUserLeave> UserLeave = new PagedList<AbstractUserLeave>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@EmployeesId", abstractUserLeave.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractUserLeave.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserLeave_LeaveTypeCount, param, commandType: CommandType.StoredProcedure);
                UserLeave.Values.AddRange(task.Read<UserLeave>());
                UserLeave.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserLeave;
        }
    }
}
