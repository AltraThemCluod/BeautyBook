using BeautyBook;
using BeautyBook.Entities.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BeautyBook.Entities.Contract
{
    public abstract class AbstractSalonVendorDashboard
    {
        public long VendorId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public long Type { get; set; }
    }

    public abstract class AbstractVendorSalesAndRating
    {
        public long TotalSales { get; set; }
        public decimal AverageRating { get; set; }
    }

    public abstract class AbstractVendorTopCustomer
    {
        public long SalonId { get; set; }
        public string SalonName { get; set; }
        public string SalonOrderCount { get; set; }
        public decimal TotalSales { get; set; }
        public string CustomerCount { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
    }

    public abstract class AbstractVendorTopProduct
    {
        public long Id { get; set; }
        public long VendorId { get; set; }
        public long Type { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ProductThumbnailImage { get; set; }
        public string ProductName { get; set; }
        public decimal ProductAverageRating { get; set; }
        public long ProductStarCount { get; set; }
        public decimal Price { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal OfferPercentage { get; set; }
        public long SalesCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Date { get; set; }

    }

}


