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
    public class EmailMarketingDao : AbstractEmailMarketingDao
    {
        public override SuccessResult<AbstractEmailMarketing> EmailMarketing_ById(long Id)
        {
            SuccessResult<AbstractEmailMarketing> EmailMarketing = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmailMarketing_ById, param, commandType: CommandType.StoredProcedure);
                EmailMarketing = task.Read<SuccessResult<AbstractEmailMarketing>>().SingleOrDefault();
                EmailMarketing.Item = task.Read<EmailMarketing>().SingleOrDefault();
            }

            return EmailMarketing;
        }

        public override PagedList<AbstractEmailMarketing> EmailMarketing_All(PageParam pageParam, string search, long CreatedBy,long SalonId)
        {
            PagedList<AbstractEmailMarketing> EmailMarketing = new PagedList<AbstractEmailMarketing>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmailMarketing_All, param, commandType: CommandType.StoredProcedure);
                EmailMarketing.Values.AddRange(task.Read<EmailMarketing>());
                EmailMarketing.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return EmailMarketing;
        }


        public override SuccessResult<AbstractEmailMarketing> EmailMarketing_Upsert(AbstractEmailMarketing abstractEmailMarketing,long NoOfEmailCount)
        {
            SuccessResult<AbstractEmailMarketing> EmailMarketing = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractEmailMarketing.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractEmailMarketing.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@EmailSubject", abstractEmailMarketing.EmailSubject, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Gender", abstractEmailMarketing.Gender, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CustomerSinceStart", abstractEmailMarketing.CustomerSinceStart, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CustomerSinceEnd", abstractEmailMarketing.CustomerSinceEnd, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LastVisitStart", abstractEmailMarketing.LastVisitStart, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LastVisitEnd", abstractEmailMarketing.LastVisitEnd, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@MinAppoinment", abstractEmailMarketing.MinAppoinment, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MaxAppoinment", abstractEmailMarketing.MaxAppoinment, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MinYear", abstractEmailMarketing.MinYear, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MaxYear", abstractEmailMarketing.MaxYear, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ServicesId", abstractEmailMarketing.ServicesId, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsSendEmail", abstractEmailMarketing.IsSendEmail, dbType: DbType.Boolean, direction: ParameterDirection.Input);
            param.Add("@SaveStatus", abstractEmailMarketing.SaveStatus, dbType: DbType.Boolean, direction: ParameterDirection.Input);
            param.Add("@EmailTemplate", abstractEmailMarketing.EmailTemplate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractEmailMarketing.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractEmailMarketing.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@NoOfEmailCount", NoOfEmailCount, dbType: DbType.Int64, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmailMarketing_Upsert, param, commandType: CommandType.StoredProcedure);
                EmailMarketing = task.Read<SuccessResult<AbstractEmailMarketing>>().SingleOrDefault();
                EmailMarketing.Item = task.Read<EmailMarketing>().SingleOrDefault();
            }

            return EmailMarketing;
        }

        public override SuccessResult<AbstractEmailMarketing> UserEmailMarketing_Upsert(long Id,long EmailMarketingId,string UserIds, string Body)
        {
            SuccessResult<AbstractEmailMarketing> EmailMarketing = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@EmailMarketingId", EmailMarketingId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserIds", UserIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Body", Body, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.UserEmailMarketing_Upsert, param, commandType: CommandType.StoredProcedure);
                EmailMarketing = task.Read<SuccessResult<AbstractEmailMarketing>>().SingleOrDefault();
                EmailMarketing.Item = task.Read<EmailMarketing>().SingleOrDefault();
            }

            return EmailMarketing;
        }

    }
}
