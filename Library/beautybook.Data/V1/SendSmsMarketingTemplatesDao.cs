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
    public class SendSmsMarketingTemplatesDao : AbstractSendSmsMarketingTemplatesDao
    {

        public override SuccessResult<AbstractSendSmsMarketingTemplates> SendSMSMarketingTemplates_Upsert(AbstractSendSmsMarketingTemplates abstractSendSmsMarketingTemplates)
        {
            SuccessResult<AbstractSendSmsMarketingTemplates> SendSmsMarketingTemplates = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractSendSmsMarketingTemplates.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractSendSmsMarketingTemplates.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", abstractSendSmsMarketingTemplates.UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SMSSubject", abstractSendSmsMarketingTemplates.SMSSubject, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SMSTemplateName", abstractSendSmsMarketingTemplates.SMSTemplateName, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UpdateHtmlTemplatesText", abstractSendSmsMarketingTemplates.UpdateHtmlTemplatesText, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractSendSmsMarketingTemplates.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
           

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SendSMSMarketingTemplates_Upsert, param, commandType: CommandType.StoredProcedure);
                SendSmsMarketingTemplates = task.Read<SuccessResult<AbstractSendSmsMarketingTemplates>>().SingleOrDefault();
                SendSmsMarketingTemplates.Item = task.Read<SendSmsMarketingTemplates>().SingleOrDefault();
            }

            return SendSmsMarketingTemplates;
        }

        public override PagedList<AbstractSendSmsMarketingTemplates> SMSUser_All(PageParam pageParam, string search, string CustomerSinceStartDate, string CustomerSinceEndDate, long MinAppoinment, long MaxAppoinment, long MinAge, long MaxAge, string LastVisitStartDate, string LastVisitEndDate, string ServicesIds,long IsAllCustomer)
        {
            PagedList<AbstractSendSmsMarketingTemplates> SendSmsMarketingTemplates = new PagedList<AbstractSendSmsMarketingTemplates>();

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
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SMSUser_All, param, commandType: CommandType.StoredProcedure);
                SendSmsMarketingTemplates.Values.AddRange(task.Read<SendSmsMarketingTemplates>());
                SendSmsMarketingTemplates.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return SendSmsMarketingTemplates;
        }

    }
}
