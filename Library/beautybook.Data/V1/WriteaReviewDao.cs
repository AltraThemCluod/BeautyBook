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
    public class WriteaReviewDao : AbstractWriteaReviewDao
    {

        public override SuccessResult<AbstractWriteaReview> WriteaReview_Upsert(AbstractWriteaReview abstractWriteaReview)
        {
            SuccessResult<AbstractWriteaReview> WriteaReview = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractWriteaReview.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractWriteaReview.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductId", abstractWriteaReview.ProductId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@RatingNo", abstractWriteaReview.RatingNo, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Heading", abstractWriteaReview.Heading, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Review", abstractWriteaReview.Review, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractWriteaReview.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.WriteaReview_Upsert, param, commandType: CommandType.StoredProcedure);
                WriteaReview = task.Read<SuccessResult<AbstractWriteaReview>>().SingleOrDefault();
                WriteaReview.Item = task.Read<WriteaReview>().SingleOrDefault();
            }

            return WriteaReview;
        }


        public override PagedList<AbstractWriteaReview> WriteaReview_All(PageParam pageParam, string search, long ProductId,long VendorId)
        {
            PagedList<AbstractWriteaReview> WriteaReview = new PagedList<AbstractWriteaReview>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductId", ProductId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@VendorId", VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.WriteaReview_All, param, commandType: CommandType.StoredProcedure);
                WriteaReview.Values.AddRange(task.Read<WriteaReview>());
                WriteaReview.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return WriteaReview;
        }

    }
}
