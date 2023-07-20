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
    public class MasterProductBrandDao : AbstractMasterProductBrandDao
    {

        public override SuccessResult<AbstractMasterProductBrand> MasterProductBrand_Upsert(AbstractMasterProductBrand abstractMasterProductBrand)
        {
            SuccessResult<AbstractMasterProductBrand> MasterProductBrand = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractMasterProductBrand.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractMasterProductBrand.SalonId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Name", abstractMasterProductBrand.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractMasterProductBrand.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy ", abstractMasterProductBrand.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductBrand_Upsert, param, commandType: CommandType.StoredProcedure);
                MasterProductBrand = task.Read<SuccessResult<AbstractMasterProductBrand>>().SingleOrDefault();
                MasterProductBrand.Item = task.Read<MasterProductBrand>().SingleOrDefault();
            }

            return MasterProductBrand;
        }

        public override SuccessResult<AbstractMasterProductBrand> MasterProductBrand_ById(long Id)
        {
            SuccessResult<AbstractMasterProductBrand> MasterProductBrand = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductBrand_ById, param, commandType: CommandType.StoredProcedure);
                MasterProductBrand = task.Read<SuccessResult<AbstractMasterProductBrand>>().SingleOrDefault();
                MasterProductBrand.Item = task.Read<MasterProductBrand>().SingleOrDefault();
            }

            return MasterProductBrand;
        }

        public override PagedList<AbstractMasterProductBrand> MasterProductBrand_All(PageParam pageParam, string search,long SalonId)
        {
            PagedList<AbstractMasterProductBrand> MasterProductBrand = new PagedList<AbstractMasterProductBrand>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductBrand_All, param, commandType: CommandType.StoredProcedure);
                MasterProductBrand.Values.AddRange(task.Read<MasterProductBrand>());
                MasterProductBrand.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return MasterProductBrand;
        }

        public override SuccessResult<AbstractMasterProductBrand> MasterProductBrand_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractMasterProductBrand> MasterProductBrand = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductBrand_Delete, param, commandType: CommandType.StoredProcedure);
                MasterProductBrand = task.Read<SuccessResult<AbstractMasterProductBrand>>().SingleOrDefault();
                MasterProductBrand.Item = task.Read<MasterProductBrand>().SingleOrDefault();
            }

            return MasterProductBrand;
        }

        public override SuccessResult<AbstractMasterProductBrand> MasterProductBrand_ActInAct(long Id)
        {
            SuccessResult<AbstractMasterProductBrand> MasterProductBrand = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductBrand_ActInAct, param, commandType: CommandType.StoredProcedure);
                MasterProductBrand = task.Read<SuccessResult<AbstractMasterProductBrand>>().SingleOrDefault();
                MasterProductBrand.Item = task.Read<MasterProductBrand>().SingleOrDefault();
            }

            return MasterProductBrand;
        }

    }
}
