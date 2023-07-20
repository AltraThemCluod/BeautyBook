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
    public abstract class AbstractSelectPlan_SMSPackages
    {
        public long Id { get; set; }
        public long SalonId { get; set; }

        public long SMSPackagesId { get; set; }
        public long CompleteNoOfMsg { get; set; }
        public long NoOfMessages { get; set; }
        public long SendSMSCount { get; set; }
        public long IsSms { get; set; }
        public long ExpiredPlanDay { get; set; }

        public string SMSPackagesName { get; set; }

        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public decimal SMSPackagesPrice { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";
       

    }
}
