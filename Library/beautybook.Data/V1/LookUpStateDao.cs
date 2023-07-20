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
    public class LookUpStateDao : AbstractLookUpStateDao
    {
        
        public override PagedList<AbstractLookUpState> LookUpState_All(PageParam pageParam, string search, long CountryId)
        {
            PagedList<AbstractLookUpState> LookUpState = new PagedList<AbstractLookUpState>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CountryId", CountryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.LookUpState_All, param, commandType: CommandType.StoredProcedure);
                LookUpState.Values.AddRange(task.Read<LookUpState>());
                LookUpState.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return LookUpState;
        }
        
    }
}
