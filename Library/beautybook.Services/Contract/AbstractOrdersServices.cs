using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Entities.Contract;

namespace BeautyBook.Services.Contract
{
    public abstract class AbstractOrdersServices
    {
        public abstract PagedList<AbstractOrders> Orders_All(PageParam pageParam, string search, long LookUpStatusId, string OrderNo, string DateOfOrder, long SalonId,long VendorId);
        public abstract PagedList<AbstractOrders> SalonOrder_All(PageParam pageParam, string search, long SalonId);
        public abstract SuccessResult<AbstractOrders> Orders_ById(long Id);
        public abstract SuccessResult<AbstractOrders> Orders_PaymentComplete(long Id);
        public abstract SuccessResult<AbstractOrders> Orders_Upsert(AbstractOrders abstractOrders);
        public abstract SuccessResult<AbstractOrders> Orders_UpdateCheckoutDetails(AbstractOrders abstractOrders);
        public abstract SuccessResult<AbstractOrders> Orders_UpdatePaymentMethod(AbstractOrders abstractOrders);
        public abstract SuccessResult<AbstractOrders> Orders_ChangeStatus(long Id, long LookUpStatusId,string Comment);
        public abstract SuccessResult<AbstractOrders> Orders_Delete(long Id, long DeletedBy);

        //Invoice
        public abstract PagedList<AbstractOrderInvoiceMultipalVendor> OrderInvoiceMultipalVendor_All(PageParam pageParam, string Search, long OrderId);
        public abstract SuccessResult<AbstractOrderInvoiceMultipalVendor> OrderInvoiceMultipalVendor_ById(long OrderId, long UserId);

        //Invoice Order
        public abstract PagedList<AbstractOrderInvoice> OrderInvoice_All(PageParam pageParam, string Search, long UserId, string InvoiceDate, string InvoiceNumber, long CustomerId);
        public abstract PagedList<AbstractOrderInvoiceSalon> OrderInvoiceSalon_All(PageParam pageParam, string Search, long UserId);
        public abstract SuccessResult<AbstractOrderInvoice> OrderInvoice_ById(long Id);
        public abstract SuccessResult<AbstractOrderInvoice> OrderReturnInvoice_ById(long Id);
        public abstract SuccessResult<AbstractOrderReturnInvoice> OrderReturnInvoice_Update(AbstractOrderReturnInvoice abstractOrderReturnInvoice);
        public abstract PagedList<AbstractOrderInvoice> SalonOrderInvoice_All(PageParam pageParam, string Search, long SalonId, string InvoiceDate, string InvoiceNumber, long CustomerId);
        public abstract PagedList<AbstractOrderInvoiceSalon> SalonOrderVendor_All(PageParam pageParam, string Search, long SalonId);
    }
}
