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
    public class SellerProductsFeedbackDao : AbstractSellerProductsFeedbackDao
    {
        public override SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback_ById(long Id)
        {
            SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SellerProductsFeedback_ById, param, commandType: CommandType.StoredProcedure);
                SellerProductsFeedback = task.Read<SuccessResult<AbstractSellerProductsFeedback>>().SingleOrDefault();
                SellerProductsFeedback.Item = task.Read<SellerProductsFeedback>().SingleOrDefault();
            }

            return SellerProductsFeedback;
        }
        public override SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback_ChangeStatus(long Id, long LookUpStatusId, long LookUpStatusChangedBy)
        {
            SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusChangedBy", LookUpStatusChangedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SellerProductsFeedback_ChangeStatus, param, commandType: CommandType.StoredProcedure);
                SellerProductsFeedback = task.Read<SuccessResult<AbstractSellerProductsFeedback>>().SingleOrDefault();
                SellerProductsFeedback.Item = task.Read<SellerProductsFeedback>().SingleOrDefault();
            }

            return SellerProductsFeedback;
        }
        public override PagedList<AbstractSellerProductsFeedback> SellerProductsFeedback_All(PageParam pageParam, string search, AbstractSellerProductsFeedback abstractSellerProductsFeedback)
        {
            PagedList<AbstractSellerProductsFeedback> SellerProductsFeedback = new PagedList<AbstractSellerProductsFeedback>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@FeedbackByUserId", abstractSellerProductsFeedback.FeedbackByUserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SellerProductsFeedback_All, param, commandType: CommandType.StoredProcedure);
                SellerProductsFeedback.Values.AddRange(task.Read<SellerProductsFeedback>());
                SellerProductsFeedback.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SellerProductsFeedback;
        }
        public override SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback_Upsert(AbstractSellerProductsFeedback abstractSellerProductsFeedback)
        {
            SuccessResult<AbstractSellerProductsFeedback> SellerProductsFeedback = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractSellerProductsFeedback.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@FeedbackByUserId", abstractSellerProductsFeedback.FeedbackByUserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SellerProducts", abstractSellerProductsFeedback.SellerProducts, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Rating", abstractSellerProductsFeedback.Rating, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@Review", abstractSellerProductsFeedback.Review, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractSellerProductsFeedback.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractSellerProductsFeedback.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SellerProductsFeedback_Upsert, param, commandType: CommandType.StoredProcedure);
                SellerProductsFeedback = task.Read<SuccessResult<AbstractSellerProductsFeedback>>().SingleOrDefault();
                SellerProductsFeedback.Item = task.Read<SellerProductsFeedback>().SingleOrDefault();
            }

            return SellerProductsFeedback;
        }
       
    }
}
