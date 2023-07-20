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
    public class MasterProductWeightDao : AbstractMasterProductWeightDao
    {

        public override SuccessResult<AbstractMasterProductWeight> MasterProductWeight_Upsert(AbstractMasterProductWeight abstractMasterProductWeight)
        {
            SuccessResult<AbstractMasterProductWeight> MasterProductWeight = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractMasterProductWeight.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Name", abstractMasterProductWeight.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractMasterProductWeight.CreatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy ", abstractMasterProductWeight.UpdatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductWeight_Upsert, param, commandType: CommandType.StoredProcedure);
                MasterProductWeight = task.Read<SuccessResult<AbstractMasterProductWeight>>().SingleOrDefault();
                MasterProductWeight.Item = task.Read<MasterProductWeight>().SingleOrDefault();
            }

            return MasterProductWeight;
        }

        public override SuccessResult<AbstractMasterProductWeight> MasterProductWeight_ById(long Id)
        {
            SuccessResult<AbstractMasterProductWeight> MasterProductWeight = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductWeight_ById, param, commandType: CommandType.StoredProcedure);
                MasterProductWeight = task.Read<SuccessResult<AbstractMasterProductWeight>>().SingleOrDefault();
                MasterProductWeight.Item = task.Read<MasterProductWeight>().SingleOrDefault();
            }

            return MasterProductWeight;
        }

        public override PagedList<AbstractMasterProductWeight> MasterProductWeight_All(PageParam pageParam, string search)
        {
            PagedList<AbstractMasterProductWeight> MasterProductWeight = new PagedList<AbstractMasterProductWeight>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductWeight_All, param, commandType: CommandType.StoredProcedure);
                MasterProductWeight.Values.AddRange(task.Read<MasterProductWeight>());
                MasterProductWeight.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return MasterProductWeight;
        }

        public override SuccessResult<AbstractMasterProductWeight> MasterProductWeight_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractMasterProductWeight> MasterProductWeight = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductWeight_Delete, param, commandType: CommandType.StoredProcedure);
                MasterProductWeight = task.Read<SuccessResult<AbstractMasterProductWeight>>().SingleOrDefault();
                MasterProductWeight.Item = task.Read<MasterProductWeight>().SingleOrDefault();
            }

            return MasterProductWeight;
        }

        public override SuccessResult<AbstractMasterProductWeight> MasterProductWeight_ActInAct(long Id)
        {
            SuccessResult<AbstractMasterProductWeight> MasterProductWeight = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductWeight_ActInAct, param, commandType: CommandType.StoredProcedure);
                MasterProductWeight = task.Read<SuccessResult<AbstractMasterProductWeight>>().SingleOrDefault();
                MasterProductWeight.Item = task.Read<MasterProductWeight>().SingleOrDefault();
            }

            return MasterProductWeight;
        }

    }
}
