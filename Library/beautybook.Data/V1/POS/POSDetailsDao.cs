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
using Newtonsoft.Json;

namespace BeautyBook.Data.V1
{
	public class POSDetailsDao : AbstractPOSDetailsDao
	{
		
		public override PagedList<AbstractPOSDetails>POSDetails_All(PageParam pageParam, string search , long SalonId, long POSSessionId)
		{
			PagedList<AbstractPOSDetails> POSDetails = new PagedList<AbstractPOSDetails>();

			var param = new DynamicParameters();
			param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
			param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@SessionId", POSSessionId, dbType: DbType.Int64, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSDetails_All, param, commandType: CommandType.StoredProcedure);
				POSDetails.Values.AddRange(task.Read<POSDetails>());
				POSDetails.TotalRecords = task.Read<long>().SingleOrDefault();
			}
			return POSDetails;
		}

		public override PagedList<AbstractPosOffer> PosOffer_All(long OfferId, long SalonId)
		{
			PagedList<AbstractPosOffer> PosOffer = new PagedList<AbstractPosOffer>();

			var param = new DynamicParameters();
			param.Add("@OfferId", OfferId, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.PosOffer_All, param, commandType: CommandType.StoredProcedure);
				PosOffer.Values.AddRange(task.Read<PosOffer>());
				PosOffer.TotalRecords = task.Read<long>().SingleOrDefault();
			}
			return PosOffer;
		}

		public override PagedList<AbstractPOSAssignEmployee> POSAssignEmployee_All(long LookUpUserTypeId, long SalonId)
		{
			PagedList<AbstractPOSAssignEmployee> POSAssignEmployee = new PagedList<AbstractPOSAssignEmployee>();

			var param = new DynamicParameters();
			param.Add("@LookUpUserTypeId", LookUpUserTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSAssignEmployee_All, param, commandType: CommandType.StoredProcedure);
				POSAssignEmployee.Values.AddRange(task.Read<POSAssignEmployee>());
				POSAssignEmployee.TotalRecords = task.Read<long>().SingleOrDefault();
			}
			return POSAssignEmployee;
		}

		public override SuccessResult<AbstractPOSDetails>POSDetails_ById(int Id)
		{
			SuccessResult<AbstractPOSDetails> POSDetails = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSDetails_ById, param, commandType: CommandType.StoredProcedure);
				POSDetails = task.Read<SuccessResult<AbstractPOSDetails>>().SingleOrDefault();
				POSDetails.Item = task.Read<POSDetails>().SingleOrDefault();
			}

			return POSDetails;
		}

		public override SuccessResult<AbstractPOSDetails>POSDetails_Delete(int Id, int DeletedBy)
		{
			SuccessResult<AbstractPOSDetails> POSDetails = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@DeletedBy", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSDetails_Delete, param, commandType: CommandType.StoredProcedure);
				POSDetails = task.Read<SuccessResult<AbstractPOSDetails>>().SingleOrDefault();
				POSDetails.Item = task.Read<POSDetails>().SingleOrDefault();
			}

			return POSDetails;
		}

		public override SuccessResult<AbstractPOSDetails>POSDetails_Upsert(AbstractPOSDetails abstractPOSDetails)
		{
			SuccessResult<AbstractPOSDetails> POSDetails = null;
			var param = new DynamicParameters();

			param.Add("@Id", abstractPOSDetails.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@POSSessionId", abstractPOSDetails.POSSessionId, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@POSTypeId", abstractPOSDetails.POSTypeId, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@CategoryId", abstractPOSDetails.CategoryId, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@ServiceIds", abstractPOSDetails.ServiceIds, dbType: DbType.String, direction: ParameterDirection.Input);
			param.Add("@CustomerId", abstractPOSDetails.CustomerId, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@CreatedBy", abstractPOSDetails.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@UpdatedBy", abstractPOSDetails.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSDetails_Upsert, param, commandType: CommandType.StoredProcedure);
				POSDetails = task.Read<SuccessResult<AbstractPOSDetails>>().SingleOrDefault();
				POSDetails.Item = task.Read<POSDetails>().SingleOrDefault();
			}
			return POSDetails;
		}

	}
}
