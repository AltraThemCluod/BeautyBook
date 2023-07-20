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
    public class SalonVendorDashboardDao : AbstractSalonVendorDashboardDao
    {
        public override SuccessResult<AbstractVendorSalesAndRating> VendorSalesAndRating_VendorId(long VendorId)
        {
            SuccessResult<AbstractVendorSalesAndRating> VendorSalesAndRating = null;
            var param = new DynamicParameters();

            param.Add("@VendorId", VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.VendorSalesAndRating_VendorId, param, commandType: CommandType.StoredProcedure);
                VendorSalesAndRating = task.Read<SuccessResult<AbstractVendorSalesAndRating>>().SingleOrDefault();
                VendorSalesAndRating.Item = task.Read<VendorSalesAndRating>().SingleOrDefault();
            }

            return VendorSalesAndRating;
        }
        public override PagedList<AbstractVendorTopCustomer> VendorTopCustomer_All(PageParam pageParam, string search,long VendorId, string FromDate,string ToDate,long Type)
        {
            PagedList<AbstractVendorTopCustomer> VendorTopCustomer = new PagedList<AbstractVendorTopCustomer>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@VendorId", VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FromDate", FromDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ToDate", ToDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int64, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.VendorTopCustomer_All, param, commandType: CommandType.StoredProcedure);
                VendorTopCustomer.Values.AddRange(task.Read<VendorTopCustomer>());
                VendorTopCustomer.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return VendorTopCustomer;
        }

        public override PagedList<AbstractVendorTopProduct> VendorTopProduct_All(PageParam pageParam, string search, long VendorId, string FromDate, string ToDate, long Type)
        {
            PagedList<AbstractVendorTopProduct> VendorTopProduct = new PagedList<AbstractVendorTopProduct>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@VendorId", VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FromDate", FromDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ToDate", ToDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.VendorTopProduct_All, param, commandType: CommandType.StoredProcedure);
                VendorTopProduct.Values.AddRange(task.Read<VendorTopProduct>());
                VendorTopProduct.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return VendorTopProduct;
        }
    }
}
