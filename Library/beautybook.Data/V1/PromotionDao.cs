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
    public class PromotionDao : AbstractPromotionDao
    {
        public override PagedList<AbstractPromotion> Promotion_All(PageParam pageParam, string search, int VendorId, int ProductId, int ProductTypeId, int ProductBrandId)
        {
            PagedList<AbstractPromotion> Promotion = new PagedList<AbstractPromotion>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@VendorId", VendorId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ProductId", ProductId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId",ProductTypeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ProductBrandId",ProductBrandId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Promotion_All, param, commandType: CommandType.StoredProcedure);
                Promotion.Values.AddRange(task.Read<Promotion>());
                Promotion.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Promotion;
        }

        public override SuccessResult<AbstractPromotion> Promotion_ById(int Id)
        {
            SuccessResult<AbstractPromotion> Promotion = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Promotion_ById, param, commandType: CommandType.StoredProcedure);
                Promotion = task.Read<SuccessResult<AbstractPromotion>>().SingleOrDefault();
                Promotion.Item = task.Read<Promotion>().SingleOrDefault();
            }

            return Promotion;
        }

        public override SuccessResult<AbstractPromotion> Promotion_Upsert(AbstractPromotion abstractPromotion)
        {
            SuccessResult<AbstractPromotion> Promotion = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractPromotion.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@VendorId", abstractPromotion.VendorId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ProductId", abstractPromotion.ProductId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", abstractPromotion.ProductTypeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ProductBrandId", abstractPromotion.ProductBrandId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@StartDate", abstractPromotion.StartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EndDate", abstractPromotion.EndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@OriginalPrice", abstractPromotion.OriginalPrice, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@OfferPrice", abstractPromotion.OfferPrice, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractPromotion.CreatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy ", abstractPromotion.UpdatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Promotion_Upsert, param, commandType: CommandType.StoredProcedure);
                Promotion = task.Read<SuccessResult<AbstractPromotion>>().SingleOrDefault();
                Promotion.Item = task.Read<Promotion>().SingleOrDefault();
            }

            return Promotion;
        }
    }
}
