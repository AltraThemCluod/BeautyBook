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
    public class MasterOrderStatusDao : AbstractMasterOrderStatusDao
    {

        public override SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_Upsert(AbstractMasterOrderStatus abstractMasterOrderStatus)
        {
            SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractMasterOrderStatus.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Name", abstractMasterOrderStatus.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractMasterOrderStatus.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy ", abstractMasterOrderStatus.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterOrderStatus_Upsert, param, commandType: CommandType.StoredProcedure);
                MasterOrderStatus = task.Read<SuccessResult<AbstractMasterOrderStatus>>().SingleOrDefault();
                MasterOrderStatus.Item = task.Read<MasterOrderStatus>().SingleOrDefault();
            }

            return MasterOrderStatus;
        }

        public override SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_ById(long Id)
        {
            SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterOrderStatus_ById, param, commandType: CommandType.StoredProcedure);
                MasterOrderStatus = task.Read<SuccessResult<AbstractMasterOrderStatus>>().SingleOrDefault();
                MasterOrderStatus.Item = task.Read<MasterOrderStatus>().SingleOrDefault();
            }

            return MasterOrderStatus;
        }

        public override PagedList<AbstractMasterOrderStatus> MasterOrderStatus_All(PageParam pageParam, string search)
        {
            PagedList<AbstractMasterOrderStatus> MasterOrderStatus = new PagedList<AbstractMasterOrderStatus>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterOrderStatus_All, param, commandType: CommandType.StoredProcedure);
                MasterOrderStatus.Values.AddRange(task.Read<MasterOrderStatus>());
                MasterOrderStatus.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return MasterOrderStatus;
        }

        public override SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterOrderStatus_Delete, param, commandType: CommandType.StoredProcedure);
                MasterOrderStatus = task.Read<SuccessResult<AbstractMasterOrderStatus>>().SingleOrDefault();
                MasterOrderStatus.Item = task.Read<MasterOrderStatus>().SingleOrDefault();
            }

            return MasterOrderStatus;
        }

        public override SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus_ActInAct(long Id)
        {
            SuccessResult<AbstractMasterOrderStatus> MasterOrderStatus = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterOrderStatus_ActInAct, param, commandType: CommandType.StoredProcedure);
                MasterOrderStatus = task.Read<SuccessResult<AbstractMasterOrderStatus>>().SingleOrDefault();
                MasterOrderStatus.Item = task.Read<MasterOrderStatus>().SingleOrDefault();
            }

            return MasterOrderStatus;
        }

    }
}
