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
    public class SmsMarketingDao : AbstractSmsMarketingDao
    {
        public override SuccessResult<AbstractSmsMarketing> SmsMarketing_ById(long Id)
        {
            SuccessResult<AbstractSmsMarketing> SmsMarketing = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SmsMarketing_ById, param, commandType: CommandType.StoredProcedure);
                SmsMarketing = task.Read<SuccessResult<AbstractSmsMarketing>>().SingleOrDefault();
                SmsMarketing.Item = task.Read<SmsMarketing>().SingleOrDefault();
            }

            return SmsMarketing;
        }

        public override PagedList<AbstractSmsMarketing> SmsMarketing_All(PageParam pageParam, string search, long CreatedBy,long SalonId)
        {
            PagedList<AbstractSmsMarketing> SmsMarketing = new PagedList<AbstractSmsMarketing>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SmsMarketing_All, param, commandType: CommandType.StoredProcedure);
                SmsMarketing.Values.AddRange(task.Read<SmsMarketing>());
                SmsMarketing.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SmsMarketing;
        }


        public override SuccessResult<AbstractSmsMarketing> SmsMarketing_Upsert(AbstractSmsMarketing abstractSmsMarketing,long NoOfSMSCount)
        {
            SuccessResult<AbstractSmsMarketing> SmsMarketing = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractSmsMarketing.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractSmsMarketing.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SmsSubject", abstractSmsMarketing.SmsSubject, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Gender", abstractSmsMarketing.Gender, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CustomerSinceStart", abstractSmsMarketing.CustomerSinceStart, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CustomerSinceEnd", abstractSmsMarketing.CustomerSinceEnd, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LastVisitStart", abstractSmsMarketing.LastVisitStart, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LastVisitEnd", abstractSmsMarketing.LastVisitEnd, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@MinAppoinment", abstractSmsMarketing.MinAppoinment, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MaxAppoinment", abstractSmsMarketing.MaxAppoinment, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MinYear", abstractSmsMarketing.MinYear, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MaxYear", abstractSmsMarketing.MaxYear, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ServicesId", abstractSmsMarketing.ServicesId, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SmsTemplate", abstractSmsMarketing.SmsTemplate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractSmsMarketing.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractSmsMarketing.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@NoOfSMSCount", NoOfSMSCount, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SmsMarketing_Upsert, param, commandType: CommandType.StoredProcedure);
                SmsMarketing = task.Read<SuccessResult<AbstractSmsMarketing>>().SingleOrDefault();
                SmsMarketing.Item = task.Read<SmsMarketing>().SingleOrDefault();
            }

            return SmsMarketing;
        }
        public override SuccessResult<AbstractSmsMarketing> UserSMSMarketing_Upsert(long Id, long SMSMarketingId, string UserIds, string Body)
        {
            SuccessResult<AbstractSmsMarketing> SmsMarketing = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SMSMarketingId", SMSMarketingId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserIds", UserIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Body", Body, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserSMSMarketing_Upsert, param, commandType: CommandType.StoredProcedure);
                SmsMarketing = task.Read<SuccessResult<AbstractSmsMarketing>>().SingleOrDefault();
                SmsMarketing.Item = task.Read<SmsMarketing>().SingleOrDefault();
            }

            return SmsMarketing;
        }

    }
}
