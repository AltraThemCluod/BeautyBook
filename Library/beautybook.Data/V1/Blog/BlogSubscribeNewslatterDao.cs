﻿using System;
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
    public class BlogSubscribeNewslatterDao : AbstractBlogSubscribeNewslatterDao
    {
        public override SuccessResult<AbstractBlogSubscribeNewslatter> BlogSubscribeNewslatter_Insert(AbstractBlogSubscribeNewslatter abstractBlogSubscribeNewslatter)
        {
            SuccessResult<AbstractBlogSubscribeNewslatter> BlogSubscribeNewslatter = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractBlogSubscribeNewslatter.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@BlogId", abstractBlogSubscribeNewslatter.BlogId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Email", abstractBlogSubscribeNewslatter.Email, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogSubscribeNewslatter_Insert, param, commandType: CommandType.StoredProcedure);
                BlogSubscribeNewslatter = task.Read<SuccessResult<AbstractBlogSubscribeNewslatter>>().SingleOrDefault();
                BlogSubscribeNewslatter.Item = task.Read<BlogSubscribeNewslatter>().SingleOrDefault();
            }
            return BlogSubscribeNewslatter;
        }

        public override PagedList<AbstractBlogSubscribeNewslatter> BlogSubscribeNewslatter_All(PageParam pageParam, string search)
        {
            PagedList<AbstractBlogSubscribeNewslatter> BlogSubscribeNewslatter = new PagedList<AbstractBlogSubscribeNewslatter>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.BlogSubscribeNewslatter_All, param, commandType: CommandType.StoredProcedure);
                BlogSubscribeNewslatter.Values.AddRange(task.Read<BlogSubscribeNewslatter>());
                BlogSubscribeNewslatter.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return BlogSubscribeNewslatter;
        }
    }
}
