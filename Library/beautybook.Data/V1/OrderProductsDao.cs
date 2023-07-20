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
    public class OrderProductsDao : AbstractOrderProductsDao
    {

        public override PagedList<AbstractOrderProducts> OrderProducts_All(PageParam pageParam, string search, long OrderId,long VendorId)
        {
            PagedList<AbstractOrderProducts> OrderProducts = new PagedList<AbstractOrderProducts>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@OrderId", OrderId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@VendorId", VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderProducts_All, param, commandType: CommandType.StoredProcedure);
                OrderProducts.Values.AddRange(task.Read<OrderProducts>());
                OrderProducts.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return OrderProducts;
        }

        public override PagedList<AbstractOrderProducts> VendorProduct_ByVendorId(PageParam pageParam, string search, long VendorId, long SalonId)
        {
            PagedList<AbstractOrderProducts> OrderProducts = new PagedList<AbstractOrderProducts>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@VendorId", VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.VendorProduct_ByVendorId, param, commandType: CommandType.StoredProcedure);
                OrderProducts.Values.AddRange(task.Read<OrderProducts>());
                OrderProducts.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return OrderProducts;
        }

        public override bool OrderProducts_Delete(long Id)
        {
            bool result = false;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.OrderProducts_Delete, param, commandType: CommandType.StoredProcedure);
                result = task.SingleOrDefault<bool>();
            }
            return result;

        }

        public override SuccessResult<AbstractOrderProducts> OrderProducts_Upsert(AbstractOrderProducts abstractOrderProducts)
        {
            SuccessResult<AbstractOrderProducts> OrderProducts = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractOrderProducts.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@OrderId", abstractOrderProducts.OrderId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@VendorIdStr", abstractOrderProducts.VendorIdStr, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AddToCartIdStr", abstractOrderProducts.AddToCartIdStr, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderProducts_Upsert, param, commandType: CommandType.StoredProcedure);
                OrderProducts = task.Read<SuccessResult<AbstractOrderProducts>>().SingleOrDefault();
                OrderProducts.Item = task.Read<OrderProducts>().SingleOrDefault();
            }

            return OrderProducts;
        }

        public override SuccessResult<AbstractTotalNewOrder> SalonOwnerOrdersCount_VendorId(long VendorId)
        {
            SuccessResult<AbstractTotalNewOrder> TotalNewOrder = null;
            var param = new DynamicParameters();

            param.Add("@VendorId", VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOwnerOrdersCount_VendorId, param, commandType: CommandType.StoredProcedure);
                TotalNewOrder = task.Read<SuccessResult<AbstractTotalNewOrder>>().SingleOrDefault();
                TotalNewOrder.Item = task.Read<TotalNewOrder>().SingleOrDefault();
            }

            return TotalNewOrder;
        }

        public override SuccessResult<AbstractSalonOwnerOrdersCount> SalonOwnerOrdersCount_UpdateStatus(long OrderId)
        {
            SuccessResult<AbstractSalonOwnerOrdersCount> SalonOwnerOrdersCount = null;
            var param = new DynamicParameters();

            param.Add("@OrderId", OrderId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOwnerOrdersCount_UpdateStatus, param, commandType: CommandType.StoredProcedure);
                SalonOwnerOrdersCount = task.Read<SuccessResult<AbstractSalonOwnerOrdersCount>>().SingleOrDefault();
                SalonOwnerOrdersCount.Item = task.Read<SalonOwnerOrdersCount>().SingleOrDefault();
            }

            return SalonOwnerOrdersCount;
        }

    }
}
