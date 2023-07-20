//-----------------------------------------------------------------------
// <copyright file="ServiceModule.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Services
{
    using Autofac;
    using Data;
    using BeautyBook.Services.Contract;
    using BeautyBook.Entities.Contract;





    /// <summary>
    /// The Service module for dependency injection.
    /// </summary>
    public class ServiceModule : Module
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
            builder.RegisterModule<DataModule>();
            builder.RegisterType<V1.LookUpServicesServices>().As<AbstractLookUpServicesServices>().InstancePerDependency();
            builder.RegisterType<V1.SalonsServices>().As<AbstractSalonsServices>().InstancePerDependency();
            builder.RegisterType<V1.SalonServicesServices>().As<AbstractSalonServicesServices>().InstancePerDependency();
            builder.RegisterType<V1.SellerProductsFeedbackServices>().As<AbstractSellerProductsFeedbackServices>().InstancePerDependency();
            builder.RegisterType<V1.UsersServices>().As<AbstractUsersServices>().InstancePerDependency();
            builder.RegisterType<V1.LookUpCountryServices>().As<AbstractLookUpCountryServices>().InstancePerDependency();
            builder.RegisterType<V1.LookUpEmployeeRolesServices>().As<AbstractLookUpEmployeeRolesServices>().InstancePerDependency();
            builder.RegisterType<V1.LookUpEmployeeTypeServices>().As<AbstractLookUpEmployeeTypeServices>().InstancePerDependency();
            builder.RegisterType<V1.LookUpLeaveTypeServices>().As<AbstractLookUpLeaveTypeServices>().InstancePerDependency();
            builder.RegisterType<V1.LookUpPaymentMethodServices>().As<AbstractLookUpPaymentMethodServices>().InstancePerDependency();
            builder.RegisterType<V1.LookUpStateServices>().As<AbstractLookUpStateServices>().InstancePerDependency();
            builder.RegisterType<V1.LookUpStatusServices>().As<AbstractLookUpStatusServices>().InstancePerDependency();
            builder.RegisterType<V1.LookUpUserTypeServices>().As<AbstractLookUpUserTypeServices>().InstancePerDependency();
            builder.RegisterType<V1.UserServicesServices>().As<AbstractUserServicesServices>().InstancePerDependency();
            builder.RegisterType<V1.UserWorkingHoursServices>().As<AbstractUserWorkingHoursServices>().InstancePerDependency();
            builder.RegisterType<V1.UserAppointmentsServices>().As<AbstractUserAppointmentsServices>().InstancePerDependency();
            builder.RegisterType<V1.UserLeaveServices>().As<AbstractUserLeaveServices>().InstancePerDependency();
            builder.RegisterType<V1.UserWorkSheetServices>().As<AbstractUserWorkSheetServices>().InstancePerDependency();
            builder.RegisterType<V1.AdminServices>().As<AbstractAdminServices>().InstancePerDependency();
            builder.RegisterType<V1.ProductServices>().As<AbstractProductServices>().InstancePerDependency();
            builder.RegisterType<V1.VendorApprovelServices>().As<AbstractVendorApprovelServices>().InstancePerDependency();

            builder.RegisterType<V1.MasterProductBrandServices>().As<AbstractMasterProductBrandServices>().InstancePerDependency();
            builder.RegisterType<V1.MasterProductTypeServices>().As<AbstractMasterProductTypeServices>().InstancePerDependency();
            builder.RegisterType<V1.MasterProductWeightServices>().As<AbstractMasterProductWeightServices>().InstancePerDependency();
            builder.RegisterType<V1.MasterOrderStatusServices>().As<AbstractMasterOrderStatusServices>().InstancePerDependency();

            builder.RegisterType<V1.OrderProductsServices>().As<AbstractOrderProductsServices>().InstancePerDependency();
            builder.RegisterType<V1.OrdersServices>().As<AbstractOrdersServices>().InstancePerDependency();
            builder.RegisterType<V1.ProductImageServices>().As<AbstractProductImageServices>().InstancePerDependency();

            builder.RegisterType<V1.WriteaReviewServices>().As<AbstractWriteaReviewServices>().InstancePerDependency();
            builder.RegisterType<V1.MasterProductCategoryServices>().As<AbstractMasterProductCategoryServices>().InstancePerDependency();
            builder.RegisterType<V1.OrderStatusTrackingServices>().As<AbstractOrderStatusTrackingServices>().InstancePerDependency();
            builder.RegisterType<V1.PromotionServices>().As<AbstractPromotionServices>().InstancePerDependency();
            builder.RegisterType<V1.AddToCartServices>().As<AbstractAddToCartServices>().InstancePerDependency();

            builder.RegisterType<V1.ShippingAddressServices>().As<AbstractShippingAddressServices>().InstancePerDependency();
            builder.RegisterType<V1.UserSalonsServices>().As<AbstractUserSalonsServices>().InstancePerDependency();
            builder.RegisterType<V1.ProductWishlistServices>().As<AbstractProductWishlistServices>().InstancePerDependency();
            builder.RegisterType<V1.InventoryProductServices>().As<AbstractInventoryProductServices>().InstancePerDependency();
            builder.RegisterType<V1.OfflineVendorServices>().As<AbstractOfflineVendorServices>().InstancePerDependency();


            builder.RegisterType<V1.ServicePackageServices>().As<AbstractServicePackageServices>().InstancePerDependency();
            builder.RegisterType<V1.MasterServicePackageServices>().As<AbstractMasterServicePackageServices>().InstancePerDependency();
            builder.RegisterType<V1.PackageCustomPriceServices>().As<AbstractPackageCustomPriceServices>().InstancePerDependency();

            builder.RegisterType<V1.EmailMarketingServices>().As<AbstractEmailMarketingServices>().InstancePerDependency();
            builder.RegisterType<V1.SmsMarketingServices>().As<AbstractSmsMarketingServices>().InstancePerDependency();
            builder.RegisterType<V1.OfferServices>().As<AbstractOfferServices>().InstancePerDependency();

            builder.RegisterType<V1.LookUpDurationServices>().As<AbstractLookUpDurationServices>().InstancePerDependency();
            builder.RegisterType<V1.Create_EmailMsg_PackagesServices>().As<AbstractCreate_EmailMsg_PackagesServices>().InstancePerDependency();
            builder.RegisterType<V1.Create_SMS_PackagesServices>().As<AbstractCreate_SMS_PackagesServices>().InstancePerDependency();
            builder.RegisterType<V1.DailyDealsServices>().As<AbstractDailyDealsServices>().InstancePerDependency();
            builder.RegisterType<V1.EmailMarketingTemplatesServices>().As<AbstractEmailMarketingTemplatesServices>().InstancePerDependency();
            builder.RegisterType<V1.SendEmailMarketingTemplatesServices>().As<AbstractSendEmailMarketingTemplatesServices>().InstancePerDependency();
            builder.RegisterType<V1.SendSmsMarketingTemplatesServices>().As<AbstractSendSmsMarketingTemplatesServices>().InstancePerDependency();

            builder.RegisterType<V1.SelectPlan_SMSPackagesServices>().As<AbstractSelectPlan_SMSPackagesServices>().InstancePerDependency();
            builder.RegisterType<V1.SelectPlan_EmailPackagesServices>().As<AbstractSelectPlan_EmailPackagesServices>().InstancePerDependency();
            builder.RegisterType<V1.TermsAndConditionsServices>().As<AbstractTermsAndConditionsServices>().InstancePerDependency();
            builder.RegisterType<V1.DashboardDataServices>().As<AbstractDashboardDataServices>().InstancePerDependency();
            builder.RegisterType<V1.SalonOwnerDashboardServices>().As<AbstractSalonOwnerDashboardServices>().InstancePerDependency();
            builder.RegisterType<V1.SalonVendorDashboardServices>().As<AbstractSalonVendorDashboardServices>().InstancePerDependency();
            builder.RegisterType<V1.TechnicalSupportServices>().As<AbstractTechnicalSupportServices>().InstancePerDependency();
            builder.RegisterType<V1.PrivacyPolicyServices>().As<AbstractPrivacyPolicyServices>().InstancePerDependency();
            
            ///Blog////////////////////
            builder.RegisterType<V1.BlogCategoryServices>().As<AbstractBlogCategoryServices>().InstancePerDependency();
            builder.RegisterType<V1.BlogContentServices>().As<AbstractBlogContentServices>().InstancePerDependency();
            builder.RegisterType<V1.BlogServices>().As<AbstractBlogServices>().InstancePerDependency();
            builder.RegisterType<V1.BlogSubscribeNewslatterServices>().As<AbstractBlogSubscribeNewslatterServices>().InstancePerDependency();
            builder.RegisterType<V1.BlogUsersServices>().As<AbstractBlogUsersServices>().InstancePerDependency();

			///POS////////////////////
			builder.RegisterType<V1.POSDetailsServices >().As< AbstractPOSDetailsServices >().InstancePerDependency();
			builder.RegisterType<V1.POSOrderDetailsServices >().As< AbstractPOSOrderDetailsServices >().InstancePerDependency();
			builder.RegisterType<V1.POSOpeningCashServices>().As<AbstractPOSOpeningCashServices>().InstancePerDependency();
			builder.RegisterType<V1.POSPaymentServices>().As<AbstractPOSPaymentServices>().InstancePerDependency();
			builder.RegisterType<V1.MasterPOSPaymentTypeServices>().As<AbstractMasterPOSPaymentTypeServices>().InstancePerDependency();
			builder.RegisterType<V1.MasterPOSCoinsBillsServices>().As<AbstractMasterPOSCoinsBillsServices>().InstancePerDependency();
			builder.RegisterType<V1.POSSessionServices>().As<AbstractPOSSessionServices>().InstancePerDependency();

			builder.RegisterType<V1.EmployeePermissionServices>().As<AbstractEmployeePermissionServices>().InstancePerDependency();

			base.Load(builder);
        }
    }
}
