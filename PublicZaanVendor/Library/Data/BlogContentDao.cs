using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicZaanVendor.Data;
using PublicZaanVendor.Entities.Contract;
using PublicZaanVendor.Entities.V1;
using Dapper;
using PublicZaanVendor.Common;

namespace PublicZaanVendor.Data.V1
{
    public class BlogContentDao
    {
        public SuccessResult<AbstractBlogContent> BlogContent_Upsert(AbstractBlogContent abstractBlogContent)
        {
            SuccessResult<AbstractBlogContent> BlogContent = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractBlogContent.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@BlogId", abstractBlogContent.BlogId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Heading", abstractBlogContent.Heading, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ImageUrl", abstractBlogContent.ImageUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Content", abstractBlogContent.Content, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogContent_Upsert, param, commandType: CommandType.StoredProcedure);
                BlogContent = task.Read<SuccessResult<AbstractBlogContent>>().SingleOrDefault();
                BlogContent.Item = task.Read<BlogContent>().SingleOrDefault();
            }
            return BlogContent;
        }

        public SuccessResult<AbstractBlogContent> BlogContent_Delete(int Id)
        {
            SuccessResult<AbstractBlogContent> BlogContent = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogContent_Delete, param, commandType: CommandType.StoredProcedure);
                BlogContent = task.Read<SuccessResult<AbstractBlogContent>>().SingleOrDefault();
                BlogContent.Item = task.Read<BlogContent>().SingleOrDefault();
            }

            return BlogContent;
        }
    }
}
