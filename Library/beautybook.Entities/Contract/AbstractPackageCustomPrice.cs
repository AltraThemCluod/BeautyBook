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
    public abstract class AbstractPackageCustomPrice
    {
        public long Id { get; set; }
        public string CustomPrice { get; set; }
        public long SalonId { get; set; }
        public long PackageId { get; set; }
        public long LookUpServiceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        
    }
}

