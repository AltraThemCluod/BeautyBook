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
    public class VendorApprovelDao : AbstractVendorApprovelDao
    {
        public override SuccessResult<AbstractVendorApprovel> VendorApprovel_IsApproved(int Id)
        {
            SuccessResult<AbstractVendorApprovel> VendorApprovel = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.VendorApprovel_IsApproved, param, commandType: CommandType.StoredProcedure);
                VendorApprovel = task.Read<SuccessResult<AbstractVendorApprovel>>().SingleOrDefault();
                VendorApprovel.Item = task.Read<VendorApprovel>().SingleOrDefault();
            }

            return VendorApprovel;
        }
        public override PagedList<AbstractVendorApprovel> VendorApprovel_All(PageParam pageParam, string search , int IsApprove , int LookUpUserTypeId)
        {
            PagedList<AbstractVendorApprovel> VendorApprovel = new PagedList<AbstractVendorApprovel>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsApprove", IsApprove, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpUserTypeId", LookUpUserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.VendorApprovel_All, param, commandType: CommandType.StoredProcedure);
                VendorApprovel.Values.AddRange(task.Read<VendorApprovel>());
                VendorApprovel.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return VendorApprovel;
        }
        

    }
}
