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
	public class MasterPOSCoinsBillsDao : AbstractMasterPOSCoinsBillsDao
	{

		public override PagedList<AbstractMasterPOSCoinsBills> MasterPOSCoinsBills_All(PageParam pageParam, string search)
		{
			PagedList<AbstractMasterPOSCoinsBills> MasterPOSCoinsBills = new PagedList<AbstractMasterPOSCoinsBills>();

			var param = new DynamicParameters();
			param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.MasterPOSCoinsBills_All, param, commandType: CommandType.StoredProcedure);
				MasterPOSCoinsBills.Values.AddRange(task.Read<MasterPOSCoinsBills>());
				MasterPOSCoinsBills.TotalRecords = task.Read<long>().SingleOrDefault();
			}
			return MasterPOSCoinsBills;
		}
		
	}
}
