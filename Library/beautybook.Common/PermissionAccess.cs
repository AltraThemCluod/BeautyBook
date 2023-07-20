using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBook.Common
{
    public class PermissionAccess
    {
        public PermissionAccess()
        {
            this.admin = new Admin();
            this.subadmin = new SubAdmin();
            this.aggregator = new Aggregator();      
        }

        public Admin admin;
        public SubAdmin subadmin;
        public Aggregator aggregator;
        
        public class Admin
        {
            public bool Dashboard_Dashboard_TotalCheckInToday { get; set; }
            public bool Dashboard_Dashboard_TotalOrdersToday { get; set; }
            public bool Dashboard_Dashboard_TotalAggregatorsToday { get; set; }
            public bool Dashboard_Dashboard_TotalExpenseToday { get; set; }
            public bool Dashboard_Dashboard_TotalLeaveToday { get; set; }
            public bool Dashboard_Dashboard_TotalDepositsToday { get; set; }
            public bool Dashboard_Dashboard_AllowViewAccess { get; set; }
        }

        public class SubAdmin
        {
            public bool Departments_Departments_AllowViewAccess { get; set; }
            public bool Departments_Manage_AddEditDepartments { get; set; }
            public bool Designations_Designations_AllowViewAccess { get; set; }
            public bool Designations_Manage_AddEditDesignations { get; set; }
            public bool Designations_DesignationPermissionList_AddEditPermissions { get; set; }
            public bool States_States_AllowViewAccess { get; set; }
            public bool States_Manage_AddEditStates { get; set; }
            public bool ExpenseType_ExpenseType_AllowViewAccess { get; set; }
            public bool ExpenseType_Manage_AddEditExpenseType { get; set; }
            public bool CMS_CMS_AllowViewAccess { get; set; }
            public bool CMS_Manage_AddEditEMS { get; set; }
            public bool Target_Target_AllowViewAccess { get; set; }
            public bool Target_Manage_AddEditTarget { get; set; }
            public bool Dashboard_Dashboard_TodaysDistanceTravelled { get; set; }
            public bool Dashboard_Dashboard_TotalExpenseAddedToday { get; set; }
            public bool Dashboard_Dashboard_TodaysTarget { get; set; }
            public bool Dashboard_Dashboard_MonthlyTarget { get; set; }
            public bool SubAdmins_SubAdmins_AllowViewAccess { get; set; }
            public bool SubAdmins_Manage_AddEditSubAdmins { get; set; }
            public bool TargetDetails_TargetDetails_AllowViewAccess { get; set; }
            public bool TargetDetails_Manage_AddEditTargetDetails { get; set; }
            public bool Expense_Expense_AllowViewAccess { get; set; }
            public bool Expense_Manage_AddEditExpense { get; set; }
            public bool Leaves_Leaves_AllowViewAccess { get; set; }
            public bool Leaves_Manage_AddEditLeaves { get; set; }
            public bool Banks_Banks_AllowViewAccess { get; set; }
            public bool Banks_Manage_AddEditBanks { get; set; }
            public bool ToDos_ToDos_AllowViewAccess { get; set; }
            public bool Attendance_Attendance_AllowViewAccess { get; set; }
            public bool Aggregators_Aggregators_AllowViewAccess { get; set; }
            public bool MyStaffPosition_MyStaffPosition_AllowViewAccess { get; set; }
            public bool MyTeamDailyReports_MyTeamDailyReports_AllowViewAccess { get; set; }
            public bool Account_ResetPSW_AddEditResetPSW { get; set; }
            public bool Account_MyAccount_AddEditMyAccount { get; set; }
            public bool Admin_Admin_AllowViewAccess { get; set; }
            public bool Dashboard_Dashboard_AllowViewAccess { get; set; }
            public bool Aggregator_Aggregator_UpdateStatus { get; set; }
            public bool Master_Master_AllowViewAccess { get; set; }
            public bool Staff_Staff_AllowViewAccess { get; set; }
            public bool MyTeamPerformance_MyTeamPerformance_AllowViewAccess { get; set; }
            public bool RefferedSeller_RefferedSeller_AllowViewAccess { get; set; }
            public bool TalukawiseReport_TalukawiseReport_AllowViewAccess { get; set; }
            public bool SubAdminBankReport_SubAdminBankReport_AllowViewAccess { get; set; }
            public bool UpcomingProduct_UpcomingProduct_AllowViewAccess { get; set; }
            public bool UpcomingProduct_Manage_AddEditUpcomingProduct { get; set; }
            public bool Customer_Customer_AllowViewAccess { get; set; }
            public bool Customer_Manage_AddEditCustomer { get; set; }
            public bool PersonalInformation_PersonalInformation_AllowViewAccess { get; set; }
            public bool Reports_Reports_AllowViewAccess { get; set; }

        }

        public class Aggregator
        {
            public bool Dashboard_Dashboard_AvailableCredit { get; set; }
            public bool Dashboard_Dashboard_Profit { get; set; }
            public bool Dashboard_Dashboard_Orders { get; set; }
            public bool Dashboard_Dashboard_ONLINEUSERS { get; set; }
            public bool Customer_Customer_AllowViewAccess { get; set; }
            public bool Customer_Manage_AddEditCustomer { get; set; }
            public bool SubAggregator_SubAggregator_AllowViewAccess { get; set; }
            public bool SubAggregator_Manage_AddEditSubAggregator { get; set; }
            public bool Orders_Orders_AllowViewAccess { get; set; }
            public bool Payments_Payments_AllowViewAccess { get; set; }
            public bool Payments_Manage_AddEditPayments { get; set; }
            public bool Transaction_Transaction_AllowViewAccess { get; set; }
            public bool Banks_Banks_AllowViewAccess { get; set; }
            public bool Banks_Manage_AddEditBanks { get; set; }
            public bool Manufacturers_Manufacturers_AllowViewAccess { get; set; }
            public bool Manufacturers_Manage_AddEditManufacturers { get; set; }
            public bool AggregatorProfile_Register_AddEditBusinessInformation { get; set; }
            public bool AggregatorProfile_PersonalInformation_AddEditPersonalInformation { get; set; }
        }

    }
}
