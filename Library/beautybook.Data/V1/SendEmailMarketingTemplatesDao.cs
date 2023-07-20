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
    public class SendEmailMarketingTemplatesDao : AbstractSendEmailMarketingTemplatesDao
    {

        public override SuccessResult<AbstractSendEmailMarketingTemplates> SendEmailMarketingTemplates_Upsert(AbstractSendEmailMarketingTemplates abstractSendEmailMarketingTemplates)
        {
            SuccessResult<AbstractSendEmailMarketingTemplates> SendEmailMarketingTemplates = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractSendEmailMarketingTemplates.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractSendEmailMarketingTemplates.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractSendEmailMarketingTemplates.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@EmailSubject", abstractSendEmailMarketingTemplates.EmailSubject, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EmailTemplateName", abstractSendEmailMarketingTemplates.EmailTemplateName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UpdateHtmlTemplatesText", abstractSendEmailMarketingTemplates.UpdateHtmlTemplatesText, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractSendEmailMarketingTemplates.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
           

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SendEmailMarketingTemplates_Upsert, param, commandType: CommandType.StoredProcedure);
                SendEmailMarketingTemplates = task.Read<SuccessResult<AbstractSendEmailMarketingTemplates>>().SingleOrDefault();
                SendEmailMarketingTemplates.Item = task.Read<SendEmailMarketingTemplates>().SingleOrDefault();
            }

            return SendEmailMarketingTemplates;
        }

        public override PagedList<AbstractSendEmailMarketingTemplates> EmailUser_All(PageParam pageParam, string search, string CustomerSinceStartDate, string CustomerSinceEndDate, long MinAppoinment, long MaxAppoinment, long MinAge, long MaxAge, string LastVisitStartDate, string LastVisitEndDate, string ServicesIds,long IsAllCustomer,long SalonId)
        {
            PagedList<AbstractSendEmailMarketingTemplates> SendEmailMarketingTemplates = new PagedList<AbstractSendEmailMarketingTemplates>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CustomerSinceStartDate",CustomerSinceStartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CustomerSinceEndDate",CustomerSinceEndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@MinAppoinment", MinAppoinment, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MaxAppoinment",MaxAppoinment, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MinAge", MinAge, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MaxAge", MaxAge, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LastVisitStartDate", LastVisitStartDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LastVisitEndDate", LastVisitEndDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ServicesIds", ServicesIds, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsAllCustomer", IsAllCustomer, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmailUser_All, param, commandType: CommandType.StoredProcedure);
                SendEmailMarketingTemplates.Values.AddRange(task.Read<SendEmailMarketingTemplates>());
                SendEmailMarketingTemplates.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SendEmailMarketingTemplates;
        }

    }
}
