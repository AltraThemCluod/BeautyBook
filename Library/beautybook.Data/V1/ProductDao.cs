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
    public class ProductDao : AbstractProductDao
    {

        public override SuccessResult<AbstractProduct> Product_ById(long Id)
        {
            SuccessResult<AbstractProduct> Product = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Product_ById, param, commandType: CommandType.StoredProcedure);
                Product = task.Read<SuccessResult<AbstractProduct>>().SingleOrDefault();


                Product.Item = task.Read<Product>().SingleOrDefault();

                if (Product.Item != null)
                {

                    Product.Item.ProductHighlights = new List<ProductHighlights>();
                    Product.Item.ProductHighlights.AddRange(task.Read<ProductHighlights>());

                    Product.Item.ProductServiceHighlights = new List<ProductServiceHighlights>();
                    Product.Item.ProductServiceHighlights.AddRange(task.Read<ProductServiceHighlights>());

                    Product.Item.ProductSpecifications = new List<ProductSpecifications>();
                    Product.Item.ProductSpecifications.AddRange(task.Read<ProductSpecifications>());

                    Product.Item.ProductSeller = new List<Users>();
                    Product.Item.ProductSeller.AddRange(task.Read<Users>());

                    Product.Item.ProductImage = new List<ProductImage>();
                    Product.Item.ProductImage.AddRange(task.Read<ProductImage>());
                }

            }

            return Product;
        }
        public override SuccessResult<AbstractProduct> Product_ChangeStatus(long Id, long LookUpStatusId)
        {
            SuccessResult<AbstractProduct> Product = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Product_ChangeStatus, param, commandType: CommandType.StoredProcedure);
                Product = task.Read<SuccessResult<AbstractProduct>>().SingleOrDefault();
                Product.Item = task.Read<Product>().SingleOrDefault();
            }

            return Product;
        }

        public override SuccessResult<AbstractProduct> Product_UpdateQty(long Id, long ProductQty, long UpdatedBy)
        {
            SuccessResult<AbstractProduct> Product = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductQty", ProductQty, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Product_UpdateQty, param, commandType: CommandType.StoredProcedure);
                Product = task.Read<SuccessResult<AbstractProduct>>().SingleOrDefault();
                Product.Item = task.Read<Product>().SingleOrDefault();
            }

            return Product;
        }

        public override PagedList<AbstractProduct> Product_All(PageParam pageParam, string search, string ProductName, long ProductTypeId, long ProductBrandId, long LookUpStatusId, long VendorId, string ProductBrandIds, string ProductCategoryIds, string MinPrice, string MaxPrice,long SortBy)
        {
            PagedList<AbstractProduct> Product = new PagedList<AbstractProduct>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductName", ProductName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", ProductTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductBrandId", ProductBrandId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@VendorId", VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductBrandIds", ProductBrandIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductCategoryIds", ProductCategoryIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@MinPrice", MinPrice, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@MaxPrice", MaxPrice, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SortBy", SortBy, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Product_All, param, commandType: CommandType.StoredProcedure);
                Product.Values.AddRange(task.Read<Product>());
                Product.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Product;
        }

       

        public override SuccessResult<AbstractProduct> Product_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractProduct> Product = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Product_Delete, param, commandType: CommandType.StoredProcedure);
                Product = task.Read<SuccessResult<AbstractProduct>>().SingleOrDefault();
                Product.Item = task.Read<Product>().SingleOrDefault();
            }
            return Product;
        }

        public override SuccessResult<AbstractProduct> Product_Upsert(AbstractProduct abstractProduct)
        {
            SuccessResult<AbstractProduct> Product = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractProduct.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@VendorId", abstractProduct.VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductName", abstractProduct.ProductName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", abstractProduct.ProductTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductCategoryId", abstractProduct.ProductCategoryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductBrandId", abstractProduct.ProductBrandId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductQty", abstractProduct.ProductQty, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Price", abstractProduct.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@Weight", abstractProduct.Weight, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@WeightTypeId", abstractProduct.WeightTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LowQtyAlert", abstractProduct.LowQtyAlert, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractProduct.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractProduct.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductTax", abstractProduct.ProductTax, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductInformation", abstractProduct.ProductInformation, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ReturnPolicy", abstractProduct.ReturnPolicy, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@HighlightsLabel", abstractProduct.HighlightsLabel, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@HighlightsDiscription", abstractProduct.HighlightsDiscription, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SpecificationsHighlightsLabel", abstractProduct.SpecificationsHighlightsLabel, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SpecificationsHighlightsDiscription", abstractProduct.SpecificationsHighlightsDiscription, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ServiceHighlightsDiscription", abstractProduct.ServiceHighlightsDiscription, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ExpiryDate", abstractProduct.ExpiryDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsExpiryDate", abstractProduct.IsExpiryDate, dbType: DbType.Boolean, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Product_Upsert, param, commandType: CommandType.StoredProcedure);
                Product = task.Read<SuccessResult<AbstractProduct>>().SingleOrDefault();
                Product.Item = task.Read<Product>().SingleOrDefault();
            }

            return Product;
        }

    }
}