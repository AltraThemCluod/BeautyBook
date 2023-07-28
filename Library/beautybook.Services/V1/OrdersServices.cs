using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautyBook.Common;
using BeautyBook.Common.Paging;
using BeautyBook.Data.Contract;
using BeautyBook.Entities.Contract;
using BeautyBook.Services.Contract;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

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
            //GeneratePDF();

            var response = this.abstractOrdersDao.Orders_PaymentComplete(Id);

            EmailHelper.PurchaseProduct(response.Item.SalonName, response.Item.PrimaryPhone, response.Item.AddressLine1, "Imageurl" , "احمر شفاه سائل مطفي من هدى بيوتي - ثعلبة" , 80);

            return response;
        }

        public void GeneratePDF()
        {
            string dynamicHtml = "<!DOCTYPE html><html><head><title>Dynamic PDF</title></head><body><h1>Hello, PDF!</h1><p>This is a dynamically generated PDF using iTextSharp in C#.</p></body></html>";

            Document pdfDoc = new Document(PageSize.A4);

            string outputFilePath = "E:\\PDF\\dynamic_output.pdf";

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(outputFilePath, FileMode.Create));

            pdfDoc.Open();

            using (var sr = new StringReader(dynamicHtml))
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            }

            pdfDoc.Close();

            //EmailHelper.PurchaseProductEmail("amiparakartik@gmail.com" , outputFilePath);
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