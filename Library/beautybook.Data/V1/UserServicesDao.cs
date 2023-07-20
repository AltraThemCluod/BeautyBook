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
    public class UserServicesDao : AbstractUserServicesDao
    {
        public override PagedList<AbstractUserServices> UserServices_SalonServicesById(PageParam pageParam, string search,long SalonServicesById)
        {
            PagedList<AbstractUserServices> UserServices = new PagedList<AbstractUserServices>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonServicesById", SalonServicesById, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserServices_SalonServicesById, param, commandType: CommandType.StoredProcedure);
                UserServices.Values.AddRange(task.Read<UserServices>());
                UserServices.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return UserServices;
        }
        public override SuccessResult<AbstractUserServices> UserServices_Upsert(AbstractUserServices abstractUserServices)
        {
            SuccessResult<AbstractUserServices> UserServices = null;
            var param = new DynamicParameters();

            param.Add("@UserId", abstractUserServices.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonServiceId", abstractUserServices.SalonServiceId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Duration", abstractUserServices.Duration, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Price", abstractUserServices.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractUserServices.CreatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserServices_Upsert, param, commandType: CommandType.StoredProcedure);
                UserServices = task.Read<SuccessResult<AbstractUserServices>>().SingleOrDefault();
                UserServices.Item = task.Read<UserServices>().SingleOrDefault();
            }

            return UserServices;
        }
        public override SuccessResult<AbstractUserServices> UserServices_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractUserServices> UserServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserServices_Delete, param, commandType: CommandType.StoredProcedure);
                UserServices = task.Read<SuccessResult<AbstractUserServices>>().SingleOrDefault();
                UserServices.Item = task.Read<UserServices>().SingleOrDefault();
            }
            return UserServices;
        }
    }
}
