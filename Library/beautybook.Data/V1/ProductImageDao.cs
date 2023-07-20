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
    public class ProductImageDao : AbstractProductImageDao
    {
        

        public override SuccessResult<AbstractProductImage> ProductImage_Upsert(AbstractProductImage abstractProductImage)
        {
            SuccessResult<AbstractProductImage> ProductImage = null;
            var param = new DynamicParameters();
            param.Add("@Id", abstractProductImage.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductId", abstractProductImage.ProductId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ImageUrl", abstractProductImage.ImageUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractProductImage.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ProductImage_Upsert, param, commandType: CommandType.StoredProcedure);
                ProductImage = task.Read<SuccessResult<AbstractProductImage>>().SingleOrDefault();
                
            }

            return ProductImage;
        }

    }
}
