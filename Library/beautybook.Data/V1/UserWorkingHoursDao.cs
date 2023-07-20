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
    public class UserWorkingHoursDao : AbstractUserWorkingHoursDao
    {
        public override PagedList<AbstractUserWorkingHours> UserWorkingHours_ByUserId(PageParam pageParam, string search,long UserId)
        {
            PagedList<AbstractUserWorkingHours> UserWorkingHours = new PagedList<AbstractUserWorkingHours>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserWorkingHours_ByUserId, param, commandType: CommandType.StoredProcedure);
                UserWorkingHours.Values.AddRange(task.Read<UserWorkingHours>());
                UserWorkingHours.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserWorkingHours;
        }

        public override PagedList<AbstractUserWorkingHours> EmployeeWorkingHour_ServicesId(PageParam pageParam, string search, long CategoryId, string ServicesIds, long SalonId)
        {
            PagedList<AbstractUserWorkingHours> UserWorkingHours = new PagedList<AbstractUserWorkingHours>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CategoryId", CategoryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ServicesIds", ServicesIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmployeeWorkingHour_ServicesId, param, commandType: CommandType.StoredProcedure);
                UserWorkingHours.Values.AddRange(task.Read<UserWorkingHours>());
                UserWorkingHours.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserWorkingHours;
        }

    }
}
