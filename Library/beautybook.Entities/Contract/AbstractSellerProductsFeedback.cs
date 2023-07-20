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
    public abstract class AbstractSellerProductsFeedback
    {
        public long Id { get; set; }
        public long FeedbackByUserId { get; set; }
        public string SellerProducts { get; set; }
        public float Rating { get; set; }
        public string Review { get; set; }
        public long LookUpStatusId { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }
        public string UserName  { get; set;}
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public long UpdatedBy { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdateDateStr => UpdateDate != null ? UpdateDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

