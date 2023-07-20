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
using Newtonsoft.Json;
using PublicZaanVendor.Common;
using PublicZaanVendor.Common.Paging;

namespace PublicZaanVendor.Data.V1
{
    public class BlogDao
    {
        public SuccessResult<AbstractBlog> Blog_ActInAct(int Id)
        {
            SuccessResult<AbstractBlog> Blog = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Blog_ActInAct, param, commandType: CommandType.StoredProcedure);
                Blog = task.Read<SuccessResult<AbstractBlog>>().SingleOrDefault();
                Blog.Item = task.Read<UsersBlog>().SingleOrDefault();
            }

            return Blog;
        }

        public PagedList<AbstractBlog> Blog_All(PageParam pageParam, string search,int CreatedUserId,int CategoryId)
        {
            PagedList<AbstractBlog> Blog = new PagedList<AbstractBlog>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedUserId", CreatedUserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@CategoryId", CategoryId, dbType: DbType.Int32, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Blog_All, param, commandType: CommandType.StoredProcedure);
                Blog.Values.AddRange(task.Read<UsersBlog>());
                Blog.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Blog;
        }

        public PagedList<AbstractBlog> BlogTopArticles_All()
        {
            PagedList<AbstractBlog> Blog = new PagedList<AbstractBlog>();

            var param = new DynamicParameters();

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogTopArticles_All, param, commandType: CommandType.StoredProcedure);
                Blog.Values.AddRange(task.Read<UsersBlog>());
                Blog.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Blog;
        }

        public PagedList<AbstractBlog> BlogTopPost_All()
        {
            PagedList<AbstractBlog> Blog = new PagedList<AbstractBlog>();

            var param = new DynamicParameters();

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogTopPost_All, param, commandType: CommandType.StoredProcedure);
                Blog.Values.AddRange(task.Read<UsersBlog>());
                Blog.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Blog;
        }

        public SuccessResult<AbstractBlog> Blog_ById(int Id)
        {
            SuccessResult<AbstractBlog> Blog = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Blog_ById, param, commandType: CommandType.StoredProcedure);
                Blog = task.Read<SuccessResult<AbstractBlog>>().SingleOrDefault();
                Blog.Item = task.Read<UsersBlog>().SingleOrDefault();

                if(Blog.Code == 200 && Blog.Item != null)
                {
                    if(Blog.Item.BlogContentStr != null && Blog.Item.BlogContentStr != "")
                    {
                        Blog.Item.BlogContent = JsonConvert.DeserializeObject<List<BlogContentRoot>>(Blog.Item.BlogContentStr);
                    }
                }
            }

            return Blog;
        }

        public SuccessResult<AbstractBlog> SocialShare_Insert(int BlogId, int Type)
        {
            SuccessResult<AbstractBlog> Blog = null;
            var param = new DynamicParameters();

            param.Add("@BlogId", BlogId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SocialShare_Insert, param, commandType: CommandType.StoredProcedure);
                Blog = task.Read<SuccessResult<AbstractBlog>>().SingleOrDefault();
                Blog.Item = task.Read<UsersBlog>().SingleOrDefault();
            }

            return Blog;
        }

        public SuccessResult<AbstractBlog> BlogLike_Insert(int BlogId, bool IsLike)
        {
            SuccessResult<AbstractBlog> Blog = null;
            var param = new DynamicParameters();

            param.Add("@BlogId", BlogId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@IsLike", IsLike, dbType: DbType.Boolean, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogLike_Insert, param, commandType: CommandType.StoredProcedure);
                Blog = task.Read<SuccessResult<AbstractBlog>>().SingleOrDefault();
                Blog.Item = task.Read<UsersBlog>().SingleOrDefault();
            }

            return Blog;
        }

        public SuccessResult<AbstractBlog> BlogSharedCount()
        {
            SuccessResult<AbstractBlog> Blog = null;
            var param = new DynamicParameters();

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogSharedCount, param, commandType: CommandType.StoredProcedure);
                Blog = task.Read<SuccessResult<AbstractBlog>>().SingleOrDefault();
                Blog.Item = task.Read<UsersBlog>().SingleOrDefault();
            }

            return Blog;
        }

        public SuccessResult<AbstractBlog> Blog_Delete(int Id)
        {
            SuccessResult<AbstractBlog> Blog = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Blog_Delete, param, commandType: CommandType.StoredProcedure);
                Blog = task.Read<SuccessResult<AbstractBlog>>().SingleOrDefault();
                Blog.Item = task.Read<UsersBlog>().SingleOrDefault();
            }

            return Blog;
        }

        public SuccessResult<AbstractBlog> Blog_Upsert(AbstractBlog abstractBlog)
        {
            SuccessResult<AbstractBlog> Blog = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractBlog.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@CategoryId", abstractBlog.CategoryId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ThumbnailImageUrl", abstractBlog.ThumbnailImageUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ThumbnailDataUrl", abstractBlog.ThumbnailDataUrl, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Title", abstractBlog.Title, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Description", abstractBlog.Description, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedUserId", abstractBlog.CreatedUserId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Blog_Upsert, param, commandType: CommandType.StoredProcedure);
                Blog = task.Read<SuccessResult<AbstractBlog>>().SingleOrDefault();
                Blog.Item = task.Read<UsersBlog>().SingleOrDefault();
            }
            return Blog;
        }

        ///// Blog images ///////////////////////////////////

        public PagedList<AbstractBlogImages> BlogImages_All(PageParam pageParam, string search, int UserId)
        {
            PagedList<AbstractBlogImages> BlogImages = new PagedList<AbstractBlogImages>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogImages_All, param, commandType: CommandType.StoredProcedure);
                BlogImages.Values.AddRange(task.Read<BlogImages>());
                BlogImages.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return BlogImages;
        }

        public SuccessResult<AbstractBlogImages> BlogImages_Delete(int Id)
        {
            SuccessResult<AbstractBlogImages> BlogImages = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogImages_Delete, param, commandType: CommandType.StoredProcedure);
                BlogImages = task.Read<SuccessResult<AbstractBlogImages>>().SingleOrDefault();
                BlogImages.Item = task.Read<BlogImages>().SingleOrDefault();
            }

            return BlogImages;
        }

        public SuccessResult<AbstractBlogImages> BlogImages_Upsert(AbstractBlogImages abstractBlogImages)
        {
            SuccessResult<AbstractBlogImages> BlogImages = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractBlogImages.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractBlogImages.UserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@ImageUrl", abstractBlogImages.ImageUrl, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogImages_Upsert, param, commandType: CommandType.StoredProcedure);
                BlogImages = task.Read<SuccessResult<AbstractBlogImages>>().SingleOrDefault();
                BlogImages.Item = task.Read<BlogImages>().SingleOrDefault();
            }
            return BlogImages;
        }

    }
}
