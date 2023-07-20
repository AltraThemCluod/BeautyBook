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
using PublicZaanVendor.Common.Paging;

namespace PublicZaanVendor.Data.V1
{
    public class BlogCategoryDao
    {
        public SuccessResult<AbstractBlogCategory> BlogCategory_ActInAct(int Id)
        {
            SuccessResult<AbstractBlogCategory> BlogCategory = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogCategory_ActInAct, param, commandType: CommandType.StoredProcedure);
                BlogCategory = task.Read<SuccessResult<AbstractBlogCategory>>().SingleOrDefault();
                BlogCategory.Item = task.Read<BlogCategory>().SingleOrDefault();
            }

            return BlogCategory;
        }

        public PagedList<AbstractBlogCategory> BlogCategory_All(PageParam pageParam, string search)
        {
            PagedList<AbstractBlogCategory> BlogCategory = new PagedList<AbstractBlogCategory>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogCategory_All, param, commandType: CommandType.StoredProcedure);
                BlogCategory.Values.AddRange(task.Read<BlogCategory>());
                BlogCategory.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return BlogCategory;
        }
        public SuccessResult<AbstractBlogCategory> BlogCategory_ById(int Id)
        {
            SuccessResult<AbstractBlogCategory> BlogCategory = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogCategory_ById, param, commandType: CommandType.StoredProcedure);
                BlogCategory = task.Read<SuccessResult<AbstractBlogCategory>>().SingleOrDefault();
                BlogCategory.Item = task.Read<BlogCategory>().SingleOrDefault();
            }

            return BlogCategory;
        }

        public SuccessResult<AbstractBlogCategory> BlogCategory_Delete(int Id)
        {
            SuccessResult<AbstractBlogCategory> BlogCategory = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogCategory_Delete, param, commandType: CommandType.StoredProcedure);
                BlogCategory = task.Read<SuccessResult<AbstractBlogCategory>>().SingleOrDefault();
                BlogCategory.Item = task.Read<BlogCategory>().SingleOrDefault();
            }

            return BlogCategory;
        }

        public SuccessResult<AbstractBlogCategory> BlogCategory_Upsert(AbstractBlogCategory abstractBlogCategory)
        {
            SuccessResult<AbstractBlogCategory> BlogCategory = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractBlogCategory.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@CategoryName", abstractBlogCategory.CategoryName, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogCategory_Upsert, param, commandType: CommandType.StoredProcedure);
                BlogCategory = task.Read<SuccessResult<AbstractBlogCategory>>().SingleOrDefault();
                BlogCategory.Item = task.Read<BlogCategory>().SingleOrDefault();
            }
            return BlogCategory;
        }
               
    }
}
