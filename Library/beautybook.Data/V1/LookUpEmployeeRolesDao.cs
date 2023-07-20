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
    public class LookUpEmployeeRolesDao : AbstractLookUpEmployeeRolesDao
    {
        
        public override PagedList<AbstractLookUpEmployeeRoles> LookUpEmployeeRoles_All(PageParam pageParam, string search,string IsLanguage)
        {
            PagedList<AbstractLookUpEmployeeRoles> LookUpEmployeeRoles = new PagedList<AbstractLookUpEmployeeRoles>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsLanguage", IsLanguage, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.LookUpEmployeeRoles_All, param, commandType: CommandType.StoredProcedure);
                LookUpEmployeeRoles.Values.AddRange(task.Read<LookUpEmployeeRoles>());
                LookUpEmployeeRoles.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return LookUpEmployeeRoles;
        }
        
    }
}
