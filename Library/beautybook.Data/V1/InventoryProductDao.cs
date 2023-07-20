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
    public class InventoryProductDao : AbstractInventoryProductDao
    {
        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_ById(long Id)
        {
            SuccessResult<AbstractInventoryProduct> InventoryProduct = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.InventoryProduct_ById, param, commandType: CommandType.StoredProcedure);
                InventoryProduct = task.Read<SuccessResult<AbstractInventoryProduct>>().SingleOrDefault();
                InventoryProduct.Item = task.Read<InventoryProduct>().SingleOrDefault();
            }

            return InventoryProduct;
        }

        public override PagedList<AbstractInventoryProduct> InventoryProduct_All(PageParam pageParam, string search, string ProductName, long ProductTypeId, long ProductBrandId, decimal Weight, long WeightTypeId, long SalonId)
        {
            PagedList<AbstractInventoryProduct> InventoryProduct = new PagedList<AbstractInventoryProduct>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductName", ProductName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", ProductTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductBrandId", ProductBrandId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Weight", Weight, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@WeightTypeId", WeightTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.InventoryProduct_All, param, commandType: CommandType.StoredProcedure);
                InventoryProduct.Values.AddRange(task.Read<InventoryProduct>());
                InventoryProduct.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return InventoryProduct;
        }

        public override PagedList<AbstractInventoryProduct> InventoryProduct_TopOne(PageParam pageParam, string search, string ProductName, long ProductTypeId, long ProductBrandId, long SalonId)
        {
            PagedList<AbstractInventoryProduct> InventoryProduct = new PagedList<AbstractInventoryProduct>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductName", ProductName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", ProductTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductBrandId", ProductBrandId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.InventoryProduct_TopOne, param, commandType: CommandType.StoredProcedure);
                InventoryProduct.Values.AddRange(task.Read<InventoryProduct>());
                InventoryProduct.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return InventoryProduct;
        }


        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_Upsert(AbstractInventoryProduct abstractInventoryProduct)
        {
            SuccessResult<AbstractInventoryProduct> InventoryProduct = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractInventoryProduct.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractInventoryProduct.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductName", abstractInventoryProduct.ProductName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", abstractInventoryProduct.ProductTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductBrandId", abstractInventoryProduct.ProductBrandId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@VendorId", abstractInventoryProduct.VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@OfflineVendorId", abstractInventoryProduct.OfflineVendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@BillNumber", abstractInventoryProduct.BillNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PurchaseDate", abstractInventoryProduct.PurchaseDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductQty", abstractInventoryProduct.ProductQty, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Price", abstractInventoryProduct.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@Weight", abstractInventoryProduct.Weight, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@WeightTypeId", abstractInventoryProduct.WeightTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LowQtyAlert", abstractInventoryProduct.LowQtyAlert, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@PurchaseVia", abstractInventoryProduct.PurchaseVia, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductImage", abstractInventoryProduct.ProductImage, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractInventoryProduct.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractInventoryProduct.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@IsExpiryDate", abstractInventoryProduct.IsExpiryDate, dbType: DbType.Boolean, direction: ParameterDirection.Input);
            param.Add("@ExpiryDate", abstractInventoryProduct.ExpiryDate, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.InventoryProduct_Upsert, param, commandType: CommandType.StoredProcedure);
                InventoryProduct = task.Read<SuccessResult<AbstractInventoryProduct>>().SingleOrDefault();
                InventoryProduct.Item = task.Read<InventoryProduct>().SingleOrDefault();
            }

            return InventoryProduct;
        }

        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_UpdateInventory(AbstractInventoryProduct abstractInventoryProduct)
        {
            SuccessResult<AbstractInventoryProduct> InventoryProduct = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractInventoryProduct.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@PurchaseDate", abstractInventoryProduct.PurchaseDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductQty", abstractInventoryProduct.ProductQty, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Price", abstractInventoryProduct.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@Weight", abstractInventoryProduct.Weight, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@WeightTypeId", abstractInventoryProduct.WeightTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractInventoryProduct.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.InventoryProduct_UpdateInventory, param, commandType: CommandType.StoredProcedure);
                InventoryProduct = task.Read<SuccessResult<AbstractInventoryProduct>>().SingleOrDefault();
                InventoryProduct.Item = task.Read<InventoryProduct>().SingleOrDefault();
            }

            return InventoryProduct;
        }

        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_UpdateQty(AbstractInventoryProduct abstractInventoryProduct)
        {
            SuccessResult<AbstractInventoryProduct> InventoryProduct = null;
            var param = new DynamicParameters();

            param.Add("@ProductName", abstractInventoryProduct.ProductName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductQty", abstractInventoryProduct.ProductQty, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", abstractInventoryProduct.ProductTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductBrandId", abstractInventoryProduct.ProductBrandId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Weight", abstractInventoryProduct.Weight, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@WeightTypeId", abstractInventoryProduct.WeightTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractInventoryProduct.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractInventoryProduct.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.InventoryProduct_UpdateQty, param, commandType: CommandType.StoredProcedure);
                InventoryProduct = task.Read<SuccessResult<AbstractInventoryProduct>>().SingleOrDefault();
                InventoryProduct.Item = task.Read<InventoryProduct>().SingleOrDefault();
            }

            return InventoryProduct;
        }

        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_LowQtyAlert(AbstractInventoryProduct abstractInventoryProduct)
        {
            SuccessResult<AbstractInventoryProduct> InventoryProduct = null;
            var param = new DynamicParameters();

            param.Add("@ProductName", abstractInventoryProduct.ProductName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LowQtyAlert", abstractInventoryProduct.LowQtyAlert, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", abstractInventoryProduct.ProductTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductBrandId", abstractInventoryProduct.ProductBrandId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Weight", abstractInventoryProduct.Weight, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@WeightTypeId", abstractInventoryProduct.WeightTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractInventoryProduct.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.InventoryProduct_LowQtyAlert, param, commandType: CommandType.StoredProcedure);
                InventoryProduct = task.Read<SuccessResult<AbstractInventoryProduct>>().SingleOrDefault();
                InventoryProduct.Item = task.Read<InventoryProduct>().SingleOrDefault();
            }

            return InventoryProduct;
        }

        public override SuccessResult<AbstractInventoryProduct> InventoryProduct_Delete(string Ids, long DeletedBy)
        {
            SuccessResult<AbstractInventoryProduct> InventoryProduct = null;
            var param = new DynamicParameters();

            param.Add("@Ids", Ids, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.InventoryProduct_Delete, param, commandType: CommandType.StoredProcedure);
                InventoryProduct = task.Read<SuccessResult<AbstractInventoryProduct>>().SingleOrDefault();
                InventoryProduct.Item = task.Read<InventoryProduct>().SingleOrDefault();
            }
            return InventoryProduct;
        }
    }
}
