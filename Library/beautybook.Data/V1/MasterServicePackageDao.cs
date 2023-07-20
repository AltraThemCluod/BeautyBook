﻿using System;
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
    public class MasterServicePackageDao : AbstractMasterServicePackageDao
    {
        
        public override PagedList<AbstractMasterServicePackage> MasterServicePackage_All(PageParam pageParam, string search,long ServicePackageId)
        {
            PagedList<AbstractMasterServicePackage> MasterServicePackage = new PagedList<AbstractMasterServicePackage>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@ServicePackageId", ServicePackageId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.MasterServicePackage_All, param, commandType: CommandType.StoredProcedure);
                MasterServicePackage.Values.AddRange(task.Read<MasterServicePackage>());
                MasterServicePackage.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return MasterServicePackage;
        }

    }
}
