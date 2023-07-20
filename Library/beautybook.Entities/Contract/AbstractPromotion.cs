using BeautyBook.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBook.Entities.Contract
{
    public abstract class AbstractPromotion
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }

        public string ProductTypeName { get; set; }

        public int ProductBrandId { get; set; }

        public string ProductBrandName { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string Price { get; set; }

        public decimal OriginalPrice { get; set; }
        public decimal OfferPrice { get; set; }





        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }


        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MM-yyyy hh:mm tt") : "-";

      



    }
}
