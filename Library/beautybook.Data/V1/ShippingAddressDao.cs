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
    public class ShippingAddressDao : AbstractShippingAddressDao
    {
        public override PagedList<AbstractShippingAddress> ShippingAddress_All(PageParam pageParam, string search,long SalonId)
        {
            PagedList<AbstractShippingAddress> ShippingAddress = new PagedList<AbstractShippingAddress>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ShippingAddress_All, param, commandType: CommandType.StoredProcedure);
                ShippingAddress.Values.AddRange(task.Read<ShippingAddress>());
                ShippingAddress.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return ShippingAddress;
        }



        public override SuccessResult<AbstractShippingAddress> ShippingAddress_Upsert(AbstractShippingAddress abstractShippingAddress)
        {
            SuccessResult<AbstractShippingAddress> ShippingAddress = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractShippingAddress.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractShippingAddress.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Address", abstractShippingAddress.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@CountryId", abstractShippingAddress.CountryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@StateId", abstractShippingAddress.StateId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@City", abstractShippingAddress.City, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ZipCode ", abstractShippingAddress.ZipCode, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@PrimaryNumber", abstractShippingAddress.PrimaryNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@AlternateNumber", abstractShippingAddress.AlternateNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@IsDefault", abstractShippingAddress.IsDefault, dbType: DbType.Boolean, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractShippingAddress.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ShippingAddress_Upsert, param, commandType: CommandType.StoredProcedure);
                ShippingAddress = task.Read<SuccessResult<AbstractShippingAddress>>().SingleOrDefault();
                ShippingAddress.Item = task.Read<ShippingAddress>().SingleOrDefault();
            }

            return ShippingAddress;
        }
        public override SuccessResult<AbstractShippingAddress> ShippingAddress_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractShippingAddress> ShippingAddress = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ShippingAddress_Delete, param, commandType: CommandType.StoredProcedure);
                ShippingAddress = task.Read<SuccessResult<AbstractShippingAddress>>().SingleOrDefault();
                ShippingAddress.Item = task.Read<ShippingAddress>().SingleOrDefault();
            }
            return ShippingAddress;
        }

        public override SuccessResult<AbstractShippingAddress> ShippingAddress_ById(long Id)
        {
            SuccessResult<AbstractShippingAddress> ShippingAddress = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.ShippingAddress_ById, param, commandType: CommandType.StoredProcedure);
                ShippingAddress = task.Read<SuccessResult<AbstractShippingAddress>>().SingleOrDefault();
                ShippingAddress.Item = task.Read<ShippingAddress>().SingleOrDefault();
            }
            return ShippingAddress;
        }
    }
}
