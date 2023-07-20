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
    public class DailyDealsDao : AbstractDailyDealsDao
    {
        public override SuccessResult<AbstractDailyDeals> DailyDeals_Delete(long Id,long DeletedBy)
        {
            SuccessResult<AbstractDailyDeals> DailyDeals = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.DailyDeals_Delete, param, commandType: CommandType.StoredProcedure);
                DailyDeals = task.Read<SuccessResult<AbstractDailyDeals>>().SingleOrDefault();
                DailyDeals.Item = task.Read<DailyDeals>().SingleOrDefault();
            }

            return DailyDeals;
        }
        public override PagedList<AbstractDailyDeals> DailyDeals_All(PageParam pageParam, string search,long SalonId)
        {
            PagedList<AbstractDailyDeals> DailyDeals = new PagedList<AbstractDailyDeals>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.DailyDeals_All, param, commandType: CommandType.StoredProcedure);
                DailyDeals.Values.AddRange(task.Read<DailyDeals>());
                DailyDeals.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return DailyDeals;
        }
        public override SuccessResult<AbstractDailyDeals> DailyDeals_ById(long Id)
        {
            SuccessResult<AbstractDailyDeals> DailyDeals = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.DailyDeals_ById, param, commandType: CommandType.StoredProcedure);
                DailyDeals = task.Read<SuccessResult<AbstractDailyDeals>>().SingleOrDefault();
                DailyDeals.Item = task.Read<DailyDeals>().SingleOrDefault();
            }

            return DailyDeals;
        }       
       
        public override SuccessResult<AbstractDailyDeals> DailyDeals_Upsert(AbstractDailyDeals abstractDailyDeals)
        {
            SuccessResult<AbstractDailyDeals> DailyDeals = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractDailyDeals.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractDailyDeals.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@OfferDate", abstractDailyDeals.OfferDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@StartTime", abstractDailyDeals.StartTime, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EndTime", abstractDailyDeals.EndTime, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ServicesIds", abstractDailyDeals.ServicesIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PackagesIds", abstractDailyDeals.PackagesIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ServiceOfferPrice", abstractDailyDeals.ServiceOfferPrice, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PackagesOfferPrice", abstractDailyDeals.PackagesOfferPrice, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@OfferPrice", abstractDailyDeals.OfferPrice, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractDailyDeals.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractDailyDeals.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.DailyDeals_Upsert, param, commandType: CommandType.StoredProcedure);
                DailyDeals = task.Read<SuccessResult<AbstractDailyDeals>>().SingleOrDefault();
                DailyDeals.Item = task.Read<DailyDeals>().SingleOrDefault();
            }

            return DailyDeals;
        }
               
    }
}
