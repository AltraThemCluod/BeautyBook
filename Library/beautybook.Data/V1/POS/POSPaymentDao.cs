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
	public class POSPaymentDao : AbstractPOSPaymentDao
	{
	    public override SuccessResult<AbstractPOSPayment> POSPayment_ById(int Id)
		{
			SuccessResult<AbstractPOSPayment> POSPayment = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSPayment_ById, param, commandType: CommandType.StoredProcedure);
				POSPayment = task.Read<SuccessResult<AbstractPOSPayment>>().SingleOrDefault();
				POSPayment.Item = task.Read<POSPayment>().SingleOrDefault();
			}

			return POSPayment;
		}
			
		public override SuccessResult<AbstractPOSPayment> POSPayment_Upsert(AbstractPOSPayment abstractPOSPayment)
		{
			SuccessResult<AbstractPOSPayment> POSPayment = null;
			var param = new DynamicParameters();

			param.Add("@Id", abstractPOSPayment.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@POSDetailsId", abstractPOSPayment.POSDetailsId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@SubTotal", abstractPOSPayment.SubTotal, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@DiscountSales", abstractPOSPayment.DiscountSales, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@TotalSalesTax", abstractPOSPayment.TotalSalesTax, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@TotalAmount", abstractPOSPayment.TotalAmount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@PaymentTypeId", abstractPOSPayment.PaymentTypeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@CashAmount", abstractPOSPayment.CashAmount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@CareditAmount", abstractPOSPayment.CareditAmount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@QrCodeURL", abstractPOSPayment.QrCodeURL, dbType: DbType.String, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.POSPayment_Upsert, param, commandType: CommandType.StoredProcedure);
				POSPayment = task.Read<SuccessResult<AbstractPOSPayment>>().SingleOrDefault();
				POSPayment.Item = task.Read<POSPayment>().SingleOrDefault();
			}
			return POSPayment;
		}

		public override SuccessResult<AbstractPosReturnInvoice> PosReturnInvoice_Update(AbstractPosReturnInvoice abstractPosReturnInvoice)
		{
			SuccessResult<AbstractPosReturnInvoice> PosReturnInvoice = null;
			var param = new DynamicParameters();

			param.Add("@Id", abstractPosReturnInvoice.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@PosOrderInvoiceStr", abstractPosReturnInvoice.PosOrderInvoiceStr, dbType: DbType.String, direction: ParameterDirection.Input);
			param.Add("@TotalAmount", abstractPosReturnInvoice.TotalAmount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@SubTotal", abstractPosReturnInvoice.SubTotal, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@TotalSalesTax", abstractPosReturnInvoice.TotalSalesTax, dbType: DbType.Decimal, direction: ParameterDirection.Input);
			param.Add("@QrCodeURL", abstractPosReturnInvoice.QrCodeURL, dbType: DbType.String, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.PosReturnInvoice_Update, param, commandType: CommandType.StoredProcedure);
				PosReturnInvoice = task.Read<SuccessResult<AbstractPosReturnInvoice>>().SingleOrDefault();
				PosReturnInvoice.Item = task.Read<PosReturnInvoice>().SingleOrDefault();
			}
			return PosReturnInvoice;
		}

		public override SuccessResult<AbstractPosInvoice> PosInvoice_ById(int Id)
		{
			SuccessResult<AbstractPosInvoice> PosInvoice = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.PosInvoice_ById, param, commandType: CommandType.StoredProcedure);
				PosInvoice = task.Read<SuccessResult<AbstractPosInvoice>>().SingleOrDefault();
				PosInvoice.Item = task.Read<PosInvoice>().SingleOrDefault();
			}

			return PosInvoice;
		}

		public override SuccessResult<AbstractPosInvoice> PosReturnInvoice_ById(int Id)
		{
			SuccessResult<AbstractPosInvoice> PosInvoice = null;
			var param = new DynamicParameters();

			param.Add("@Id", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.PosReturnInvoice_ById, param, commandType: CommandType.StoredProcedure);
				PosInvoice = task.Read<SuccessResult<AbstractPosInvoice>>().SingleOrDefault();
				PosInvoice.Item = task.Read<PosInvoice>().SingleOrDefault();
			}

			return PosInvoice;
		}

		public override PagedList<AbstractPosInvoice> PosInvoice_All(PageParam pageParam, string search,long SalonId, string InvoiceDate, string InvoiceNumber, long CustomerId)
		{
			PagedList<AbstractPosInvoice> PosInvoice = new PagedList<AbstractPosInvoice>();

			var param = new DynamicParameters();
			param.Add("@Offset", pageParam.Offset, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Limit", pageParam.Limit, dbType: DbType.Int32, direction: ParameterDirection.Input);
			param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
			param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
			param.Add("@InvoiceDate", InvoiceDate, dbType: DbType.String, direction: ParameterDirection.Input);
			param.Add("@InvoiceNumber", InvoiceNumber, dbType: DbType.String, direction: ParameterDirection.Input);
			param.Add("@CustomerId", CustomerId, dbType: DbType.Int64, direction: ParameterDirection.Input);

			using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
			{
				var task = con.QueryMultiple(SQLConfig.PosInvoice_All, param, commandType: CommandType.StoredProcedure);
				PosInvoice.Values.AddRange(task.Read<PosInvoice>());
				PosInvoice.TotalRecords = task.Read<long>().SingleOrDefault();
			}
			return PosInvoice;
		}

	}
}
