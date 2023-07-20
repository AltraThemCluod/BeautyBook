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
    public abstract class AbstractOrderStatusTracking
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long LookUpStatusId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LookUpStatus { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

