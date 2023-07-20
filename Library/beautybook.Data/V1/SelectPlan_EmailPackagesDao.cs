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
    public class SelectPlan_EmailPackagesDao : AbstractSelectPlan_EmailPackagesDao
    {

        public override SuccessResult<AbstractSelectPlan_EmailPackages> SelectPlan_EmailPackages_Upsert(AbstractSelectPlan_EmailPackages abstractSelectPlan_EmailPackages)
        {
            SuccessResult<AbstractSelectPlan_EmailPackages> SelectPlan_EmailPackages = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractSelectPlan_EmailPackages.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractSelectPlan_EmailPackages.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@EmailPackagesId", abstractSelectPlan_EmailPackages.EmailPackagesId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractSelectPlan_EmailPackages.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SelectPlan_EmailPackages_Upsert, param, commandType: CommandType.StoredProcedure);
                SelectPlan_EmailPackages = task.Read<SuccessResult<AbstractSelectPlan_EmailPackages>>().SingleOrDefault();
                SelectPlan_EmailPackages.Item = task.Read<SelectPlan_EmailPackages>().SingleOrDefault();
            }

            return SelectPlan_EmailPackages;
        }

        public override SuccessResult<AbstractSelectPlan_EmailPackages> SelectPlanEmailPackages_UpdatePaymentStatus(long SelectPlanEmailPackagesId)
        {
            SuccessResult<AbstractSelectPlan_EmailPackages> SelectPlan_EmailPackages = null;
            var param = new DynamicParameters();

            param.Add("@SelectPlanEmailPackagesId", SelectPlanEmailPackagesId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SelectPlanEmailPackages_UpdatePaymentStatus, param, commandType: CommandType.StoredProcedure);
                SelectPlan_EmailPackages = task.Read<SuccessResult<AbstractSelectPlan_EmailPackages>>().SingleOrDefault();
                SelectPlan_EmailPackages.Item = task.Read<SelectPlan_EmailPackages>().SingleOrDefault();
            }
            return SelectPlan_EmailPackages;
        }

        public override PagedList<AbstractSelectPlan_EmailPackages> SelectPlan_EmailPackages_BySalonId(PageParam pageParam, string search, long SalonId)
        {
            PagedList<AbstractSelectPlan_EmailPackages> SelectPlan_EmailPackages = new PagedList<AbstractSelectPlan_EmailPackages>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SelectPlan_EmailPackages_BySalonId, param, commandType: CommandType.StoredProcedure);
                SelectPlan_EmailPackages.Values.AddRange(task.Read<SelectPlan_EmailPackages>());
                SelectPlan_EmailPackages.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SelectPlan_EmailPackages;
        }
        
    }
}
