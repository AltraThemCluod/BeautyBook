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
    public abstract class AbstractUserAppointments
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ServiceCount { get; set; }
        public long SalonId { get; set; }
        public string Email { get; set; }
        public long AssignedToUserId { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public decimal Price { get; set; }
        public decimal Duration { get; set; }
        public string Comment { get; set; }
        public string ServicesIds { get; set; }
        public long CategoryId { get; set; }
        public long LookUpStatusId { get; set; }
        public DateTime LookUpStatusChangedDate { get; set; }
        public long LookUpStatusChangedBy { get; set; }
        public string CancellationCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public long DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerSecondName { get; set; }
        public string CustomerGender { get; set; }
        public string CustomerUsername { get; set; }
        public string AssignedToFirstname { get; set; }
        public string AssignedToSecondName { get; set; }
        public string AssignedToUsername { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerProfileUrl { get; set; }
        public string AssignedToProfile { get; set; }
        public string CustomerEmail { get; set; }
        public string AssignedToEmail { get; set; }
        public string CustomerAlternatePhone { get; set; }
        public string CustomerPrimaryPhone { get; set; }
        public string CustomerAddressLine1 { get; set; }
        public string ServicesIdsName { get; set; }
        public string LookUpStatusName { get; set; }
        //public string TotalPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ATotalDuration { get; set; }
        public string TotalDuration { get; set; }
        public string LastAppointmentDate { get; set; }
        public string LastAppointmentTime { get; set; }
        public long UserAppointmentId { get; set; }
        public string InvoiceNo { get; set; }
        public string SalonName { get; set; }
        public string SalonCommercialRegisterNo { get; set; }
        public string SalonLogoUrl { get; set; }
        public string SalonPrimaryPhone { get; set; }
        public string SalonAddressLine1 { get; set; }
        public string ServicesPrice { get; set; }
        public string ServicesTotalPrice { get; set; }
        public decimal Tax { get; set; }
        public string InvoicePrintDate { get; set; }  
        public string SalonTaxNumber { get; set; }
        public string ServicesObject { get; set; }
        public string CustomerName { get; set; }
        public long ParentId { get; set; }
        public string OldInvoiceNumber { get; set; }
        public List<AppoinmentServices> AppoinmentServices { get; set; }
        public long OrignalInvoiceNo { get;set; }

        public string ServiceDetailsStr { get; set; }

        public List<ServiceDetailsRoot> ServiceDetailsObj { get; set; }

        public string AppointmentServices { get; set; }

        public List<AppointmentServicesRoot> AppointmentServicesList { get; set; }

        public string QrCodeURL { get; set; }

        [NotMapped]
        public string CreatedDateStr => CreatedDate != null ? CreatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string UpdatedDateStr => UpdatedDate != null ? UpdatedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
        [NotMapped]
        public string DeletedDateStr => DeletedDate != null ? DeletedDate.ToString("dd-MMM-yyyy hh:mm tt") : "-";
    }
}

public class AppointmentServicesRoot
{
    public string ServiceName { get; set; }
    public string ServiceProvider { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}


// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class AppoinmentServices
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ServiceProvider { get; set; }
}

public class AbstractAppoinmentCustomers
{
    public long UserId { get; set; }
    public string CustomerName { get; set; }
}

public abstract class AbstractUserReturnInvoiceUpdate
{
    public long Id { get; set; }
    public string ServicesObject { get; set; }
    public decimal Tax { get; set; }
    public decimal ServicesTotalPrice { get; set; }
}


public abstract class AbstractAppointmentServiceDetails
{
    public long Id { get; set; }
    public long AppointmentId { get; set; }
    public long CategoryId { get; set; }
    public string ServiceId { get; set; }
    public long AssignedToUserId { get; set; }
    public decimal OldPrice { get; set; }
    public decimal Price { get; set; }
    public decimal Duration { get; set; }
    public string AssignedUserName { get; set; }
    public string AssignedUserProfile { get; set; }
    public string AssignedUserEmail { get; set; }
}

public class ServiceDetailsRoot
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public int CategoryId { get; set; }
    public string ServiceId { get; set; }
    public int AssignedToUserId { get; set; }
    public decimal OldPrice { get; set; }
    public decimal Price { get; set; }
    public decimal Duration { get; set; }
    public string AssignedUserName { get; set; }
    public string AssignedUserProfile { get; set; }
    public string AssignedUserEmail { get; set; }
}



// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class SD_Root
{
    public int sd_serviceDetailsId { get; set; }
    public int sd_categoryName { get; set; }
    public string sd_serviceName { get; set; }
    public int sd_userAssignto { get; set; }
    public decimal sd_appointmentsPrice { get; set; }
    public decimal sd_appointmentsDuration { get; set; }
}

