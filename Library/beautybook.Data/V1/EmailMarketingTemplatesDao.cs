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
    public class EmailMarketingTemplatesDao : AbstractEmailMarketingTemplatesDao
    {
       
        public override PagedList<AbstractEmailMarketingTemplates> EmailMarketingTemplates_All(PageParam pageParam, string search)
        {
            PagedList<AbstractEmailMarketingTemplates> EmailMarketingTemplates = new PagedList<AbstractEmailMarketingTemplates>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmailMarketingTemplates_All, param, commandType: CommandType.StoredProcedure);
                EmailMarketingTemplates.Values.AddRange(task.Read<EmailMarketingTemplates>());
                EmailMarketingTemplates.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return EmailMarketingTemplates;
        }

        public override SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates_ById(long Id)
        {
            SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmailMarketingTemplates_ById, param, commandType: CommandType.StoredProcedure);
                EmailMarketingTemplates = task.Read<SuccessResult<AbstractEmailMarketingTemplates>>().SingleOrDefault();
                EmailMarketingTemplates.Item = task.Read<EmailMarketingTemplates>().SingleOrDefault();
            }

            return EmailMarketingTemplates;
        }

        public override SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates_Delete(long Id , long DeletedBy)
        {
            SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmailMarketingTemplates_Delete, param, commandType: CommandType.StoredProcedure);
                EmailMarketingTemplates = task.Read<SuccessResult<AbstractEmailMarketingTemplates>>().SingleOrDefault();
                EmailMarketingTemplates.Item = task.Read<EmailMarketingTemplates>().SingleOrDefault();
            }

            return EmailMarketingTemplates;
        }

        public override SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates_Upsert(AbstractEmailMarketingTemplates abstractEmailMarketingTemplates)
        {
            SuccessResult<AbstractEmailMarketingTemplates> EmailMarketingTemplates = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractEmailMarketingTemplates.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Name", abstractEmailMarketingTemplates.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Image", abstractEmailMarketingTemplates.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@HtmlTemplatesText", abstractEmailMarketingTemplates.HtmlTemplatesText, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmailMarketingTemplates_Upsert, param, commandType: CommandType.StoredProcedure);
                EmailMarketingTemplates = task.Read<SuccessResult<AbstractEmailMarketingTemplates>>().SingleOrDefault();
                EmailMarketingTemplates.Item = task.Read<EmailMarketingTemplates>().SingleOrDefault();
            }

            return EmailMarketingTemplates;
        }


    }
}
