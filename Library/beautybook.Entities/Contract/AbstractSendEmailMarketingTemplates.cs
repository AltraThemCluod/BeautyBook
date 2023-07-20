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
    public abstract class AbstractSendEmailMarketingTemplates
    {
        public long Id { get; set; }
        public long SalonId { get; set; }
        public long UserId { get; set; }
        public string EmailSubject { get; set; }
        public string EmailTemplateName { get; set; }
        public string UpdateHtmlTemplatesText { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerSinceStartDate { get; set; }
        public string CustomerSinceEndDate { get; set; }
        public long MinAppoinment { get; set; }
        public long MaxAppoinment { get; set; }
        public long MinAge { get; set; }
        public long MaxAge { get; set; }
        public string LastVisitStartDate { get; set; }
        public string LastVisitEndDate { get; set; }
        public string ServicesIds { get; set; }
        public string Email { get; set; }
        public long IsAllCustomer { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

