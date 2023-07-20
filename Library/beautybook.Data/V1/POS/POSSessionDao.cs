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
	public class POSSessionDao : AbstractPOSSessionDao
	{

		public override PagedList<AbstractPOSSession> POSSession_BySalonId(PageParam pageParam, string search, long SalonId)
		{
			PagedList<AbstractPOSSession> POSSession = new PagedList<AbstractPOSSession>();

			var param = new DynamicParameters();
			param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
			param.Add("@SalonId", SalonId, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSSession_BySalonId, param, commandType: CommandType.StoredProcedure);
				POSSession.Values.AddRange(task.Read<POSSession>());
				POSSession.TotalRecords = task.Read<long>().SingleOrDefault();
			}
			return POSSession;
		}
		public override SuccessResult<AbstractPOSSession> POSSession_ById(long Id)
		{
			SuccessResult<AbstractPOSSession> POSSession = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSSession_ById, param, commandType: CommandType.StoredProcedure);
				POSSession = task.Read<SuccessResult<AbstractPOSSession>>().SingleOrDefault();
				POSSession.Item = task.Read<POSSession>().SingleOrDefault();
			}

			return POSSession;
		}

		public override SuccessResult<AbstractPOSSession> POSSession_TopBySalonId(long SalonId)
		{
			SuccessResult<AbstractPOSSession> POSSession = null;
			var param = new DynamicParameters();

			param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSSession_TopBySalonId, param, commandType: CommandType.StoredProcedure);
				POSSession = task.Read<SuccessResult<AbstractPOSSession>>().SingleOrDefault();
				POSSession.Item = task.Read<POSSession>().SingleOrDefault();
			}

			return POSSession;
		}

		public override SuccessResult<AbstractPOSSession> POSSession_Create(long Id, long SalonId, bool IsNewSessionCreate)
		{
			SuccessResult<AbstractPOSSession> POSSession = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@SalonId", SalonId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@IsNewSessionCreate", IsNewSessionCreate, dbType: DbType.Boolean, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSSession_Create, param, commandType: CommandType.StoredProcedure);
				POSSession = task.Read<SuccessResult<AbstractPOSSession>>().SingleOrDefault();
				POSSession.Item = task.Read<POSSession>().SingleOrDefault();
			}

			return POSSession;
		}

		public override SuccessResult<AbstractUsers> POSSession_Authentication(string Email , long @SessionId)
		{
			SuccessResult<AbstractUsers> Users = null;
			var param = new DynamicParameters();

			param.Add("@Email", Email, dbType: DbType.String, direction: ParameterDirection.Input);
			param.Add("@SessionId", SessionId, dbType: DbType.Int64, direction: ParameterDirection.Input);
			

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSSession_Authentication, param, commandType: CommandType.StoredProcedure);
				Users = task.Read<SuccessResult<AbstractUsers>>().SingleOrDefault();
				Users.Item = task.Read<Users>().SingleOrDefault();
			}

			return Users;
		}

		public override SuccessResult<AbstractPOSSession> POSSessionClosing_CachAndAt(long Id)
		{
			SuccessResult<AbstractPOSSession> POSSession = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);


			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSSessionClosing_CachAndAt, param, commandType: CommandType.StoredProcedure);
				POSSession = task.Read<SuccessResult<AbstractPOSSession>>().SingleOrDefault();
				POSSession.Item = task.Read<POSSession>().SingleOrDefault();
			}

			return POSSession;
		}
	}
}
