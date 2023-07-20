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
    public class ServicePackageDao : AbstractServicePackageDao
    {
        public override SuccessResult<AbstractServicePackage> ServicePackage_ActInAct(long Id)
        {
            SuccessResult<AbstractServicePackage> ServicePackage = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ServicePackage_ActInAct, param, commandType: CommandType.StoredProcedure);
                ServicePackage = task.Read<SuccessResult<AbstractServicePackage>>().SingleOrDefault();
                ServicePackage.Item = task.Read<ServicePackage>().SingleOrDefault();
            }

            return ServicePackage;
        }
        public override PagedList<AbstractServicePackage> ServicePackage_All(PageParam pageParam, string search, long SalonId)
        {
            PagedList<AbstractServicePackage> ServicePackage = new PagedList<AbstractServicePackage>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ServicePackage_All, param, commandType: CommandType.StoredProcedure);
                ServicePackage.Values.AddRange(task.Read<ServicePackage>());
                ServicePackage.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return ServicePackage;
        }
        public override SuccessResult<AbstractServicePackage> ServicePackage_ById(long Id)
        {
            SuccessResult<AbstractServicePackage> ServicePackage = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ServicePackage_ById, param, commandType: CommandType.StoredProcedure);
                ServicePackage = task.Read<SuccessResult<AbstractServicePackage>>().SingleOrDefault();
                ServicePackage.Item = task.Read<ServicePackage>().SingleOrDefault();
                if (ServicePackage.Item != null)
                {
                    ServicePackage.Item.MasterServicePackage = new List<MasterServicePackage>();
                    ServicePackage.Item.MasterServicePackage.AddRange(task.Read<MasterServicePackage>());
                }
            }

            return ServicePackage;
        }
        
        public override SuccessResult<AbstractServicePackage> ServicePackage_Upsert(AbstractServicePackage abstractServicePackage)
        {
            SuccessResult<AbstractServicePackage> ServicePackage = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractServicePackage.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractServicePackage.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@PackageName", abstractServicePackage.PackageName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CustomPrice", abstractServicePackage.CustomPrice, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@ServicePackageIdStr", abstractServicePackage.ServicePackageIdStr, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractServicePackage.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractServicePackage.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@IsActive", abstractServicePackage.IsActive, dbType: DbType.Boolean, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ServicePackage_Upsert, param, commandType: CommandType.StoredProcedure);
                ServicePackage = task.Read<SuccessResult<AbstractServicePackage>>().SingleOrDefault();
                ServicePackage.Item = task.Read<ServicePackage>().SingleOrDefault();
            }

            return ServicePackage;
        }

    }
}
