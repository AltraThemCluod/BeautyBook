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
    public class TechnicalSupportDao : AbstractTechnicalSupportDao
    {

        public override SuccessResult<AbstractTechnicalSupport> TechnicalSupport_Upsert(AbstractTechnicalSupport abstractTechnicalSupport)
        {
            SuccessResult<AbstractTechnicalSupport> TechnicalSupport = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractTechnicalSupport.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractTechnicalSupport.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserTypeId", abstractTechnicalSupport.UserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractTechnicalSupport.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Discription", abstractTechnicalSupport.Discription, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractTechnicalSupport.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractTechnicalSupport.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.TechnicalSupport_Upsert, param, commandType: CommandType.StoredProcedure);
                TechnicalSupport = task.Read<SuccessResult<AbstractTechnicalSupport>>().SingleOrDefault();
                TechnicalSupport.Item = task.Read<TechnicalSupport>().SingleOrDefault();
            }

            return TechnicalSupport;
        }


        public override PagedList<AbstractTechnicalSupport> TechnicalSupport_All(PageParam pageParam, string search, long SalonId,long UserId,long UserTypeId)
        {
            PagedList<AbstractTechnicalSupport> TechnicalSupport = new PagedList<AbstractTechnicalSupport>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserTypeId", UserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.TechnicalSupport_All, param, commandType: CommandType.StoredProcedure);
                TechnicalSupport.Values.AddRange(task.Read<TechnicalSupport>());
                TechnicalSupport.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return TechnicalSupport;
        }

        public override SuccessResult<AbstractTechnicalSupport> TechnicalSupport_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractTechnicalSupport> TechnicalSupport = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.TechnicalSupport_Delete, param, commandType: CommandType.StoredProcedure);
                TechnicalSupport = task.Read<SuccessResult<AbstractTechnicalSupport>>().SingleOrDefault();
                TechnicalSupport.Item = task.Read<TechnicalSupport>().SingleOrDefault();
            }
            return TechnicalSupport;
        }

        public override SuccessResult<AbstractTechnicalSupport> TechnicalSupport_ById(long Id)
        {
            SuccessResult<AbstractTechnicalSupport> TechnicalSupport = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.TechnicalSupport_ById, param, commandType: CommandType.StoredProcedure);
                TechnicalSupport = task.Read<SuccessResult<AbstractTechnicalSupport>>().SingleOrDefault();
                TechnicalSupport.Item = task.Read<TechnicalSupport>().SingleOrDefault();
            }
            return TechnicalSupport;
        }

    }
}
