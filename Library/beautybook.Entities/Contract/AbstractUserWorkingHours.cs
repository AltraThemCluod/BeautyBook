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
    public abstract class AbstractUserWorkingHours
    {
        
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Day { get; set; }
        public string ArabicDay { get; set; }
        public long LookUpStatusId { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }


        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

