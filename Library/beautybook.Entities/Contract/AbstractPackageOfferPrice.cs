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
    public abstract class AbstractPackageOfferPrice
    {
        public long Id { get; set; }
        public long OfferId { get; set; }
        public long PackagesId { get; set; }
        public decimal PackagesOfferPrice { get; set; }
        public long IsType { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        
    }
}

