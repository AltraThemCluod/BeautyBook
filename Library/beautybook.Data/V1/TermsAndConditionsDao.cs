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
    public class TermsAndConditionsDao : AbstractTermsAndConditionsDao
    {
      
        public override PagedList<AbstractTermsAndConditions> TermsAndConditions_All(PageParam pageParam, string search)
        {
            PagedList<AbstractTermsAndConditions> TermsAndConditions = new PagedList<AbstractTermsAndConditions>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.TermsAndConditions_All, param, commandType: CommandType.StoredProcedure);
                TermsAndConditions.Values.AddRange(task.Read<TermsAndConditions>());
                TermsAndConditions.TotalRecords = task.Read<Int64>().SingleOrDefault();
            }
            return TermsAndConditions;
        }
        public override SuccessResult<AbstractTermsAndConditions> TermsAndConditions_ById(long Id)
        {
            SuccessResult<AbstractTermsAndConditions> TermsAndConditions = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.TermsAndConditions_ById, param, commandType: CommandType.StoredProcedure);
                TermsAndConditions = task.Read<SuccessResult<AbstractTermsAndConditions>>().SingleOrDefault();
                TermsAndConditions.Item = task.Read<TermsAndConditions>().SingleOrDefault();
            }

            return TermsAndConditions;
        }       
        public override SuccessResult<AbstractTermsAndConditions> TermsAndConditions_Upsert(AbstractTermsAndConditions abstractTermsAndConditions)
        {
            SuccessResult<AbstractTermsAndConditions> TermsAndConditions = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractTermsAndConditions.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@TermsAndConditionsText ", abstractTermsAndConditions.TermsAndConditionsText, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractTermsAndConditions.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UpdatedBy", abstractTermsAndConditions.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.TermsAndConditions_Upsert, param, commandType: CommandType.StoredProcedure);
                TermsAndConditions = task.Read<SuccessResult<AbstractTermsAndConditions>>().SingleOrDefault();
                TermsAndConditions.Item = task.Read<TermsAndConditions>().SingleOrDefault();
            }

            return TermsAndConditions;
        }
               
    }
}
