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
    public class ProductWishlistDao : AbstractProductWishlistDao
    {
        public override PagedList<AbstractProductWishlist> ProductWishlist_All(PageParam pageParam, string search, long SalonId)
        {
            PagedList<AbstractProductWishlist> ProductWishlist = new PagedList<AbstractProductWishlist>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProductWishlist_All, param, commandType: CommandType.StoredProcedure);
                ProductWishlist.Values.AddRange(task.Read<ProductWishlist>());
                ProductWishlist.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return ProductWishlist;
        }

        public override SuccessResult<AbstractProductWishlist> ProductWishlist_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractProductWishlist> ProductWishlist = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProductWishlist_Delete, param, commandType: CommandType.StoredProcedure);
                ProductWishlist = task.Read<SuccessResult<AbstractProductWishlist>>().SingleOrDefault();
                ProductWishlist.Item = task.Read<ProductWishlist>().SingleOrDefault();
            }

            return ProductWishlist;
        }
        public override SuccessResult<AbstractProductWishlist> ProductWishlist_Upsert(AbstractProductWishlist abstractProductWishlist)
        {
            SuccessResult<AbstractProductWishlist> ProductWishlist = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractProductWishlist.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductId", abstractProductWishlist.ProductId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractProductWishlist.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProductWishlist_Upsert, param, commandType: CommandType.StoredProcedure);
                ProductWishlist = task.Read<SuccessResult<AbstractProductWishlist>>().SingleOrDefault();
                ProductWishlist.Item = task.Read<ProductWishlist>().SingleOrDefault();
            }

            return ProductWishlist;
        }
    }
}
