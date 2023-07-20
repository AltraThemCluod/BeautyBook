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
    public abstract class AbstractSmsMarketing
    {
        public long Id { get; set; }
        public long SalonId { get; set; }
        public string SmsSubject { get; set; }
        public long Gender { get; set; }
        public string CustomerSinceStart { get; set; }
        public string CustomerSinceEnd { get; set; }
        public string LastVisitStart { get; set; }
        public string LastVisitEnd { get; set; }
        public long MinAppoinment { get; set; }
        public long MaxAppoinment { get; set; }
        public long MinYear { get; set; }
        public long MaxYear { get; set; }
        public string ServicesId { get; set; }
        public string SmsTemplate { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string UserFullName { get; set; }
        public string ToSMS { get; set; }
        public string Body { get; set; }
        public long SMSMarketingId { get; set; }
        public string UserIds { get; set; }
        public long NoOfSMSCount { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

