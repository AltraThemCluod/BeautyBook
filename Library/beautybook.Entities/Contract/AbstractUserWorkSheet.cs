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
    public abstract class AbstractUserWorkSheet
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long SalonId { get; set; }
        public long LookUpStatusId { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string Break { get; set; }
        public string AttendanceDate { get; set; }
        public string ShortLeave { get; set; }
        public string Productive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }    
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string SecondName { get; set; }
        public string LookUpEmployeeRoles { get; set; }
        public string LookUpStatus { get; set; }
        public long LookUpEmployeeRolesId { get; set; }
        public List<UserSalons> UserSalons { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

