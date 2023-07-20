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
    public class OfflineVendorDao : AbstractOfflineVendorDao
    {
        public override SuccessResult<AbstractOfflineVendor> OfflineVendor_Upsert(AbstractOfflineVendor abstractOfflineVendor)
        {
            SuccessResult<AbstractOfflineVendor> OfflineVendor = null;

            var param = new DynamicParameters();

            param.Add("@Id", abstractOfflineVendor.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractOfflineVendor.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Name", abstractOfflineVendor.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@PhoneNumber", abstractOfflineVendor.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@Email", abstractOfflineVendor.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("CreatedBy", abstractOfflineVendor.CreatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);
 

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OfflineVendor_Upsert, param, commandType: CommandType.StoredProcedure);
                OfflineVendor = task.Read<SuccessResult<AbstractOfflineVendor>>().SingleOrDefault();
                OfflineVendor.Item = task.Read<OfflineVendor>().SingleOrDefault();
            }

            return OfflineVendor;
        }

        public override SuccessResult<AbstractOfflineVendor> OfflineVendor_ById(long Id)
        {
            SuccessResult<AbstractOfflineVendor> OfflineVendor = null;

            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OfflineVendor_ById, param, commandType: CommandType.StoredProcedure);
                OfflineVendor = task.Read<SuccessResult<AbstractOfflineVendor>>().SingleOrDefault();
                OfflineVendor.Item = task.Read<OfflineVendor>().SingleOrDefault();
            }

            return OfflineVendor;
        }




        public override PagedList<AbstractOfflineVendor> OfflineVendor_All(PageParam pageParam, string Search, long SalonId)
        {
            PagedList<AbstractOfflineVendor> OfflineVendor = new PagedList<AbstractOfflineVendor>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
            param.Add("@Search", Search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OfflineVendor_All, param, commandType: CommandType.StoredProcedure);
                OfflineVendor.Values.AddRange(task.Read<OfflineVendor>());
                OfflineVendor.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return OfflineVendor;
        }


      




    }

}



