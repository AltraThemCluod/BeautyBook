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
    public class SelectPlan_SMSPackagesDao : AbstractSelectPlan_SMSPackagesDao
    {

        public override SuccessResult<AbstractSelectPlan_SMSPackages>SelectPlan_SMSPackages_Upsert(AbstractSelectPlan_SMSPackages abstractSelectPlan_SMSPackages)
        {
            SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlan_SMSPackages = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractSelectPlan_SMSPackages.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractSelectPlan_SMSPackages.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SMSPackagesId", abstractSelectPlan_SMSPackages.SMSPackagesId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractSelectPlan_SMSPackages.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SelectPlan_SMSPackages_Upsert, param, commandType: CommandType.StoredProcedure);
                SelectPlan_SMSPackages = task.Read<SuccessResult<AbstractSelectPlan_SMSPackages>>().SingleOrDefault();
                SelectPlan_SMSPackages.Item = task.Read<SelectPlan_SMSPackages>().SingleOrDefault();
            }

            return SelectPlan_SMSPackages;
        }

        public override SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlanSMSPackages_UpdatePaymentStatus(long SelectPlanSMSPackagesId)
        {
            SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlan_SMSPackages = null;
            var param = new DynamicParameters();

            param.Add("@SelectPlanSMSPackagesId", SelectPlanSMSPackagesId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SelectPlanSMSPackages_UpdatePaymentStatus, param, commandType: CommandType.StoredProcedure);
                SelectPlan_SMSPackages = task.Read<SuccessResult<AbstractSelectPlan_SMSPackages>>().SingleOrDefault();
                SelectPlan_SMSPackages.Item = task.Read<SelectPlan_SMSPackages>().SingleOrDefault();
            }

            return SelectPlan_SMSPackages;
        }

        public override SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlan_CompleteNoOfMsg(long Number,long SalonId,long IsSms)
        {
            SuccessResult<AbstractSelectPlan_SMSPackages> SelectPlan_SMSPackages = null;
            var param = new DynamicParameters();

            param.Add("@Number", Number, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@IsSms", IsSms, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SelectPlan_CompleteNoOfMsg, param, commandType: CommandType.StoredProcedure);
                SelectPlan_SMSPackages = task.Read<SuccessResult<AbstractSelectPlan_SMSPackages>>().SingleOrDefault();
                SelectPlan_SMSPackages.Item = task.Read<SelectPlan_SMSPackages>().SingleOrDefault();
            }

            return SelectPlan_SMSPackages;
        }



        public override PagedList<AbstractSelectPlan_SMSPackages> SelectPlan_SMSPackages_BySalonId(PageParam pageParam, string search, long SalonId)
        {
            PagedList<AbstractSelectPlan_SMSPackages> SelectPlan_SMSPackages = new PagedList<AbstractSelectPlan_SMSPackages>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SelectPlan_SMSPackages_BySalonId, param, commandType: CommandType.StoredProcedure);
                SelectPlan_SMSPackages.Values.AddRange(task.Read<SelectPlan_SMSPackages>());
                SelectPlan_SMSPackages.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SelectPlan_SMSPackages;
        }

    }
}
