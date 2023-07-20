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
    public class OrderStatusTrackingDao : AbstractOrderStatusTrackingDao
    {
        public override SuccessResult<AbstractOrderStatusTracking> OrderStatusTracking_ByOrderId(long OrderId)
        {
            SuccessResult<AbstractOrderStatusTracking> OrderStatusTracking = null;
            var param = new DynamicParameters();

            param.Add("@OrderId", OrderId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderStatusTracking_ByOrderId, param, commandType: CommandType.StoredProcedure);
                OrderStatusTracking = task.Read<SuccessResult<AbstractOrderStatusTracking>>().SingleOrDefault();
                OrderStatusTracking.Item = task.Read<OrderStatusTracking>().SingleOrDefault();
            }

            return OrderStatusTracking;
        }
        public override SuccessResult<AbstractOrderStatusTracking> OrderStatusTracking_Upsert(AbstractOrderStatusTracking abstractOrderStatusTracking)
        {
            SuccessResult<AbstractOrderStatusTracking> OrderStatusTracking = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractOrderStatusTracking.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@OrderId", abstractOrderStatusTracking.OrderId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", abstractOrderStatusTracking.LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Comment", abstractOrderStatusTracking.Comment, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderStatusTracking_Upsert, param, commandType: CommandType.StoredProcedure);
                OrderStatusTracking = task.Read<SuccessResult<AbstractOrderStatusTracking>>().SingleOrDefault();
                OrderStatusTracking.Item = task.Read<OrderStatusTracking>().SingleOrDefault();
            }

            return OrderStatusTracking;
        }
    }
}
