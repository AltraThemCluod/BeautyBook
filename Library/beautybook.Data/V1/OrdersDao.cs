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
    public class OrdersDao : AbstractOrdersDao
    {
        public override SuccessResult<AbstractOrders> Orders_ById(long Id)
        {
            SuccessResult<AbstractOrders> Orders = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Orders_ById, param, commandType: CommandType.StoredProcedure);
                Orders = task.Read<SuccessResult<AbstractOrders>>().SingleOrDefault();
                Orders.Item = task.Read<Orders>().SingleOrDefault();

                if (Orders.Item != null)
                {
                    Orders.Item.OrderProducts = new List<OrderProducts>();
                    Orders.Item.OrderProducts.AddRange(task.Read<OrderProducts>());
                }

                if (Orders.Item != null)
                {
                    Orders.Item.OrderStatusTracking = new List<OrderStatusTracking>();
                    Orders.Item.OrderStatusTracking.AddRange(task.Read<OrderStatusTracking>());
                }
            }

            return Orders;
        }

        public override SuccessResult<AbstractOrders> Orders_PaymentComplete(long Id)
        {
            SuccessResult<AbstractOrders> Orders = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Orders_PaymentComplete, param, commandType: CommandType.StoredProcedure);
                Orders = task.Read<SuccessResult<AbstractOrders>>().SingleOrDefault();
                Orders.Item = task.Read<Orders>().SingleOrDefault();
            }

            return Orders;
        }

        public override SuccessResult<AbstractOrders> Orders_ChangeStatus(long Id, long LookUpStatusId, string Comment)
        {
            SuccessResult<AbstractOrders> Orders = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Comment", Comment, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Orders_ChangeStatus, param, commandType: CommandType.StoredProcedure);
                Orders = task.Read<SuccessResult<AbstractOrders>>().SingleOrDefault();
                Orders.Item = task.Read<Orders>().SingleOrDefault();
            }

            return Orders;
        }

        public override PagedList<AbstractOrders> Orders_All(PageParam pageParam, string search, long LookUpStatusId, string OrderNo, string DateOfOrder, long SalonId, long VendorId)
        {
            PagedList<AbstractOrders> Orders = new PagedList<AbstractOrders>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@LookUpStatusId", LookUpStatusId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@OrderNo", OrderNo, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@DateOfOrder", DateOfOrder, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@VendorId", VendorId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Orders_All, param, commandType: CommandType.StoredProcedure);
                Orders.Values.AddRange(task.Read<Orders>());
                Orders.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Orders;
        }

        public override PagedList<AbstractOrders> SalonOrder_All(PageParam pageParam, string search, long SalonId)
        {
            PagedList<AbstractOrders> Orders = new PagedList<AbstractOrders>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOrder_All, param, commandType: CommandType.StoredProcedure);
                Orders.Values.AddRange(task.Read<Orders>());
                Orders.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return Orders;
        }

        public override SuccessResult<AbstractOrders> Orders_Upsert(AbstractOrders abstractOrders)
        {
            SuccessResult<AbstractOrders> Orders = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractOrders.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@SalonId", abstractOrders.SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CreatedBy", abstractOrders.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@AddToCartIdStr", abstractOrders.AddToCartIdStr, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@VendorIdStr", abstractOrders.VendorIdStr, dbType: DbType.String, direction: ParameterDirection.Input);


            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Orders_Upsert, param, commandType: CommandType.StoredProcedure);
                Orders = task.Read<SuccessResult<AbstractOrders>>().SingleOrDefault();
                Orders.Item = task.Read<Orders>().SingleOrDefault();
            }

            return Orders;
        }

        public override SuccessResult<AbstractOrders> Orders_UpdateCheckoutDetails(AbstractOrders abstractOrders)
        {
            SuccessResult<AbstractOrders> Orders = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractOrders.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@ShippingAddressId", abstractOrders.ShippingAddressId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@IsBillingAddress", abstractOrders.IsBillingAddress, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@BillingAddressId", abstractOrders.BillingAddressId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Orders_UpdateCheckoutDetails, param, commandType: CommandType.StoredProcedure);
                Orders = task.Read<SuccessResult<AbstractOrders>>().SingleOrDefault();
                Orders.Item = task.Read<Orders>().SingleOrDefault();
            }

            return Orders;
        }

        public override SuccessResult<AbstractOrders> Orders_UpdatePaymentMethod(AbstractOrders abstractOrders)
        {
            SuccessResult<AbstractOrders> Orders = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractOrders.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@MasterPaymentMethodId", abstractOrders.MasterPaymentMethodId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Orders_UpdatePaymentMethod, param, commandType: CommandType.StoredProcedure);
                Orders = task.Read<SuccessResult<AbstractOrders>>().SingleOrDefault();
                Orders.Item = task.Read<Orders>().SingleOrDefault();
            }

            return Orders;
        }

        public override SuccessResult<AbstractOrders> Orders_Delete(long Id, long DeletedBy)
        {
            SuccessResult<AbstractOrders> Orders = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@DeletedBy", DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.Orders_Delete, param, commandType: CommandType.StoredProcedure);
                Orders = task.Read<SuccessResult<AbstractOrders>>().SingleOrDefault();
                Orders.Item = task.Read<Orders>().SingleOrDefault();
            }
            return Orders;
        }


        //Invoice

        public override PagedList<AbstractOrderInvoiceMultipalVendor> OrderInvoiceMultipalVendor_All(PageParam pageParam, string search, long OrderId)
        {
            PagedList<AbstractOrderInvoiceMultipalVendor> OrderInvoiceMultipalVendor = new PagedList<AbstractOrderInvoiceMultipalVendor>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@OrderId", OrderId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderInvoiceMultipalVendor_All, param, commandType: CommandType.StoredProcedure);
                OrderInvoiceMultipalVendor.Values.AddRange(task.Read<OrderInvoiceMultipalVendor>());
                OrderInvoiceMultipalVendor.TotalRecords = task.Read<long>().SingleOrDefault();

                for (int i = 0; i < OrderInvoiceMultipalVendor.Values.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(OrderInvoiceMultipalVendor.Values[i].OrderObjectStr)))
                    {
                        OrderInvoiceMultipalVendor.Values[i].OrderObject = JsonConvert.DeserializeObject<List<OrderObject>>(Convert.ToString(OrderInvoiceMultipalVendor.Values[i].OrderObjectStr));
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(OrderInvoiceMultipalVendor.Values[i].SalonObjectStr)))
                    {
                        OrderInvoiceMultipalVendor.Values[i].SalonObject = JsonConvert.DeserializeObject<List<SalonObject>>(Convert.ToString(OrderInvoiceMultipalVendor.Values[i].SalonObjectStr));
                    }
                }

            }
            return OrderInvoiceMultipalVendor;
        }

        public override SuccessResult<AbstractOrderInvoiceMultipalVendor> OrderInvoiceMultipalVendor_ById(long OrderId, long UserId)
        {
            SuccessResult<AbstractOrderInvoiceMultipalVendor> OrderInvoiceMultipalVendor = null;
            var param = new DynamicParameters();

            param.Add("@OrderId", OrderId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderInvoiceMultipalVendor_ById, param, commandType: CommandType.StoredProcedure);
                OrderInvoiceMultipalVendor = task.Read<SuccessResult<AbstractOrderInvoiceMultipalVendor>>().SingleOrDefault();
                OrderInvoiceMultipalVendor.Item = task.Read<OrderInvoiceMultipalVendor>().SingleOrDefault();

                if (OrderInvoiceMultipalVendor.Item != null)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(OrderInvoiceMultipalVendor.Item.OrderObjectStr)))
                    {
                        OrderInvoiceMultipalVendor.Item.OrderObject = JsonConvert.DeserializeObject<List<OrderObject>>(Convert.ToString(OrderInvoiceMultipalVendor.Item.OrderObjectStr));
                    }

                    List<SalonObject> SalonObject = new List<SalonObject>();
                    SalonObject SalonObjectObj = new SalonObject();

                    if (!string.IsNullOrEmpty(Convert.ToString(OrderInvoiceMultipalVendor.Item.SalonObjectStr)))
                    {
                        OrderInvoiceMultipalVendor.Item.SalonObject = JsonConvert.DeserializeObject<List<SalonObject>>(Convert.ToString(OrderInvoiceMultipalVendor.Item.SalonObjectStr));
                    }
                }

            }

            return OrderInvoiceMultipalVendor;
        }

        //Order Invoice
        public override PagedList<AbstractOrderInvoice> OrderInvoice_All(PageParam pageParam, string search, long UserId, string InvoiceDate, string InvoiceNumber, long CustomerId)
        {
            PagedList<AbstractOrderInvoice> OrderInvoice = new PagedList<AbstractOrderInvoice>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CustomerId ", CustomerId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@InvoiceDate ", InvoiceDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@InvoiceNumber ", InvoiceNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderInvoice_All, param, commandType: CommandType.StoredProcedure);
                OrderInvoice.Values.AddRange(task.Read<OrderInvoice>());
                OrderInvoice.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return OrderInvoice;
        }

        public override PagedList<AbstractOrderInvoiceSalon> OrderInvoiceSalon_All(PageParam pageParam, string search, long UserId)
        {
            PagedList<AbstractOrderInvoiceSalon> OrderInvoiceSalon = new PagedList<AbstractOrderInvoiceSalon>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@UserId", UserId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderInvoiceSalon_All, param, commandType: CommandType.StoredProcedure);
                OrderInvoiceSalon.Values.AddRange(task.Read<OrderInvoiceSalon>());
                OrderInvoiceSalon.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return OrderInvoiceSalon;
        }

        public override SuccessResult<AbstractOrderInvoice> OrderInvoice_ById(long Id)
        {
            SuccessResult<AbstractOrderInvoice> OrderInvoice = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderInvoice_ById, param, commandType: CommandType.StoredProcedure);
                OrderInvoice = task.Read<SuccessResult<AbstractOrderInvoice>>().SingleOrDefault();
                OrderInvoice.Item = task.Read<OrderInvoice>().SingleOrDefault();

                if (OrderInvoice.Item != null)
                {
                    OrderInvoice.Item.OrderItemData = JsonConvert.DeserializeObject<List<OrderItemData>>(Convert.ToString(OrderInvoice.Item.OrderObjectStr));
                    OrderInvoice.Item.OrderSalonData = JsonConvert.DeserializeObject<List<OrderSalonData>>(Convert.ToString(OrderInvoice.Item.SalonObjectStr));
                }
            }
            return OrderInvoice;
        }

        public override SuccessResult<AbstractOrderReturnInvoice> OrderReturnInvoice_Update(AbstractOrderReturnInvoice abstractOrderReturnInvoice)
        {
            SuccessResult<AbstractOrderReturnInvoice> OrderReturnInvoice = null;
            var param = new DynamicParameters();

            param.Add("@Id", abstractOrderReturnInvoice.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@OrderObjectStr", abstractOrderReturnInvoice.OrderObjectStr, dbType: DbType.String, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderReturnInvoice_Update, param, commandType: CommandType.StoredProcedure);
                OrderReturnInvoice = task.Read<SuccessResult<AbstractOrderReturnInvoice>>().SingleOrDefault();
                OrderReturnInvoice.Item = task.Read<OrderReturnInvoice>().SingleOrDefault();
            }
            return OrderReturnInvoice;
        }
        public override SuccessResult<AbstractOrderInvoice> OrderReturnInvoice_ById(long Id)
        {
            SuccessResult<AbstractOrderInvoice> OrderInvoice = null;
            var param = new DynamicParameters();

            param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.OrderReturnInvoice_ById, param, commandType: CommandType.StoredProcedure);
                OrderInvoice = task.Read<SuccessResult<AbstractOrderInvoice>>().SingleOrDefault();
                OrderInvoice.Item = task.Read<OrderInvoice>().SingleOrDefault();
            }
            return OrderInvoice;
        }
        public override PagedList<AbstractOrderInvoice> SalonOrderInvoice_All(PageParam pageParam, string search, long SalonId, string InvoiceDate, string InvoiceNumber, long CustomerId)
        {
            PagedList<AbstractOrderInvoice> OrderInvoice = new PagedList<AbstractOrderInvoice>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@CustomerId ", CustomerId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@InvoiceDate ", InvoiceDate, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@InvoiceNumber ", InvoiceNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOrderInvoice_All, param, commandType: CommandType.StoredProcedure);
                OrderInvoice.Values.AddRange(task.Read<OrderInvoice>());
                OrderInvoice.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return OrderInvoice;
        }

        public override PagedList<AbstractOrderInvoiceSalon> SalonOrderVendor_All(PageParam pageParam, string search, long SalonId)
        {
            PagedList<AbstractOrderInvoiceSalon> OrderInvoiceSalon = new PagedList<AbstractOrderInvoiceSalon>();

            var param = new DynamicParameters();
            param.Add("@Offset", pageParam.Offset, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Limit", pageParam.Limit, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add("@Search", search, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add("@SalonId", SalonId, dbType: DbType.Int64, direction: ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Configurations.ConnectionString))
            {
                var task = con.QueryMultiple(SQLConfig.SalonOrderVendor_All, param, commandType: CommandType.StoredProcedure);
                OrderInvoiceSalon.Values.AddRange(task.Read<OrderInvoiceSalon>());
                OrderInvoiceSalon.TotalRecords = task.Read<long>().SingleOrDefault();
            }
            return OrderInvoiceSalon;
        }
    }
}
