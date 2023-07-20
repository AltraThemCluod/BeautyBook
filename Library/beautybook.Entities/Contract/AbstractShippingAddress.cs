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
    public abstract class AbstractShippingAddress
    {
        public long Id { get; set; }
        public long SalonId { get; set; }
        public string Address { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public string City { get; set; }
        public long  ZipCode { get; set; }
        public string PrimaryNumber { get; set; }
        public string AlternateNumber { get; set; }
        public bool IsDefault { get; set; }
        public long CreatedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string SalonName { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }


        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

