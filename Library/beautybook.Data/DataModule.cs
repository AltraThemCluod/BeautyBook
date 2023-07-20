//-----------------------------------------------------------------------
// <copyright file="DataModule.cs" company="Rushkar">
//     Copyright Rushkar Solutions. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Data
{
    using Autofac;
    using BeautyBook.Data.Contract;

    //using BeautyBook.Data.Contract;


    /// <summary>
    /// Contract Class for DataModule.
    /// </summary>
    public class DataModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<V1.LookUpServicesDao>().As<AbstractLookUpServicesDao>().InstancePerDependency();
            builder.RegisterType<V1.SalonsDao>().As<AbstractSalonsDao>().InstancePerDependency();
            builder.RegisterType<V1.SalonServicesDao>().As<AbstractSalonServicesDao>().InstancePerDependency();
            builder.RegisterType<V1.UsersDao>().As<AbstractUsersDao>().InstancePerDependency();
            builder.RegisterType<V1.LookUpCountryDao>().As<AbstractLookUpCountryDao>().InstancePerDependency();
            builder.RegisterType<V1.LookUpEmployeeRolesDao>().As<AbstractLookUpEmployeeRolesDao>().InstancePerDependency();
            builder.RegisterType<V1.LookUpEmployeeTypeDao>().As<AbstractLookUpEmployeeTypeDao>().InstancePerDependency();
            builder.RegisterType<V1.LookUpLeaveTypeDao>().As<AbstractLookUpLeaveTypeDao>().InstancePerDependency();
            builder.RegisterType<V1.LookUpPaymentMethodDao>().As<AbstractLookUpPaymentMethodDao>().InstancePerDependency();
            builder.RegisterType<V1.LookUpStateDao>().As<AbstractLookUpStateDao>().InstancePerDependency();
            builder.RegisterType<V1.LookUpStatusDao>().As<AbstractLookUpStatusDao>().InstancePerDependency();
            builder.RegisterType<V1.LookUpUserTypeDao>().As<AbstractLookUpUserTypeDao>().InstancePerDependency();
            builder.RegisterType<V1.SellerProductsFeedbackDao>().As<AbstractSellerProductsFeedbackDao>().InstancePerDependency();
            builder.RegisterType<V1.UserServicesDao>().As<AbstractUserServicesDao>().InstancePerDependency();
            builder.RegisterType<V1.UserWorkingHoursDao>().As<AbstractUserWorkingHoursDao>().InstancePerDependency();
            builder.RegisterType<V1.UserAppointmentsDao>().As<AbstractUserAppointmentsDao>().InstancePerDependency();
            builder.RegisterType<V1.UserLeaveDao>().As<AbstractUserLeaveDao>().InstancePerDependency();
            builder.RegisterType<V1.UserWorkSheetDao>().As<AbstractUserWorkSheetDao>().InstancePerDependency();
            builder.RegisterType<V1.AdminDao>().As<AbstractAdminDao>().InstancePerDependency();
            builder.RegisterType<V1.ProductDao>().As<AbstractProductDao>().InstancePerDependency();
            builder.RegisterType<V1.MasterProductBrandDao>().As<AbstractMasterProductBrandDao>().InstancePerDependency();
            builder.RegisterType<V1.MasterProductTypeDao>().As<AbstractMasterProductTypeDao>().InstancePerDependency();
            builder.RegisterType<V1.MasterProductWeightDao>().As<AbstractMasterProductWeightDao>().InstancePerDependency();
            builder.RegisterType<V1.MasterOrderStatusDao>().As<AbstractMasterOrderStatusDao>().InstancePerDependency();
            builder.RegisterType<V1.OrdersDao>().As<AbstractOrdersDao>().InstancePerDependency();
            builder.RegisterType<V1.OrderProductsDao>().As<AbstractOrderProductsDao>().InstancePerDependency();
            builder.RegisterType<V1.WriteaReviewDao>().As<AbstractWriteaReviewDao>().InstancePerDependency();
            builder.RegisterType<V1.ProductImageDao>().As<AbstractProductImageDao>().InstancePerDependency();
            builder.RegisterType<V1.MasterProductCategoryDao>().As<AbstractMasterProductCategoryDao>().InstancePerDependency();
            builder.RegisterType<V1.OrderStatusTrackingDao>().As<AbstractOrderStatusTrackingDao>().InstancePerDependency();
            builder.RegisterType<V1.PromotionDao>().As<AbstractPromotionDao>().InstancePerDependency();
            builder.RegisterType<V1.AddToCartDao>().As<AbstractAddToCartDao>().InstancePerDependency();

            builder.RegisterType<V1.ShippingAddressDao>().As<AbstractShippingAddressDao>().InstancePerDependency();
            builder.RegisterType<V1.UserSalonsDao>().As<AbstractUserSalonsDao>().InstancePerDependency();
            builder.RegisterType<V1.ProductWishlistDao>().As<AbstractProductWishlistDao>().InstancePerDependency();
            builder.RegisterType<V1.InventoryProductDao>().As<AbstractInventoryProductDao>().InstancePerDependency();
            builder.RegisterType<V1.OfflineVendorDao>().As<AbstractOfflineVendorDao>().InstancePerDependency();

            builder.RegisterType<V1.ServicePackageDao>().As<AbstractServicePackageDao>().InstancePerDependency();
            builder.RegisterType<V1.MasterServicePackageDao>().As<AbstractMasterServicePackageDao>().InstancePerDependency();
            builder.RegisterType<V1.PackageCustomPriceDao>().As<AbstractPackageCustomPriceDao>().InstancePerDependency();

            builder.RegisterType<V1.EmailMarketingDao>().As<AbstractEmailMarketingDao>().InstancePerDependency();
            builder.RegisterType<V1.SmsMarketingDao>().As<AbstractSmsMarketingDao>().InstancePerDependency();
            builder.RegisterType<V1.OfferDao>().As<AbstractOfferDao>().InstancePerDependency();

            builder.RegisterType<V1.LookUpDurationDao>().As<AbstractLookUpDurationDao>().InstancePerDependency();
            builder.RegisterType<V1.Create_EmailMsg_PackagesDao>().As<AbstractCreate_EmailMsg_PackagesDao>().InstancePerDependency();
            builder.RegisterType<V1.Create_SMS_PackagesDao>().As<AbstractCreate_SMS_PackagesDao>().InstancePerDependency();
            builder.RegisterType<V1.DailyDealsDao>().As<AbstractDailyDealsDao>().InstancePerDependency();
            builder.RegisterType<V1.EmailMarketingTemplatesDao>().As<AbstractEmailMarketingTemplatesDao>().InstancePerDependency();
            builder.RegisterType<V1.SendEmailMarketingTemplatesDao>().As<AbstractSendEmailMarketingTemplatesDao>().InstancePerDependency();
            builder.RegisterType<V1.SendSmsMarketingTemplatesDao>().As<AbstractSendSmsMarketingTemplatesDao>().InstancePerDependency();

            builder.RegisterType<V1.SelectPlan_SMSPackagesDao>().As<AbstractSelectPlan_SMSPackagesDao>().InstancePerDependency();
            builder.RegisterType<V1.SelectPlan_EmailPackagesDao>().As<AbstractSelectPlan_EmailPackagesDao>().InstancePerDependency();
            builder.RegisterType<V1.VendorApprovelDao>().As<AbstractVendorApprovelDao>().InstancePerDependency();
            builder.RegisterType<V1.TermsAndConditionsDao>().As<AbstractTermsAndConditionsDao>().InstancePerDependency();
            builder.RegisterType<V1.DashboardDataDao>().As<AbstractDashboardDataDao>().InstancePerDependency();
            builder.RegisterType<V1.SalonOwnerDashboardDao>().As<AbstractSalonOwnerDashboardDao>().InstancePerDependency();
            builder.RegisterType<V1.SalonVendorDashboardDao>().As<AbstractSalonVendorDashboardDao>().InstancePerDependency();
            builder.RegisterType<V1.TechnicalSupportDao>().As<AbstractTechnicalSupportDao>().InstancePerDependency();
            builder.RegisterType<V1.PrivacyPolicyDao>().As<AbstractPrivacyPolicyDao>().InstancePerDependency();

            ///Blog////////////////////////////////////

            builder.RegisterType<V1.BlogCategoryDao>().As<AbstractBlogCategoryDao>().InstancePerDependency();
            builder.RegisterType<V1.BlogContentDao>().As<AbstractBlogContentDao>().InstancePerDependency();
            builder.RegisterType<V1.BlogDao>().As<AbstractBlogDao>().InstancePerDependency();
            builder.RegisterType<V1.BlogSubscribeNewslatterDao>().As<AbstractBlogSubscribeNewslatterDao>().InstancePerDependency();
            builder.RegisterType<V1.BlogUsersDao>().As<AbstractBlogUsersDao>().InstancePerDependency();

			///POS////////////////////////////////////
			builder.RegisterType<V1.POSDetailsDao>().As<AbstractPOSDetailsDao>().InstancePerDependency();
            builder.RegisterType<V1.POSOrderDetailsDao>().As<AbstractPOSOrderDetailsDao>().InstancePerDependency();
            builder.RegisterType<V1.POSOpeningCashDao>().As<AbstractPOSOpeningCashDao>().InstancePerDependency();
            builder.RegisterType<V1.POSPaymentDao>().As<AbstractPOSPaymentDao>().InstancePerDependency();
            builder.RegisterType<V1.MasterPOSPaymentTypeDao>().As<AbstractMasterPOSPaymentTypeDao>().InstancePerDependency();
            builder.RegisterType<V1.MasterPOSCoinsBillsDao>().As<AbstractMasterPOSCoinsBillsDao>().InstancePerDependency();
            builder.RegisterType<V1.POSSessionDao>().As<AbstractPOSSessionDao>().InstancePerDependency();
            builder.RegisterType<V1.EmployeePermissionDao>().As<AbstractEmployeePermissionDao>().InstancePerDependency();

            base.Load(builder);
        }
    }
}
