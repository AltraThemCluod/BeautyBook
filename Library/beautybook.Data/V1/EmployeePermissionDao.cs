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
    public class EmployeePermissionDao : AbstractEmployeePermissionDao
    {
        public override PagedList<AbstractEmployeeModulePermission> EmployeeModulePermission_All(PageParam pageParam, string search , long EmployeeId)
        {
            PagedList<AbstractEmployeeModulePermission> EmployeeModulePermission = new PagedList<AbstractEmployeeModulePermission>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EmployeeId", EmployeeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
           
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmployeeModulePermission_All, param, commandType: CommandType.StoredProcedure);
                EmployeeModulePermission.Values.AddRange(task.Read<EmployeeModulePermission>());
                EmployeeModulePermission.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return EmployeeModulePermission;
        }

        public override PagedList<AbstractEmployeePermissionData> EmployeePermissionData_EmployeeId(PageParam pageParam, string search, long EmployeeId,long LookUpUserTypeId)
        {
            PagedList<AbstractEmployeePermissionData> EmployeePermissionData = new PagedList<AbstractEmployeePermissionData>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@EmployeeId", EmployeeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpUserTypeId", LookUpUserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmployeePermissionData_EmployeeId, param, commandType: CommandType.StoredProcedure);
                EmployeePermissionData.Values.AddRange(task.Read<EmployeePermissionData>());
                EmployeePermissionData.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return EmployeePermissionData;
        }

        public override SuccessResult<AbstractEmployeePermissionData> EmployeePermissionData_Upsert(AbstractEmployeePermissionData abstractEmployeePermissionData)
        {
            SuccessResult<AbstractEmployeePermissionData> EmployeePermissionData = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractEmployeePermissionData.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@EmployeeId", abstractEmployeePermissionData.EmployeeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@EMP_Id", abstractEmployeePermissionData.EMP_Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Value", abstractEmployeePermissionData.Value, dbType: DbType.Boolean, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.EmployeePermissionData_Upsert, param, commandType: CommandType.StoredProcedure);
                EmployeePermissionData = task.Read<SuccessResult<AbstractEmployeePermissionData>>().SingleOrDefault();
                EmployeePermissionData.Item = task.Read<EmployeePermissionData>().SingleOrDefault();
            }

            return EmployeePermissionData;
        }

        public override bool EmployeePermissionData_Delete(long EmployeeId)
        {
            bool result = false;
            var param = new DynamicParameters();

            param.Add("@EmployeeId", EmployeeId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.Query<bool>(SQLConfig.EmployeePermissionData_Delete, param, commandType: CommandType.StoredProcedure);
                result = task.SingleOrDefault<bool>();
            }
            return result;
        }
    }
}
