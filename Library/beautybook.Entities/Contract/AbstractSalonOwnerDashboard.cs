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
    public abstract class AbstractSalonOwnerDashboard
    {
        public long SalonId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ServiceIds { get; set; }
        
    }

    public abstract class AbstractSalonOwnerSMSandEmailPackages
    {
        public string EmailCompletedMessage { get; set; }
        public string EmailUnCompleteMessage { get; set; }
        public string SMSCompletedMessage { get; set; }
        public string SMSUnCompleteMessage { get; set; }
    }

    public abstract class AbstractSalonOwnerNewCustomer
    {
        public long Id { get; set; }
        public long AssignedToUserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ProfileUrl { get; set; }
        public long LookUpCountryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Country { get; set; }
        public long UserCount { get; set; }
        public decimal Price { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public long Type { get; set; }
    }

    public abstract class AbstractSalonOwnerTopRequestedServices
    {
        public long TopRequestedServices { get; set; }
        public string Name { get; set; }
        public long ServiceCount { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public long Type { get; set; }
        public long Services { get; set; }
    }

    public abstract class AbstractSalonOwnerArray
    {
        public long UserCount { get; set; }
        public decimal Price { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public long Type { get; set; }
        public string Date { get; set; }
    }
    public abstract class AbstractTecnicalSupport
    {
        public long Id { get; set; }
        public string SalonName { get; set; }
        public string LogoUrl { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string City { get; set; }
        public long ToDaysAppointments { get; set; }
        public string CurrentDate { get; set; }
        public string CurrentTime { get; set; }
        public string CurrentDay { get; set; }
    }
}


