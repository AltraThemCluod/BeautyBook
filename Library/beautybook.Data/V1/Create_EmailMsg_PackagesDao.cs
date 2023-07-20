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
    public class Create_EmailMsg_PackagesDao : AbstractCreate_EmailMsg_PackagesDao
    {

        public override SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_Upsert(AbstractCreate_EmailMsg_Packages abstractCreate_EmailMsg_Packages)
        {
            SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractCreate_EmailMsg_Packages.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            
            param.Add("@EmailMsgPackagesName", abstractCreate_EmailMsg_Packages.EmailMsgPackagesName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("EmailMsgPackagesPrice", abstractCreate_EmailMsg_Packages.EmailMsgPackagesPrice, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpDurationId", abstractCreate_EmailMsg_Packages.LookUpDurationId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@NoOfMessages", abstractCreate_EmailMsg_Packages.NoOfMessages, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractCreate_EmailMsg_Packages.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy ", abstractCreate_EmailMsg_Packages.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_EmailMsg_Packages_Upsert, param, commandType: CommandType.StoredProcedure);
                Create_EmailMsg_Packages = task.Read<SuccessResult<AbstractCreate_EmailMsg_Packages>>().SingleOrDefault();
                Create_EmailMsg_Packages.Item = task.Read<Create_EmailMsg_Packages>().SingleOrDefault();
            }

            return Create_EmailMsg_Packages;
        }

        public override SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_ById(long Id)
        {
            SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_EmailMsg_Packages_ById, param, commandType: CommandType.StoredProcedure);
                Create_EmailMsg_Packages = task.Read<SuccessResult<AbstractCreate_EmailMsg_Packages>>().SingleOrDefault();
                Create_EmailMsg_Packages.Item = task.Read<Create_EmailMsg_Packages>().SingleOrDefault();
            }

            return Create_EmailMsg_Packages;
        }

        public override PagedList<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_All(PageParam pageParam, string search,long LookUpDurationId,long SalonId)
        {
            PagedList<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages = new PagedList<AbstractCreate_EmailMsg_Packages>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpDurationId", LookUpDurationId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_EmailMsg_Packages_All, param, commandType: CommandType.StoredProcedure);
                Create_EmailMsg_Packages.Values.AddRange(task.Read<Create_EmailMsg_Packages>());
                Create_EmailMsg_Packages.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Create_EmailMsg_Packages;
        }

        public override SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_EmailMsg_Packages_Delete, param, commandType: CommandType.StoredProcedure);
                Create_EmailMsg_Packages = task.Read<SuccessResult<AbstractCreate_EmailMsg_Packages>>().SingleOrDefault();
                Create_EmailMsg_Packages.Item = task.Read<Create_EmailMsg_Packages>().SingleOrDefault();
            }

            return Create_EmailMsg_Packages;
        }

        public override SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages_ActInAct(long Id)
        {
            SuccessResult<AbstractCreate_EmailMsg_Packages> Create_EmailMsg_Packages = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_EmailMsg_Packages_ActInAct, param, commandType: CommandType.StoredProcedure);
                Create_EmailMsg_Packages = task.Read<SuccessResult<AbstractCreate_EmailMsg_Packages>>().SingleOrDefault();
                Create_EmailMsg_Packages.Item = task.Read<Create_EmailMsg_Packages>().SingleOrDefault();
            }

            return Create_EmailMsg_Packages;
        }

    }
}
