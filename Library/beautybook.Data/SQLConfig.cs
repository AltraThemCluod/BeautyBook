//-----------------------------------------------------------------------
// <copyright file="SQLConfig.cs" company="Rushkar">
//     Copyright Rushkar. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Data
{
    /// <summary>
    /// SQL configuration class which holds stored procedure name.
    /// </summary>
    internal sealed class SQLConfig
    {
        #region LookUpServices
        public const string LookUpServices_All = "LookUpServices_All";
        public const string CustomCategory_All = "CustomCategory_All";
        public const string CustomServices_All = "CustomServices_All";
        public const string LookUpServices_ParentId = "LookUpServices_ParentId";
        public const string LookUpServices_ById = "LookUpServices_ById";
        public const string LookUpServices_Upsert = "LookUpServices_Upsert";
        public const string LookUpServices_ActInact = "LookUpServices_ActInact";
        public const string LookUpServices_Delete = "LookUpServices_Delete";
        public const string LookUpServices_AllServices = "LookUpServices_AllServices";
        #endregion
        #region Salons
        public const string Salons_All = "Salons_All";
        public const string Salons_ById = "Salons_ById";
        public const string Salons_ByUserId = "Salons_ByUserId";
        public const string Salons_Upsert = "Salons_Upsert";
        public const string Salons_ActInact = "Salons_ActInact";
        public const string Salons_ApprovedUnApproved = "Salons_ApprovedUnApproved";
        public const string Salons_Delete = "Salons_Delete";
        #endregion
        #region SalonServices
        public const string SalonServices_All = "SalonServices_All";
        public const string SalonServices_ById = "SalonServices_ById";
        public const string SalonServices_Upsert = "SalonServices_Upsert";
        public const string SalonServices_ActInact = "SalonServices_ActInact";
        public const string SalonServices_Delete = "SalonServices_Delete";
        public const string BlankSalonServices_Upsert = "BlankSalonServices_Upsert";
        #endregion
        #region Users
        public const string Users_All = "Users_All";
        public const string Users_ById = "Users_ById";
        public const string Users_ActInact= "Users_ActInact";
        public const string Users_Login = "Users_Login";
        public const string Users_MobileLogout = "Users_MobileLogout";
        public const string Users_ResetPassword = "Users_ResetPassword";
        public const string Users_Upsert = "Users_Upsert";
        public const string Users_VerifyEmail = "Users_VerifyEmail";
        public const string Users_Logout = "Users_Logout";
        public const string Users_Delete = "Users_Delete";
        public const string Users_VerifyCode = "Users_VerifyCode";
        public const string User_ByEmail = "User_ByEmail";
        public const string Employee_IsCreate = "Employee_IsCreate";
        public const string BlankUsers_Create = "BlankUsers_Create";
        #endregion
        #region LookUpCountry
        public const string LookUpCountry_All = "LookUpCountry_All";
        #endregion
        #region LookUpEmployeeRoles
            public const string LookUpEmployeeRoles_All = "LookUpEmployeeRoles_All";
        #endregion
        #region LookUpEmployeeType
            public const string LookUpEmployeeType_All = "LookUpEmployeeType_All";
        #endregion
        #region LookUpLeaveType
            public const string LookUpLeaveType_All = "LookUpLeaveType_All";
        #endregion
        #region LookUpPaymentMethod
            public const string LookUpPaymentMethod_All = "LookUpPaymentMethod_All";
        #endregion
        #region LookUpState
            public const string LookUpState_All = "LookUpState_All";
        #endregion
        #region LookUpStatus
            public const string LookUpStatus_All = "LookUpStatus_All";
        #endregion
        #region LookUpUserType
            public const string LookUpUserType_All = "LookUpUserType_All";
        #endregion
        #region SellerProductsFeedback
            public const string SellerProductsFeedback_All = "SellerProductsFeedback_All";
            public const string SellerProductsFeedback_ById = "SellerProductsFeedback_ById";
            public const string SellerProductsFeedback_Upsert = "SellerProductsFeedback_Upsert";
            public const string SellerProductsFeedback_ChangeStatus = "SellerProductsFeedback_ChangeStatus";
        #endregion
        #region UserServices
            public const string UserServices_SalonServicesById = "UserServices_SalonServicesById";
            public const string UserServices_Upsert = "UserServices_Upsert";
            public const string UserServices_Delete = "UserServices_Delete";
        #endregion
        #region UserWorkingHours
            public const string UserWorkingHours_ByUserId = "UserWorkingHours_ByUserId";
            public const string EmployeeWorkingHour_ServicesId = "EmployeeWorkingHour_ServicesId";
        #endregion
        #region UserAppointments
            public const string UserAppointments_ById = "UserAppointments_ById";
            public const string UserAppointmentsInvoice_ById = "UserAppointmentsInvoice_ById";
            public const string AppoinmentInvoice_ById = "AppoinmentInvoice_ById";
            public const string UserAppointments_ChangeStatus = "UserAppointments_ChangeStatus";
            public const string UserAppointments_All = "UserAppointments_All";
            public const string UserAppointments_Upsert = "UserAppointments_Upsert";
            public const string UserAppointments_Delete = "UserAppointments_Delete";
            public const string UserAppointments_GetAssignTo = "UserAppointments_GetAssignTo";
            public const string SalonInvoice_CustomersAll = "SalonInvoice_CustomersAll";
            public const string SalonInvoice_All = "SalonInvoice_All";
            public const string UserReturnInvoice_Update = "UserReturnInvoice_Update";
            public const string UserReturnInvoice_ById = "UserReturnInvoice_ById";
            public const string AppointmentServiceDetails_Upsert = "AppointmentServiceDetails_Upsert";
            public const string BlankAppointment_Create = "BlankAppointment_Create";
            public const string AppointmentServiceDetails_Delete = "AppointmentServiceDetails_Delete";
        #endregion

        #region UserLeave
        public const string UserLeave_ById = "UserLeave_ById";
        public const string UserLeave_ChangeStatus = "UserLeave_ChangeStatus";
        public const string UserLeave_All = "UserLeave_All";
        public const string UserLeave_Upsert = "UserLeave_Upsert";
        public const string UserLeave_Delete = "UserLeave_Delete";
        public const string UserLeave_LeaveTypeCount = "UserLeave_LeaveTypeCount";
        #endregion

        #region UserWorkSheet
        public const string UserWorkSheet_ById = "UserWorkSheet_ById";
        public const string UserWorkSheet_All = "UserWorkSheet_All";
        public const string UserWorkSheet_Upsert = "UserWorkSheet_Upsert";
        #endregion

        #region Admin
        public const string Admin_ActInact = "Admin_ActInact";
        public const string Admin_All = "Admin_All";
        public const string Admin_ById = "Admin_ById";
        public const string Admin_ChangePassword = "Admin_ChangePassword";
        public const string Admin_Login = "Admin_Login";
        public const string Admin_Logout = "Admin_Logout";
        public const string Admin_Upsert = "Admin_Upsert";
        #endregion

        #region Product
        public const string Product_ChangeStatus = "Product_ChangeStatus";
        public const string Product_All = "Product_All";
        public const string Product_ById = "Product_ById";
        public const string Product_Delete = "Product_Delete";
        public const string Product_Upsert = "Product_Upsert";
        public const string Product_UpdateQty = "Product_UpdateQty";
        public const string VendorProduct_ByVendorId = "VendorProduct_ByVendorId";
        #endregion

        public const string MasterProductBrand_All = "MasterProductBrand_All";
        public const string MasterProductBrand_ById = "MasterProductBrand_ById";
        public const string MasterProductBrand_Upsert = "MasterProductBrand_Upsert";
        public const string MasterProductBrand_Delete = "MasterProductBrand_Delete";
        public const string MasterProductBrand_ActInAct = "MasterProductBrand_ActInAct";


        public const string MasterProductType_All = "MasterProductType_All";
        public const string MasterProductType_ById = "MasterProductType_ById";
        public const string MasterProductType_Upsert = "MasterProductType_Upsert";
        public const string MasterProductType_Delete = "MasterProductType_Delete";
        public const string MasterProductType_ActInAct = "MasterProductType_ActInAct";

        public const string MasterProductWeight_All = "MasterProductWeight_All";
        public const string MasterProductWeight_ById = "MasterProductWeight_ById";
        public const string MasterProductWeight_Upsert = "MasterProductWeight_Upsert";
        public const string MasterProductWeight_Delete = "MasterProductWeight_Delete";
        public const string MasterProductWeight_ActInAct = "MasterProductWeight_ActInAct";


        public const string MasterOrderStatus_All = "MasterOrderStatus_All";
        public const string MasterOrderStatus_ById = "MasterOrderStatus_ById";
        public const string MasterOrderStatus_Upsert = "MasterOrderStatus_Upsert";
        public const string MasterOrderStatus_Delete = "MasterOrderStatus_Delete";
        public const string MasterOrderStatus_ActInAct = "MasterOrderStatus_ActInAct";

        #region Orders
        public const string Orders_All = "Orders_All";
        public const string SalonOrder_All = "SalonOrder_All";
        public const string Orders_ById = "Orders_ById";
        public const string Orders_Upsert = "Orders_Upsert";
        public const string Orders_UpdateCheckoutDetails = "Orders_UpdateCheckoutDetails";
        public const string Orders_UpdatePaymentMethod = "Orders_UpdatePaymentMethod";
        public const string Orders_Delete = "Orders_Delete";
        public const string Orders_ChangeStatus = "Orders_ChangeStatus";
        public const string Orders_PaymentComplete = "Orders_PaymentComplete";
        public const string OrderInvoiceMultipalVendor_All = "OrderInvoiceMultipalVendor_All";
        public const string OrderInvoiceMultipalVendor_ById = "OrderInvoiceMultipalVendor_ById";
        public const string OrderInvoice_All = "OrderInvoice_All";
        public const string OrderInvoiceSalon_All = "OrderInvoiceSalon_All";
        public const string OrderInvoice_ById = "OrderInvoice_ById";
        public const string OrderReturnInvoice_ById = "OrderReturnInvoice_ById";
        public const string OrderReturnInvoice_Update = "OrderReturnInvoice_Update";
        public const string SalonOrderInvoice_All = "SalonOrderInvoice_All";
        public const string SalonOrderVendor_All = "SalonOrderVendor_All";
        #endregion

        #region OrderProducts
        public const string OrderProducts_All = "OrderProducts_All";
        public const string OrderProducts_Upsert = "OrderProducts_Upsert";
        public const string OrderProducts_Delete = "OrderProducts_Delete";
        public const string SalonOwnerOrdersCount_VendorId = "SalonOwnerOrdersCount_VendorId";
        public const string SalonOwnerOrdersCount_UpdateStatus = "SalonOwnerOrdersCount_UpdateStatus";
        #endregion

        public const string WriteaReview_Upsert = "WriteaReview_Upsert";
        public const string WriteaReview_All = "WriteaReview_All";
        public const string ProductImage_Upsert = "ProductImage_Upsert";

        #region MasterProductCategory
        public const string MasterProductCategory_Upsert = "MasterProductCategory_Upsert";
        public const string MasterProductCategory_All = "MasterProductCategory_All";
        public const string MasterProductCategory_ById = "MasterProductCategory_ById";
        public const string MasterProductCategory_Delete = "MasterProductCategory_Delete";
        public const string MasterProductCategory_ActInact = "MasterProductCategory_ActInact";
        #endregion

        public const string OrderStatusTracking_ByOrderId = "OrderStatusTracking_ByOrderId";
        public const string OrderStatusTracking_Upsert = "OrderStatusTracking_Upsert";
        #region Promotion
        public const string Promotion_All = "Promotion_All";
        public const string Promotion_Upsert = "Promotion_Upsert";
        public const string Promotion_ById = "Promotion_ById";
        #endregion


        #region AddToCart
        public const string AddToCart_All = "AddToCart_All";
        public const string AddToCart_Upsert = "AddToCart_Upsert";
        public const string AddToCart_UpdateQty = "AddToCart_UpdateQty";
        public const string AddToCart_Delete = "AddToCart_Delete";
        #endregion
        #region VendorApprovel
        public const string VendorApprovel_All = "VendorApprovel_All";
        public const string VendorApprovel_IsApproved = "VendorApprovel_IsApproved";
        #endregion

        #region ShippingAddress
        public const string ShippingAddress_All = "ShippingAddress_All";
        public const string ShippingAddress_Upsert = "ShippingAddress_Upsert";
        public const string ShippingAddress_Delete = "ShippingAddress_Delete";
        public const string ShippingAddress_ById = "ShippingAddress_ById";
        #endregion


        #region UserSalons
        public const string UserSalons_gettodayworksheet = "UserSalons_gettodayworksheet";
        #endregion

        #region UserSalons
        public const string ProductWishlist_All = "ProductWishlist_All";
        public const string ProductWishlist_Delete = "ProductWishlist_Delete";
        public const string ProductWishlist_Upsert = "ProductWishlist_Upsert";
        #endregion

        public const string InventoryProduct_All = "InventoryProduct_All";
        public const string InventoryProduct_ById = "InventoryProduct_ById";
        public const string InventoryProduct_Upsert = "InventoryProduct_Upsert";
        public const string InventoryProduct_Delete = "InventoryProduct_Delete";
        public const string InventoryProduct_UpdateQty = "InventoryProduct_UpdateQty";
        public const string InventoryProduct_UpdateInventory = "InventoryProduct_UpdateInventory";
        public const string InventoryProduct_TopOne = "InventoryProduct_TopOne";
        public const string InventoryProduct_LowQtyAlert = "InventoryProduct_LowQtyAlert";

        #region OfflineVendor
        public const string OfflineVendor_All = "OfflineVendor_All";
        public const string OfflineVendor_Upsert = "OfflineVendor_Upsert";
        public const string OfflineVendor_ById = "OfflineVendor_ById";
        #endregion

        public const string MasterServicePackage_All = "MasterServicePackage_All";
        public const string PackageCustomPrice_Create = "PackageCustomPrice_Create";


        public const string ServicePackage_ActInAct = "ServicePackage_ActInAct";
        public const string ServicePackage_All = "ServicePackage_All";
        public const string ServicePackage_ById = "ServicePackage_ById";
        public const string ServicePackage_Upsert = "ServicePackage_Upsert";


        public const string Offer_All = "Offer_All";
        public const string Offer_ById = "Offer_ById";
        public const string Offer_Upsert = "Offer_Upsert";
        public const string Offer_Delete = "Offer_Delete";

        #region EmailMarketing
        public const string EmailMarketing_All = "EmailMarketing_All";
        public const string EmailMarketing_Upsert = "EmailMarketing_Upsert";
        public const string EmailMarketing_ById = "EmailMarketing_ById";
        public const string UserEmailMarketing_Upsert = "UserEmailMarketing_Upsert";

        #endregion

        #region SmsMarketing
        public const string SmsMarketing_All = "SmsMarketing_All";
        public const string SmsMarketing_Upsert = "SmsMarketing_Upsert";
        public const string SmsMarketing_ById = "SmsMarketing_ById";
        public const string UserSMSMarketing_Upsert = "UserSMSMarketing_Upsert";
        #endregion

        #region LookUpDuration
        public const string LookUpDuration_All = "LookUpDuration_All";
        #endregion

        #region Create_EmailMsg_Packages
        public const string Create_EmailMsg_Packages_All = "Create_EmailMsg_Packages_All";
        public const string Create_EmailMsg_Packages_ById = "Create_EmailMsg_Packages_ById";
        public const string Create_EmailMsg_Packages_Upsert = "Create_EmailMsg_Packages_Upsert";
        public const string Create_EmailMsg_Packages_Delete = "Create_EmailMsg_Packages_Delete";
        public const string Create_EmailMsg_Packages_ActInAct = "Create_EmailMsg_Packages_ActInAct";
        #endregion

        #region Create_SMS_Packages
        public const string Create_SMS_Packages_All = "Create_SMS_Packages_All";
        public const string Create_SMS_Packages_ById = "Create_SMS_Packages_ById";
        public const string Create_SMS_Packages_Upsert = "Create_SMS_Packages_Upsert";
        public const string Create_SMS_Packages_Delete = "Create_SMS_Packages_Delete";
        public const string Create_SMS_Packages_ActInAct = "Create_SMS_Packages_ActInAct";
        #endregion

        #region DailyDeals
        public const string DailyDeals_Delete = "DailyDeals_Delete";
        public const string DailyDeals_All = "DailyDeals_All";
        public const string DailyDeals_ById = "DailyDeals_ById";
        public const string DailyDeals_Upsert = "DailyDeals_Upsert";
        #endregion

        #region EmailMarketingTemplates
        public const string EmailMarketingTemplates_All = "EmailMarketingTemplates_All";
        public const string EmailMarketingTemplates_ById = "EmailMarketingTemplates_ById";
        public const string EmailMarketingTemplates_Delete = "EmailMarketingTemplates_Delete";
        public const string EmailMarketingTemplates_Upsert = "EmailMarketingTemplates_Upsert";
        #endregion

        #region SendEmailMarketingTemplates
        public const string SendEmailMarketingTemplates_Upsert = "SendEmailMarketingTemplates_Upsert";
        public const string EmailUser_All = "EmailUser_All";
        #endregion

        #region SendSMSMarketingTemplates
        public const string SendSMSMarketingTemplates_Upsert = "SendSMSMarketingTemplates_Upsert";
        public const string SMSUser_All = "SMSUser_All";
        #endregion
        #region SelectPlan_EmailPackages
        public const string SelectPlan_EmailPackages_Upsert = "SelectPlan_EmailPackages_Upsert";
        public const string SelectPlanEmailPackages_UpdatePaymentStatus = "SelectPlanEmailPackages_UpdatePaymentStatus";
        public const string SelectPlan_EmailPackages_BySalonId = "SelectPlan_EmailPackages_BySalonId";
        #endregion
        #region SelectPlan_SMSPackages
        public const string SelectPlan_SMSPackages_Upsert = "SelectPlan_SMSPackages_Upsert";
        public const string SelectPlanSMSPackages_UpdatePaymentStatus = "SelectPlanSMSPackages_UpdatePaymentStatus";
        public const string SelectPlan_SMSPackages_BySalonId = "SelectPlan_SMSPackages_BySalonId";
        public const string SelectPlan_CompleteNoOfMsg = "SelectPlan_CompleteNoOfMsg";
        #endregion

        public const string TermsAndConditions_All = "TermsAndConditions_All";
        public const string TermsAndConditions_ById = "TermsAndConditions_ById";
        public const string TermsAndConditions_Upsert = "TermsAndConditions_Upsert";

        public const string DashboardData_All = "DashboardData_All";

        #region SalonOwnerDashboard
        public const string SalonOwnerSMSandEmailPackages_All = "SalonOwnerSMSandEmailPackages_All";
        public const string SalonOwnerNewCustomer_All = "SalonOwnerNewCustomer_All";
        public const string SalonOwnerMostRequestedEmployee_All = "SalonOwnerMostRequestedEmployee_All";
        public const string SalonOwnerDashboard_All = "SalonOwnerDashboard_All";
        public const string SalonOwnerTopRequestedServices_All = "SalonOwnerTopRequestedServices_All";
        public const string TecnicalSupport_SalonId = "TecnicalSupport_SalonId";
        #endregion

        #region SalonVendorDashboard
        public const string VendorSalesAndRating_VendorId = "VendorSalesAndRating_VendorId";
        public const string VendorTopCustomer_All = "VendorTopCustomer_All";
        public const string VendorTopProduct_All = "VendorTopProduct_All";
        #endregion

        #region TechnicalSupport
        public const string TechnicalSupport_Upsert = "TechnicalSupport_Upsert";
        public const string TechnicalSupport_ById = "TechnicalSupport_ById";
        public const string TechnicalSupport_All = "TechnicalSupport_All";
        public const string TechnicalSupport_Delete = "TechnicalSupport_Delete";
        #endregion

        /////Blog/////////////////
        #region BlogCategory
        public const string BlogCategory_ActInAct = "BlogCategory_ActInAct";
        public const string BlogCategory_Upsert = "BlogCategory_Upsert";
        public const string BlogCategory_All = "BlogCategory_All";
        public const string BlogCategory_ById = "BlogCategory_ById";
        public const string BlogCategory_Delete = "BlogCategory_Delete";
        #endregion

        #region BlogUsers
        public const string BlogUsers_ActInAct = "BlogUsers_ActInAct";
        public const string BlogUsers_Upsert = "BlogUsers_Upsert";
        public const string BlogUsers_All = "BlogUsers_All";
        public const string BlogUsers_ById = "BlogUsers_ById";
        public const string BlogUser_Login = "BlogUser_Login";
        public const string BlogUsers_ChangePassword = "BlogUsers_ChangePassword";
        public const string BlogUsers_Delete = "BlogUsers_Delete";
        #endregion

        #region Blog
        public const string Blog_ActInAct = "Blog_ActInAct";
        public const string Blog_Upsert = "Blog_Upsert";
        public const string Blog_All = "Blog_All";
        public const string BlogTopArticles_All = "BlogTopArticles_All";
        public const string BlogTopPost_All = "BlogTopPost_All";
        public const string Blog_ById = "Blog_ById";
        public const string BlogSharedCount = "BlogSharedCount";
        public const string Blog_Delete = "Blog_Delete";
        public const string BlogImages_All = "BlogImages_All";
        public const string BlogImages_Delete = "BlogImages_Delete";
        public const string BlogImages_Upsert = "BlogImages_Upsert";
        public const string SocialShare_Insert = "SocialShare_Insert";
        public const string BlogLike_Insert = "BlogLike_Insert";
        #endregion

        #region BlogContent
        public const string BlogContent_Upsert = "BlogContent_Upsert";
        public const string BlogContent_Delete = "BlogContent_Delete";
        #endregion

        #region BlogSubscribeNewslatter
        public const string BlogSubscribeNewslatter_Insert = "BlogSubscribeNewslatter_Insert";
        public const string BlogSubscribeNewslatter_All = "BlogSubscribeNewslatter_All";
		#endregion

		/////POS/////////////////
		#region POSDetails 
		public const string POSDetails_Upsert = "POSDetails_Upsert";
		public const string POSDetails_All = "POSDetails_All";
		public const string PosOffer_All = "PosOffer_All";
		public const string POSDetails_ById = "POSDetails_ById";
		public const string POSDetails_Delete = "POSDetails_Delete";
		public const string POSAssignEmployee_All = "POSAssignEmployee_All";
		#endregion

		#region POSOrderDetails  
		public const string POSOrderDetails_Upsert = "POSOrderDetails_Upsert";
		public const string POSOrderDetails_All = "POSOrderDetails_All";
		public const string POSOrderDetails_ById = "POSOrderDetails_ById";
		public const string POSOrderDetails_Delete = "POSOrderDetails_Delete";
		#endregion

		#region POSOpeningCash  
		public const string POSOpeningCash_Add = "POSOpeningCash_Add";
		#endregion

		#region POSPayment  
		public const string POSPayment_Upsert = "POSPayment_Upsert";
		public const string POSPayment_ById = "POSPayment_ById";
		public const string PosInvoice_ById = "PosInvoice_ById";
		public const string PosReturnInvoice_ById = "PosReturnInvoice_ById";
		public const string PosInvoice_All = "PosInvoice_All";
        public const string PosReturnInvoice_Update = "PosReturnInvoice_Update";
        #endregion

        #region MasterPOSPaymentType  
        public const string MasterPOSPaymentType_All = "MasterPOSPaymentType_All";
		#endregion

		#region MasterPOSCoinsBills  
		public const string MasterPOSCoinsBills_All = "MasterPOSCoinsBills_All";
		#endregion

		#region POSSession  
		public const string POSSession_BySalonId = "POSSession_BySalonId";
		public const string POSSession_ById = "POSSession_ById";
		public const string POSSession_Create = "POSSession_Create";
		public const string POSSession_Authentication = "POSSession_Authentication";
		public const string POSSessionClosing_CachAndAt = "POSSessionClosing_CachAndAt";
		public const string POSSession_TopBySalonId = "POSSession_TopBySalonId";
        #endregion

        #region PrivacyPolicy
        public const string PrivacyPolicy_ById = "PrivacyPolicy_ById";
        public const string PrivacyPolicy_Upsert = "PrivacyPolicy_Upsert";
        #endregion

        #region EmployeePermission
        public const string EmployeeModulePermission_All = "EmployeeModulePermission_All";
        public const string EmployeePermissionData_Upsert = "EmployeePermissionData_Upsert";
        public const string EmployeePermissionData_Delete = "EmployeePermissionData_Delete";
        public const string EmployeePermissionData_EmployeeId = "EmployeePermissionData_EmployeeId";
        #endregion
    }
}
