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
    public class SalonOwnerDashboardDao : AbstractSalonOwnerDashboardDao
    {
        public override SuccessResult<AbstractSalonOwnerSMSandEmailPackages> SalonOwnerSMSandEmailPackages_All(long SalonId)
        {
            SuccessResult<AbstractSalonOwnerSMSandEmailPackages> SalonOwnerSMSandEmailPackages = null;
            var param = new DynamicParameters();

            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOwnerSMSandEmailPackages_All, param, commandType: CommandType.StoredProcedure);
                SalonOwnerSMSandEmailPackages = task.Read<SuccessResult<AbstractSalonOwnerSMSandEmailPackages>>().SingleOrDefault();
                SalonOwnerSMSandEmailPackages.Item = task.Read<SalonOwnerSMSandEmailPackages>().SingleOrDefault();
            }

            return SalonOwnerSMSandEmailPackages;
        }

        public override SuccessResult<AbstractTecnicalSupport> TecnicalSupport_SalonId(long SalonId)
        {
            SuccessResult<AbstractTecnicalSupport> TecnicalSupport = null;
            var param = new DynamicParameters();

            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.TecnicalSupport_SalonId, param, commandType: CommandType.StoredProcedure);
                TecnicalSupport = task.Read<SuccessResult<AbstractTecnicalSupport>>().SingleOrDefault();
                TecnicalSupport.Item = task.Read<TecnicalSupport>().SingleOrDefault();
            }

            return TecnicalSupport;
        }

        public override PagedList<AbstractSalonOwnerNewCustomer> SalonOwnerNewCustomer_All(PageParam pageParam, string search,long SalonId,string FromDate,string ToDate,long Type)
        {
            PagedList<AbstractSalonOwnerNewCustomer> SalonOwnerNewCustomer = new PagedList<AbstractSalonOwnerNewCustomer>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FromDate", FromDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ToDate", ToDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int64, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOwnerNewCustomer_All, param, commandType: CommandType.StoredProcedure);
                SalonOwnerNewCustomer.Values.AddRange(task.Read<SalonOwnerNewCustomer>());
                SalonOwnerNewCustomer.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SalonOwnerNewCustomer;
        }

        public override PagedList<AbstractSalonOwnerNewCustomer> SalonOwnerMostRequestedEmployee_All(PageParam pageParam, string search, long SalonId, string FromDate, string ToDate, long Type)
        {
            PagedList<AbstractSalonOwnerNewCustomer> SalonOwnerNewCustomer = new PagedList<AbstractSalonOwnerNewCustomer>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FromDate", FromDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ToDate", ToDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOwnerMostRequestedEmployee_All, param, commandType: CommandType.StoredProcedure);
                SalonOwnerNewCustomer.Values.AddRange(task.Read<SalonOwnerNewCustomer>());
                SalonOwnerNewCustomer.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SalonOwnerNewCustomer;
        }

        public override PagedList<AbstractSalonOwnerArray> SalonOwnerDashboard_All(PageParam pageParam, string search, long SalonId, string FromDate, string ToDate,long Type)
        {
            PagedList<AbstractSalonOwnerArray> SalonOwnerArray = new PagedList<AbstractSalonOwnerArray>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FromDate", FromDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ToDate", ToDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOwnerDashboard_All, param, commandType: CommandType.StoredProcedure);
                SalonOwnerArray.Values.AddRange(task.Read<SalonOwnerArray>());
                SalonOwnerArray.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SalonOwnerArray;
        }

        public override PagedList<AbstractSalonOwnerTopRequestedServices> SalonOwnerTopRequestedServices_All(PageParam pageParam, string search, long SalonId,string ServiceIds, string FromDate, string ToDate, long Type)
        {
            PagedList<AbstractSalonOwnerTopRequestedServices> SalonOwnerTopRequestedServices = new PagedList<AbstractSalonOwnerTopRequestedServices>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ServiceIds", ServiceIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@FromDate", FromDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ToDate", ToDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOwnerTopRequestedServices_All, param, commandType: CommandType.StoredProcedure);
                SalonOwnerTopRequestedServices.Values.AddRange(task.Read<SalonOwnerTopRequestedServices>());
                SalonOwnerTopRequestedServices.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SalonOwnerTopRequestedServices;
        }


    }
}
