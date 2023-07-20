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
    public class MasterProductCategoryDao : AbstractMasterProductCategoryDao
    {

        public override SuccessResult<AbstractMasterProductCategory> MasterProductCategory_Upsert(AbstractMasterProductCategory abstractMasterProductCategory)
        {
            SuccessResult<AbstractMasterProductCategory> MasterProductCategory = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractMasterProductCategory.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", abstractMasterProductCategory.ProductTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ProductCategoryName", abstractMasterProductCategory.ProductCategoryName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractMasterProductCategory.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy ", abstractMasterProductCategory.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductCategory_Upsert, param, commandType: CommandType.StoredProcedure);
                MasterProductCategory = task.Read<SuccessResult<AbstractMasterProductCategory>>().SingleOrDefault();
                MasterProductCategory.Item = task.Read<MasterProductCategory>().SingleOrDefault();
            }

            return MasterProductCategory;
        }

        public override SuccessResult<AbstractMasterProductCategory> MasterProductCategory_ById(long Id)
        {
            SuccessResult<AbstractMasterProductCategory> MasterProductCategory = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductCategory_ById, param, commandType: CommandType.StoredProcedure);
                MasterProductCategory = task.Read<SuccessResult<AbstractMasterProductCategory>>().SingleOrDefault();
                MasterProductCategory.Item = task.Read<MasterProductCategory>().SingleOrDefault();
            }

            return MasterProductCategory;
        }

        public override PagedList<AbstractMasterProductCategory> MasterProductCategory_All(PageParam pageParam, string search, long ProductTypeId)
        {
            PagedList<AbstractMasterProductCategory> MasterProductCategory = new PagedList<AbstractMasterProductCategory>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductTypeId", ProductTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductCategory_All, param, commandType: CommandType.StoredProcedure);
                MasterProductCategory.Values.AddRange(task.Read<MasterProductCategory>());
                MasterProductCategory.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return MasterProductCategory;
        }

        public override SuccessResult<AbstractMasterProductCategory> MasterProductCategory_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractMasterProductCategory> MasterProductCategory = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductCategory_Delete, param, commandType: CommandType.StoredProcedure);
                MasterProductCategory = task.Read<SuccessResult<AbstractMasterProductCategory>>().SingleOrDefault();
                MasterProductCategory.Item = task.Read<MasterProductCategory>().SingleOrDefault();
            }

            return MasterProductCategory;
        }

        public override SuccessResult<AbstractMasterProductCategory> MasterProductCategory_ActInact(long Id)
        {
            SuccessResult<AbstractMasterProductCategory> MasterProductCategory = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductCategory_ActInact, param, commandType: CommandType.StoredProcedure);
                MasterProductCategory = task.Read<SuccessResult<AbstractMasterProductCategory>>().SingleOrDefault();
                MasterProductCategory.Item = task.Read<MasterProductCategory>().SingleOrDefault();
            }

            return MasterProductCategory;
        }

    }
}
