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
    public class AddToCartDao : AbstractAddToCartDao
    {

        public override SuccessResult<AbstractAddToCart> AddToCart_Upsert(AbstractAddToCart abstractAddToCart)
        {
            SuccessResult<AbstractAddToCart> AddToCart = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractAddToCart.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractAddToCart.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductId", abstractAddToCart.ProductId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Qty", abstractAddToCart.Qty, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractAddToCart.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.AddToCart_Upsert, param, commandType: CommandType.StoredProcedure);
                AddToCart = task.Read<SuccessResult<AbstractAddToCart>>().SingleOrDefault();
                AddToCart.Item = task.Read<AddToCart>().SingleOrDefault();
            }

            return AddToCart;
        }


        public override SuccessResult<AbstractAddToCart> AddToCart_UpdateQty(string ProductIds, string Qtys)
        {
            SuccessResult<AbstractAddToCart> AddToCart = null;
            var param = new DynamicParameters();

            param.Add("@ProductIds", ProductIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Qtys", Qtys, dbType: DbType.String, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.AddToCart_UpdateQty, param, commandType: CommandType.StoredProcedure);
                AddToCart = task.Read<SuccessResult<AbstractAddToCart>>().SingleOrDefault();
                AddToCart.Item = task.Read<AddToCart>().SingleOrDefault();
            }

            return AddToCart;
        }



        public override PagedList<AbstractAddToCart> AddToCart_All(PageParam pageParam, string search, long SalonId)
        {
            PagedList<AbstractAddToCart> AddToCart = new PagedList<AbstractAddToCart>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.AddToCart_All, param, commandType: CommandType.StoredProcedure);
                AddToCart.Values.AddRange(task.Read<AddToCart>());
                AddToCart.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return AddToCart;
        }

        public override SuccessResult<AbstractAddToCart> AddToCart_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractAddToCart> AddToCart = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.AddToCart_Delete, param, commandType: CommandType.StoredProcedure);
                AddToCart = task.Read<SuccessResult<AbstractAddToCart>>().SingleOrDefault();
                AddToCart.Item = task.Read<AddToCart>().SingleOrDefault();
            }

            return AddToCart;
        }

        
    }
}
