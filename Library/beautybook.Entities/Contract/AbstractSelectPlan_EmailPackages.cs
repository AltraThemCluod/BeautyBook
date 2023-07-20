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
    public abstract class AbstractSelectPlan_EmailPackages
    {
        public int Id { get; set; }
        public long SalonId { get; set; }
        public long EmailPackagesId { get; set; }
        public long CompleteNoOfMsg { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public long NoOfMessages { get; set; }
        public string EmailMsgPackagesName { get; set; }
        public long ExpiredPlanDay { get; set; }
        public long SendEmailCount { get; set; }
        public DateTime PackageExpiryDate { get; set; }
        public bool IsExpiry { get; set; }
        public bool IsPayment { get; set; }
        public decimal EmailMsgPackagesPrice { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";

    }
}
