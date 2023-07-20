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
    public class Create_SMS_PackagesDao : AbstractCreate_SMS_PackagesDao
    {

        public override SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_Upsert(AbstractCreate_SMS_Packages abstractCreate_SMS_Packages)
        {
            SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractCreate_SMS_Packages.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SMSPackagesName", abstractCreate_SMS_Packages.SMSPackagesName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SMSPackagesPrice", abstractCreate_SMS_Packages.SMSPackagesPrice, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@LookUpDurationId", abstractCreate_SMS_Packages.LookUpDurationId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@NoOfTextMessages", abstractCreate_SMS_Packages.NoOfTextMessages, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractCreate_SMS_Packages.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy ", abstractCreate_SMS_Packages.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_SMS_Packages_Upsert, param, commandType: CommandType.StoredProcedure);
                Create_SMS_Packages = task.Read<SuccessResult<AbstractCreate_SMS_Packages>>().SingleOrDefault();
                Create_SMS_Packages.Item = task.Read<Create_SMS_Packages>().SingleOrDefault();
            }

            return Create_SMS_Packages;
        }

        //public override SuccessResult<AbstractCreate_SMS_Packages>Create_SMS_Packages_ById(long Id)
        //{
        //    SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages = null;
        //    var param = new DynamicParameters();

        //    param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

        //    using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
        //    {
        //        var task = con.QueryMultiple(SQLConfig.Create_SMS_Packages_ById, param, commandType: CommandType.StoredProcedure);
        //        Create_SMS_Packages = task.Read<SuccessResult<AbstractCreate_SMS_Packages>>().SingleOrDefault();
        //        Create_SMS_Packages.Item = task.Read<Create_SMS_Packages>().SingleOrDefault();
        //    }

        //    return Create_SMS_Packages;
        //}

        public override SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_ById(long Id)
        {
            SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_SMS_Packages_ById, param, commandType: CommandType.StoredProcedure);
                Create_SMS_Packages = task.Read<SuccessResult<AbstractCreate_SMS_Packages>>().SingleOrDefault();
                Create_SMS_Packages.Item = task.Read<Create_SMS_Packages>().SingleOrDefault();
            }

            return Create_SMS_Packages;
        }


        public override PagedList<AbstractCreate_SMS_Packages> Create_SMS_Packages_All(PageParam pageParam, string search, long LookUpDurationId,long SalonId)
        {
            PagedList<AbstractCreate_SMS_Packages> Create_SMS_Packages = new PagedList<AbstractCreate_SMS_Packages>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpDurationId", LookUpDurationId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_SMS_Packages_All, param, commandType: CommandType.StoredProcedure);
                Create_SMS_Packages.Values.AddRange(task.Read<Create_SMS_Packages>());
                Create_SMS_Packages.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Create_SMS_Packages;
        }

        public override SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_SMS_Packages_Delete, param, commandType: CommandType.StoredProcedure);
                Create_SMS_Packages = task.Read<SuccessResult<AbstractCreate_SMS_Packages>>().SingleOrDefault();
                Create_SMS_Packages.Item = task.Read<Create_SMS_Packages>().SingleOrDefault();
            }

            return Create_SMS_Packages;
        }

        public override SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages_ActInAct(long Id)
        {
            SuccessResult<AbstractCreate_SMS_Packages> Create_SMS_Packages = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Create_SMS_Packages_ActInAct, param, commandType: CommandType.StoredProcedure);
                Create_SMS_Packages = task.Read<SuccessResult<AbstractCreate_SMS_Packages>>().SingleOrDefault();
                Create_SMS_Packages.Item = task.Read<Create_SMS_Packages>().SingleOrDefault();
            }

            return Create_SMS_Packages;
        }

    }
}
