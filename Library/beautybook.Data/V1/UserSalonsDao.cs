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
    public class UserSalonsDao : AbstractUserSalonsDao
    {
        public override PagedList<AbstractUserSalons> UserSalons_gettodayworksheet(PageParam pageParam, string search, long UserId, long SalonId, string AttendanceDate)
        {
            PagedList<AbstractUserSalons> UserSalons = new PagedList<AbstractUserSalons>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EmployeesId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@AttendanceDate", AttendanceDate, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserSalons_gettodayworksheet, param, commandType: CommandType.StoredProcedure);
                UserSalons.Values.AddRange(task.Read<UserSalons>());
                UserSalons.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserSalons;
        }
    }
}
