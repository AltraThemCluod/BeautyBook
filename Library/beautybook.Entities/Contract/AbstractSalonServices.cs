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
    public abstract class AbstractSalonServices
    {
        public long Id { get; set; }
        public long LookUpServicesId { get; set; }
        public long LookUpCategoryId { get; set; }
        public long SalonId { get; set; }
        public string UserServiceId { get; set; }
        public List<UserServices> UserServices { get; set; }
        public string Description { get; set; }
        public string MinDuration { get; set; }
        public string MaxDuration { get; set; }
        public long MinPrice { get; set; }
        public long MaxPrice { get; set; }
        public string UserId { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public long LookUpStatusId { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }
        public string LookUpServicesName { get; set; }
        public string LookUpCategoryName { get; set; }
        public string ServicePhoto { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

