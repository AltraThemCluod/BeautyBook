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
    public abstract class AbstractUserLeave
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long SalonId { get; set; }
        public long LookUpLeaveTypeId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int NoOfDays { get; set; }
        public string Reason { get; set; }
        public long LookUpStatusId { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string LookUpEmployeeRoles { get; set; }
        public string LeaveType { get; set; }
        public string LookUpStatus { get; set; }
        public long LookUpEmployeeRolesId { get; set; }
        public string SecondName { get; set; }
        public string LookUpLeaveType { get; set; }
        public string LeaveTypeCount { get; set; }

        public long EmployeesId { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

