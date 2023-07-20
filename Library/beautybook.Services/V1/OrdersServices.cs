using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;

namespace BeautyBook.Services.V1
{
    public class OrdersServices : AbstractOrdersServices
    {
        private AbstractOrdersDao abstractOrdersDao;

        public OrdersServices(AbstractOrdersDao abstractOrdersDao)
        {
            this.abstractOrdersDao = abstractOrdersDao;
        }
        public override PagedList<AbstractOrders> Orders_All(PageParam pageParam, string search, long LookUpStatusId, string OrderNo, string DateOfOrder, long SalonId,long VendorId)
        {
            return this.abstractOrdersDao.Orders_All(pageParam, search, LookUpStatusId, OrderNo, DateOfOrder, SalonId, VendorId);
        }
        public override PagedList<AbstractOrders> SalonOrder_All(PageParam pageParam, string search, long SalonId)
        {
            return this.abstractOrdersDao.SalonOrder_All(pageParam, search, SalonId);
        }
        public override SuccessResult<AbstractOrders> Orders_ChangeStatus(long Id,long LookUpStatusId,string Comment)
        {
            return this.abstractOrdersDao.Orders_ChangeStatus(Id, LookUpStatusId, Comment);
        }        
        public override SuccessResult<AbstractOrders> Orders_ById(long Id)
        {
            return this.abstractOrdersDao.Orders_ById(Id);
        }

        public override SuccessResult<AbstractOrders> Orders_PaymentComplete(long Id)
        {
            return this.abstractOrdersDao.Orders_PaymentComplete(Id);
        }

        public override SuccessResult<AbstractOrders> Orders_Upsert(AbstractOrders abstractOrders)
        {
            return this.abstractOrdersDao.Orders_Upsert(abstractOrders);
        }
        public override SuccessResult<AbstractOrders> Orders_UpdateCheckoutDetails(AbstractOrders abstractOrders)
        {
            return this.abstractOrdersDao.Orders_UpdateCheckoutDetails(abstractOrders);
        }
        public override SuccessResult<AbstractOrders> Orders_UpdatePaymentMethod(AbstractOrders abstractOrders)
        {
            return this.abstractOrdersDao.Orders_UpdatePaymentMethod(abstractOrders);
        }
        public override SuccessResult<AbstractOrders> Orders_Delete(long Id, long DeletedBy)
        {
            return this.abstractOrdersDao.Orders_Delete(Id, DeletedBy);
        }

        //Invoice
        public override PagedList<AbstractOrderInvoiceMultipalVendor> OrderInvoiceMultipalVendor_All(PageParam pageParam, string search, long OrderId)
        {
            return this.abstractOrdersDao.OrderInvoiceMultipalVendor_All(pageParam, search, OrderId);
        }
        public override SuccessResult<AbstractOrderInvoiceMultipalVendor> OrderInvoiceMultipalVendor_ById(long OrderId, long UserId)
        {
            return this.abstractOrdersDao.OrderInvoiceMultipalVendor_ById(OrderId, UserId);
        }

        //Order Invoice
        public override PagedList<AbstractOrderInvoice> OrderInvoice_All(PageParam pageParam, string search, long UserId, string InvoiceDate, string InvoiceNumber, long CustomerId)
        {
            return this.abstractOrdersDao.OrderInvoice_All(pageParam, search, UserId, InvoiceDate, InvoiceNumber, CustomerId);
        }
        public override PagedList<AbstractOrderInvoiceSalon> OrderInvoiceSalon_All(PageParam pageParam, string search, long UserId)
        {
            return this.abstractOrdersDao.OrderInvoiceSalon_All(pageParam, search, UserId);
        }
        public override SuccessResult<AbstractOrderInvoice> OrderInvoice_ById(long Id)
        {
            return this.abstractOrdersDao.OrderInvoice_ById(Id);
        }
        public override SuccessResult<AbstractOrderReturnInvoice> OrderReturnInvoice_Update(AbstractOrderReturnInvoice abstractOrderReturnInvoice)
        {
            return this.abstractOrdersDao.OrderReturnInvoice_Update(abstractOrderReturnInvoice);
        }
        public override SuccessResult<AbstractOrderInvoice> OrderReturnInvoice_ById(long Id)
        {
            return this.abstractOrdersDao.OrderReturnInvoice_ById(Id);
        }
        public override PagedList<AbstractOrderInvoice> SalonOrderInvoice_All(PageParam pageParam, string search, long SalonId, string InvoiceDate, string InvoiceNumber, long CustomerId)
        {
            return this.abstractOrdersDao.SalonOrderInvoice_All(pageParam, search, SalonId, InvoiceDate, InvoiceNumber, CustomerId);
        }
        public override PagedList<AbstractOrderInvoiceSalon> SalonOrderVendor_All(PageParam pageParam, string search, long UserId)
        {
            return this.abstractOrdersDao.SalonOrderVendor_All(pageParam, search, UserId);
        }
    }
}