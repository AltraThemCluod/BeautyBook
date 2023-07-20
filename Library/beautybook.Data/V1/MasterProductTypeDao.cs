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
using Newtonsoft.Json;

namespace BeautyBook.Data.V1
{
    public class MasterProductTypeDao : AbstractMasterProductTypeDao
    {

        public override SuccessResult<AbstractMasterProductType> MasterProductType_Upsert(AbstractMasterProductType abstractMasterProductType)
        {
            SuccessResult<AbstractMasterProductType> MasterProductType = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractMasterProductType.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Name", abstractMasterProductType.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ProductTypeImage", abstractMasterProductType.ProductTypeImage, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractMasterProductType.CreatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy ", abstractMasterProductType.UpdatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductType_Upsert, param, commandType: CommandType.StoredProcedure);
                MasterProductType = task.Read<SuccessResult<AbstractMasterProductType>>().SingleOrDefault();
                MasterProductType.Item = task.Read<MasterProductType>().SingleOrDefault();
            }

            return MasterProductType;
        }

        public override SuccessResult<AbstractMasterProductType> MasterProductType_ById(long Id)
        {
            SuccessResult<AbstractMasterProductType> MasterProductType = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductType_ById, param, commandType: CommandType.StoredProcedure);
                MasterProductType = task.Read<SuccessResult<AbstractMasterProductType>>().SingleOrDefault();
                MasterProductType.Item = task.Read<MasterProductType>().SingleOrDefault();
            }

            return MasterProductType;
        }

        public override PagedList<AbstractMasterProductType> MasterProductType_All(PageParam pageParam, string search , bool IsAllow)
        {
            PagedList<AbstractMasterProductType> MasterProductType = new PagedList<AbstractMasterProductType>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsAllow", IsAllow, dbType: DbType.Boolean, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductType_All, param, commandType: CommandType.StoredProcedure);
                MasterProductType.Values.AddRange(task.Read<MasterProductType>());
                MasterProductType.TotalRecords = task.Read<long>().SingleOrDefault();

                if(MasterProductType.Values != null)
                {
                    for (int i = 0; i < MasterProductType.Values.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(MasterProductType.Values[i].ProductCategoryJson)))
                        {
                            MasterProductType.Values[i].ProductCategoryJson = JsonConvert.DeserializeObject(Convert.ToString(MasterProductType.Values[i].ProductCategoryJson));
                        }
                    }
                }
            }
            return MasterProductType;
        }

        public override SuccessResult<AbstractMasterProductType> MasterProductType_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractMasterProductType> MasterProductType = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductType_Delete, param, commandType: CommandType.StoredProcedure);
                MasterProductType = task.Read<SuccessResult<AbstractMasterProductType>>().SingleOrDefault();
                MasterProductType.Item = task.Read<MasterProductType>().SingleOrDefault();
            }

            return MasterProductType;
        }

        public override SuccessResult<AbstractMasterProductType> MasterProductType_ActInAct(long Id)
        {
            SuccessResult<AbstractMasterProductType> MasterProductType = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterProductType_ActInAct, param, commandType: CommandType.StoredProcedure);
                MasterProductType = task.Read<SuccessResult<AbstractMasterProductType>>().SingleOrDefault();
                MasterProductType.Item = task.Read<MasterProductType>().SingleOrDefault();
            }

            return MasterProductType;
        }

    }
}
