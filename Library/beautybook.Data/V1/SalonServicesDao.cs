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
    public class SalonServicesDao : AbstractSalonServicesDao
    {
        public override SuccessResult<AbstractSalonServices> SalonServices_ById(long Id)
        {
            SuccessResult<AbstractSalonServices> SalonServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonServices_ById, param, commandType: CommandType.StoredProcedure);
                SalonServices = task.Read<SuccessResult<AbstractSalonServices>>().SingleOrDefault();
                SalonServices.Item = task.Read<SalonServices>().SingleOrDefault();

                if (SalonServices.Item != null)
                {
                    SalonServices.Item.UserServices = new List<UserServices>();
                    SalonServices.Item.UserServices.AddRange(task.Read<UserServices>());
                }

            }

            return SalonServices;
        }
        public override SuccessResult<AbstractSalonServices> SalonServices_ActInact(long Id,long LookUpStatusId, long SalonId, long LookUpStatusChangedBy)
        {
            SuccessResult<AbstractSalonServices> SalonServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusChangedBy", LookUpStatusChangedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonServices_ActInact, param, commandType: CommandType.StoredProcedure);
                SalonServices = task.Read<SuccessResult<AbstractSalonServices>>().SingleOrDefault();
                SalonServices.Item = task.Read<SalonServices>().SingleOrDefault();
            }

            return SalonServices;
        }
        public override PagedList<AbstractSalonServices> SalonServices_All(PageParam pageParam, string search, long SalonsId,long LookUpServicesId,long LookUpStatusId , long LookUpCategoryId)
        {
            PagedList<AbstractSalonServices> SalonServices = new PagedList<AbstractSalonServices>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonsId", SalonsId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpServicesId", LookUpServicesId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpCategoryId", LookUpCategoryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonServices_All, param, commandType: CommandType.StoredProcedure);
                SalonServices.Values.AddRange(task.Read<SalonServices>());
                SalonServices.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SalonServices;
        }
        public override SuccessResult<AbstractSalonServices> SalonServices_Upsert(AbstractSalonServices abstractSalonServices)
        {
            SuccessResult<AbstractSalonServices> SalonServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractSalonServices.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpServicesId", abstractSalonServices.LookUpServicesId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpCategoryId", abstractSalonServices.LookUpCategoryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractSalonServices.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Description", abstractSalonServices.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractSalonServices.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractSalonServices.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserServiceId", abstractSalonServices.UserServiceId, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Duration", abstractSalonServices.Duration, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Price", abstractSalonServices.Price, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonServices_Upsert, param, commandType: CommandType.StoredProcedure);
                SalonServices = task.Read<SuccessResult<AbstractSalonServices>>().SingleOrDefault();
                SalonServices.Item = task.Read<SalonServices>().SingleOrDefault();
            }

            return SalonServices;
        }

        public override SuccessResult<AbstractSalonServices> BlankSalonServices_Upsert(AbstractSalonServices abstractSalonServices)
        {
            SuccessResult<AbstractSalonServices> SalonServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractSalonServices.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractSalonServices.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractSalonServices.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlankSalonServices_Upsert, param, commandType: CommandType.StoredProcedure);
                SalonServices = task.Read<SuccessResult<AbstractSalonServices>>().SingleOrDefault();
                SalonServices.Item = task.Read<SalonServices>().SingleOrDefault();
            }

            return SalonServices;
        }

        public override SuccessResult<AbstractSalonServices> SalonServices_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractSalonServices> SalonServices = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonServices_Delete, param, commandType: CommandType.StoredProcedure);
                SalonServices = task.Read<SuccessResult<AbstractSalonServices>>().SingleOrDefault();
                SalonServices.Item = task.Read<SalonServices>().SingleOrDefault();
            }
            return SalonServices;
        }
    }
}
