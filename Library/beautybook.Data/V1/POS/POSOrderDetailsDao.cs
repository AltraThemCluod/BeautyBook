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
	public class POSOrderDetailsDao : AbstractPOSOrderDetailsDao
	{
		public override PagedList<AbstractPOSOrderDetails>POSOrderDetails_All(PageParam pageParam, string search)
		{
			PagedList<AbstractPOSOrderDetails > POSOrderDetails  = new PagedList<AbstractPOSOrderDetails >();

			var param = new DynamicParameters();
			param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSOrderDetails_All, param, commandType: CommandType.StoredProcedure);
				POSOrderDetails.Values.AddRange(task.Read<POSOrderDetails>());
				POSOrderDetails.TotalRecords = task.Read<long>().SingleOrDefault();
			}
			return POSOrderDetails ;
		}
		public override SuccessResult<AbstractPOSOrderDetails>POSOrderDetails_ById(int Id)
		{
			SuccessResult<AbstractPOSOrderDetails> POSOrderDetails  = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSOrderDetails_ById, param, commandType: CommandType.StoredProcedure);
				POSOrderDetails = task.Read<SuccessResult<AbstractPOSOrderDetails>>().SingleOrDefault();
				POSOrderDetails.Item = task.Read<POSOrderDetails >().SingleOrDefault();
			}

			return POSOrderDetails ;
		}

		public override SuccessResult<AbstractPOSOrderDetails>POSOrderDetails_Delete(int Id, int DeletedBy)
		{
			SuccessResult<AbstractPOSOrderDetails > POSOrderDetails  = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@DeletedBy", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSOrderDetails_Delete, param, commandType: CommandType.StoredProcedure);
				POSOrderDetails  = task.Read<SuccessResult<AbstractPOSOrderDetails>>().SingleOrDefault();
				POSOrderDetails.Item = task.Read<POSOrderDetails>().SingleOrDefault();
			}

			return POSOrderDetails ;
		}

		public override SuccessResult<AbstractPOSOrderDetails>POSOrderDetails_Upsert(AbstractPOSOrderDetails  abstractPOSOrderDetails )
		{
			SuccessResult<AbstractPOSOrderDetails> POSOrderDetails  = null;
			var param = new DynamicParameters();

			param.Add("@Id", abstractPOSOrderDetails .Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@POSDetailsId", abstractPOSOrderDetails.POSDetailsId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@ServiceId", abstractPOSOrderDetails.ServiceId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@POSTypeId", abstractPOSOrderDetails.POSTypeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@CategoryId", abstractPOSOrderDetails.CategoryId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Price", abstractPOSOrderDetails.Price, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@AssignUserId", abstractPOSOrderDetails.AssignUserId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@CreatedBy", abstractPOSOrderDetails.CreatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@UpdatedBy", abstractPOSOrderDetails.UpdatedBy, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSOrderDetails_Upsert, param, commandType: CommandType.StoredProcedure);
				POSOrderDetails  = task.Read<SuccessResult<AbstractPOSOrderDetails>>().SingleOrDefault();
				POSOrderDetails.Item = task.Read<POSOrderDetails>().SingleOrDefault();
			}
			return POSOrderDetails ;
		}

	}
}
