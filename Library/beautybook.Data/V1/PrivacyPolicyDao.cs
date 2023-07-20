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
    public class PrivacyPolicyDao : AbstractPrivacyPolicyDao
    {
        public override SuccessResult<AbstractPrivacyPolicy> PrivacyPolicy_ById(long Id , long Type)
        {
            SuccessResult<AbstractPrivacyPolicy> privacyPolicy = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Type", Type, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.PrivacyPolicy_ById, param, commandType: CommandType.StoredProcedure);
                privacyPolicy = task.Read<SuccessResult<AbstractPrivacyPolicy>>().SingleOrDefault();
                privacyPolicy.Item = task.Read<PrivacyPolicy>().SingleOrDefault();
            }

            return privacyPolicy;
        }

        public override SuccessResult<AbstractPrivacyPolicy> PrivacyPolicy_Upsert(AbstractPrivacyPolicy abstractPrivacyPolicy)
        {
            SuccessResult<AbstractPrivacyPolicy> privacyPolicy = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractPrivacyPolicy.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@PrivacyPolicyText", abstractPrivacyPolicy.PrivacyPolicyText, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Type", abstractPrivacyPolicy.Type, dbType: DbType.Int64, direction: ParameterDirection.Input);
          
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.PrivacyPolicy_Upsert, param, commandType: CommandType.StoredProcedure);
                privacyPolicy = task.Read<SuccessResult<AbstractPrivacyPolicy>>().SingleOrDefault();
                privacyPolicy.Item = task.Read<PrivacyPolicy>().SingleOrDefault();
            }

            return privacyPolicy;
        }
    }
}
