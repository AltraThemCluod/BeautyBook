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
    public class LookUpServicesDao : AbstractLookUpServicesDao
    {
        public override SuccessResult<AbstractLookUpServices> LookUpServices_ById(long Id)
        {
            SuccessResult<AbstractLookUpServices> LookUpServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.LookUpServices_ById, param, commandType: CommandType.StoredProcedure);
                LookUpServices = task.Read<SuccessResult<AbstractLookUpServices>>().SingleOrDefault();
                LookUpServices.Item = task.Read<LookUpServices>().SingleOrDefault();
            }

            return LookUpServices;
        }
        public override SuccessResult<AbstractLookUpServices> LookUpServices_ActInact(long Id,long LookUpStatusId,long LookUpStatusChangedBy)
        {
            SuccessResult<AbstractLookUpServices> LookUpServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusChangedBy", LookUpStatusChangedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.LookUpServices_ActInact, param, commandType: CommandType.StoredProcedure);
                LookUpServices = task.Read<SuccessResult<AbstractLookUpServices>>().SingleOrDefault();
                LookUpServices.Item = task.Read<LookUpServices>().SingleOrDefault();
            }

            return LookUpServices;
        }
        public override PagedList<AbstractLookUpServices> LookUpServices_All(PageParam pageParam, string search, long ParentId,long SalonId,long UserId)
        {
            PagedList<AbstractLookUpServices> LookUpServices = new PagedList<AbstractLookUpServices>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ParentId", ParentId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.LookUpServices_All, param, commandType: CommandType.StoredProcedure);
                LookUpServices.Values.AddRange(task.Read<LookUpServices>());
                LookUpServices.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return LookUpServices;
        }

        public override PagedList<AbstractLookUpServices> CustomCategory_All(PageParam pageParam, string search, long ParentId, long SalonId, long UserId)
        {
            PagedList<AbstractLookUpServices> LookUpServices = new PagedList<AbstractLookUpServices>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ParentId", ParentId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CustomCategory_All, param, commandType: CommandType.StoredProcedure);
                LookUpServices.Values.AddRange(task.Read<LookUpServices>());
                LookUpServices.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return LookUpServices;
        }

        public override PagedList<AbstractLookUpServices> CustomServices_All(PageParam pageParam, string search, long SalonId, long UserId)
        {
            PagedList<AbstractLookUpServices> LookUpServices = new PagedList<AbstractLookUpServices>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.CustomServices_All, param, commandType: CommandType.StoredProcedure);
                LookUpServices.Values.AddRange(task.Read<LookUpServices>());
                LookUpServices.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return LookUpServices;
        }

        public override PagedList<AbstractLookUpServices> LookUpServices_ParentId(PageParam pageParam, string search, AbstractLookUpServices abstractLookUpServices)
        {
            PagedList<AbstractLookUpServices> LookUpServices = new PagedList<AbstractLookUpServices>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ParentId", abstractLookUpServices.ParentId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.LookUpServices_ParentId, param, commandType: CommandType.StoredProcedure);
                LookUpServices.Values.AddRange(task.Read<LookUpServices>());
                LookUpServices.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return LookUpServices;
        }
        public override SuccessResult<AbstractLookUpServices> LookUpServices_Upsert(AbstractLookUpServices abstractLookUpServices)
        {
            SuccessResult<AbstractLookUpServices> LookUpServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractLookUpServices.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Name", abstractLookUpServices.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ParentId", abstractLookUpServices.ParentId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@IconUrl", abstractLookUpServices.IconUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Price", abstractLookUpServices.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@Duration", abstractLookUpServices.Duration, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@ServPhotoUrl", abstractLookUpServices.ServPhotoUrl, dbType: DbType.String, direction: ParameterDirection.Input);
			param.Add("@UserId", abstractLookUpServices.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractLookUpServices.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.LookUpServices_Upsert, param, commandType: CommandType.StoredProcedure);
                LookUpServices = task.Read<SuccessResult<AbstractLookUpServices>>().SingleOrDefault();
                LookUpServices.Item = task.Read<LookUpServices>().SingleOrDefault();
            }

            return LookUpServices;
        }
        public override SuccessResult<AbstractLookUpServices> LookUpServices_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractLookUpServices> LookUpServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.LookUpServices_Delete, param, commandType: CommandType.StoredProcedure);
                LookUpServices = task.Read<SuccessResult<AbstractLookUpServices>>().SingleOrDefault();
                LookUpServices.Item = task.Read<LookUpServices>().SingleOrDefault();
            }
            return LookUpServices;
        }
        public override PagedList<AbstractLookUpServices> LookUpServices_AllServices(long SalonId,long PackageId)
        {
            PagedList<AbstractLookUpServices> LookUpServices = new PagedList<AbstractLookUpServices>();
            var param = new DynamicParameters();

            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@PackageId", PackageId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.LookUpServices_AllServices, param,commandType: CommandType.StoredProcedure);
                LookUpServices.Values.AddRange(task.Read<LookUpServices>());
                LookUpServices.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return LookUpServices;
        }
    }
}
