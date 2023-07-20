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
    public class PackageCustomPriceDao : AbstractPackageCustomPriceDao
    {
        
        public override SuccessResult<AbstractPackageCustomPrice> PackageCustomPrice_Create(AbstractPackageCustomPrice abstractPackageCustomPrice)
        {
            SuccessResult<AbstractPackageCustomPrice> PackageCustomPrice = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractPackageCustomPrice.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractPackageCustomPrice.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@PackageId", abstractPackageCustomPrice.PackageId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpServiceId", abstractPackageCustomPrice.LookUpServiceId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CustomPrice", abstractPackageCustomPrice.CustomPrice, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.PackageCustomPrice_Create, param, commandType: CommandType.StoredProcedure);
                PackageCustomPrice = task.Read<SuccessResult<AbstractPackageCustomPrice>>().SingleOrDefault();
                PackageCustomPrice.Item = task.Read<PackageCustomPrice>().SingleOrDefault();
            }

            return PackageCustomPrice;
        }



    }
}
