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
	public class POSOpeningCashDao : AbstractPOSOpeningCashDao
	{
	public override SuccessResult<AbstractPOSOpeningCash> POSOpeningCash_Add(AbstractPOSOpeningCash abstractPOSOpeningCash)
		{
			SuccessResult<AbstractPOSOpeningCash> POSOpeningCash = null;
			var param = new DynamicParameters();

			param.Add("@POSSessionId", abstractPOSOpeningCash.POSSessionId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@CoinsBillsId", abstractPOSOpeningCash.CoinsBillsId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Qty", abstractPOSOpeningCash.Qty, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Note", abstractPOSOpeningCash.Note, dbType: DbType.String, direction: ParameterDirection.Input);
			

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSOpeningCash_Add, param, commandType: CommandType.StoredProcedure);
				POSOpeningCash = task.Read<SuccessResult<AbstractPOSOpeningCash>>().SingleOrDefault();
				POSOpeningCash.Item = task.Read<POSOpeningCash>().SingleOrDefault();
			}
			return POSOpeningCash;
		}

	}
}
