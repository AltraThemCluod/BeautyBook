using BeautyBook.Common;
using System.Linq;

namespace BeautyBook.Pages
{
    public class Controllers
    {
        public const string Authentication = "Authentication";
        public const string Customers = "Customers";
        public const string Home = "Home";
        public const string Salons = "Salons";
        public const string Store = "Store";
        public const string SalonServices = "SalonServices";
        public const string Employee = "Employee";
	    public const string MyProfile = "MyProfile";
        public const string Appointments = "Appointments";
        public const string Packages = "Packages";
        public const string Offers = "Offers";
        public const string Marketing = "Marketing";
        public const string SmsPackage = "SmsPackage";
        public const string Email_Sms_Package = "Email_Sms_Package";
        public const string DailyDeals = "DailyDeals";
        public const string Payment = "Payment";
        public const string AppointmentInvoice = "AppointmentInvoice";
        public const string TechnicalSupport = "TechnicalSupport";
        public const string Pos = "Pos";
        public const string InventoryProduct = "InventoryProduct";
        public const string Invoices = "Invoices";


        public static string[] AdminAccess = { Home, Authentication, Customers, Salons, Store, SalonServices, Employee, MyProfile, Appointments, Packages, Offers , Marketing, SmsPackage, Email_Sms_Package, DailyDeals , Payment, AppointmentInvoice, TechnicalSupport, Pos , InventoryProduct , Invoices };
        public static string[] SalonOwnerAccess = { Salons, Home, Customers, Authentication, Store, SalonServices, Employee, MyProfile, Appointments, Packages, Offers, Marketing, SmsPackage, Email_Sms_Package, DailyDeals, Payment, AppointmentInvoice , TechnicalSupport, Pos , InventoryProduct, Invoices };
        //public static string[] EmployeeAccess = ProjectSession.EmployeePermissionController.Item.ConvertAll(x => x.Controller.ToString()).Distinct().ToArray();
        //public static string[] EmployeeAccessAction = ProjectSession.EmployeePermissionController.Item.ConvertAll(x => x.Action.ToString()).Distinct().ToArray();

        //public static string[] EmployeeAccess = { Salons, Home, Customers, Authentication, Store, SalonServices, Employee, MyProfile, Appointments, Packages, Offers, Marketing, SmsPackage, Email_Sms_Package, DailyDeals, Payment, AppointmentInvoice, TechnicalSupport, Pos };
        public static string[] CustomerAccess = {Home};
        public static string[] SellerAccess = {Home};
    }

}