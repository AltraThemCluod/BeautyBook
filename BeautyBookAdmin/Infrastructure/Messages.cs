using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyBookAdmin.Infrastructure
{
    public class Messages
    {
        #region General
        public static string AccessDenied = "You are not authorized to access this page.";
        public static string AccessDeniedContactAdmin = "If you require to access this page, Please contact the system administrator.";
        public static string ConfirmDectivate = "Are you sure you want to deactivate this Record?";
        public static string RecordActivatedSuccessfully = "Record activated successfully.";
        public static string RecordDeactivatedSuccessfully = "Record deactivated successfully.";
        public static string RecordActivationFailed = "Record activation/deactivation failed.";
        public static string AlreadyCompanyNameExist = "This Company Name is already exists.";
        public static string AlreadyProductNameExist = "This Product Name is already exists.";
        public static string AlreadyDisplayOrderExist = "This display order is already exists.";
        public static string ImageORVideoIsCompulsory = "Please choose Video or Image.";
        public static string ConfirmChangeCover = "If you active this Image as Cover then all other images will be inactive. Are you sure you want to active this Image as Cover?";
        public static string OrderStatusUpdatedSuccessfully = "Order Status updated successfully.";
        public static string ConfirmChangeStatus = "If you active this deal then all other deals will be inactive. Are you sure you want to active this deal?";
        public static string StatusUpdateFailed = "An error occurred on the system. Status can not updated.";
        public static string DefaultNotDelete = "Can not delete default bank.";
        public static string RecordSyncSuccessfully = "Sync data successfully.";
        public static string InValidClientCode = "Please Enter valid Client Code.";
        public static string InValidCredential = "Please Enter valid UserName or Password.";
        public static string InActiveAccount = "Your Account is not active. Please contact to administrator.";
        public static string ClientConfigInvalid = "Client is not Configured properly. Please contact to administrator.";
        public static string Mailsend = "Instructions on how to reset your password have been sent to your email account.";
        public static string InvalidEmail = "We couldn't find a BeautyBook account associated with email address provided.";
        public static string PasswordUnmatch = "Password does't match.";
        public static string ProductSavedSuccessfully = "Product saved successfully. Please add product details.";
        public static string AlreadyBlogNameExist = "This Blog Name is already exists.";
        public static string AlreadyCouponNameExist = "This Coupon Name or Code is already exists.";
        public static string ProductSKUSlugAlreadyExist = "This Product Sku or Slug is already exists.";
        public static string StatusChangedSuccessfully = "Status changed Successfully.";
        public static string SlugUrlAlreadyExist = "SlugURL is already exists.";
        public static string SmallSizeImage = "Image size must be greater or equal to 870X1110 px";
        public static string SmallSizeImageBlog = "Image size must be greater or equal to 500X500 px";
        public static string SmallSizeImageLookbook = "Image size must be equal to 1920X1080 px";
        public static string SmallSizeImageBlogCover = "Cover Image size must be greater or equal to 500X500 px";
        public static string NoOrderAvailableForShipped = "There is not a single order available for shipped.";
        public static string existsProduct = "Product Name already exists";
        public static string EmailSendSuccessfully = "Send Invoice Successfully.";
        public static string EmailNotExists = "This Customer Email is not Exists.";
        public static string NotRegisterMsg = "Your number is not registered with us";
        public static string MeasurableExist = "Measurable with this name already Exist.";
        public static string TimeBoundExist = "Timebound with this name already Exist.";
        public static string ActionableExist = "Actionable with this name already Exist.";
        public static string SpecificExist = "Specific with same name already exists.";
        public static string DepartmentExist = "Department with this name already Exist.";
        public static string DepartmentCodeExist = "Department with this code already Exist.";
        public static string EmployeeEmailExists = "Employee email already exists.";
        public static string EmailExists = "Email already exists.";
        public static string ClientExists = "Client already exists with same email or clientcode.";
        public static string GoalTypeExist = "Goal Type with this name already Exist.";
        public static string PaymentModeExist = "Payment Mode with this name already Exist.";
        public static string PackageExist = "Package with this name already Exist.";
        public static string ReceivedPaymentExist = "Received Payment with this name already Exist.";
        public static string PartyExist = "Party with this name already Exist.";
        public static string PartyTypeExist = "Party Type with this name already Exist.";
        public static string ShopExist = "Shop with this name already Exist.";
        public static string ItemExist = "Item with this name already Exist."; 
        public static string TagExist = "Tags with this name already Exist.";
        public static string SalesmanExist = "Salesman with this name already Exist.";
        public static string LicenseInformationExist = "License Information with this name already Exist.";
        public static string StoreExist = "Store with this name already Exist.";
        public static string NoPixelIDs = "Make sure you need insert main facebook pixel id in settings.";
        public static string SelectUserForEmail = "Please select user's to send an email.";
        public static string GoalStatusSendToUser = "Goal Status mail has been sent successfully.";
        public static string GoalDetailsUploaded(int total = 0, int success = 0)
        {
            return string.Format("{0} of {1} goals has been uploaded successfully.", success, total);
        }
        public static string GoalDetailsUpdated = "Goal details has been updated successfully.";
        public static string InvalidGoalsheetUploaded = "Please select valid data sheet having goals data.";
        public static string InvalidGoalDetails = "Pelase select valid Goal details";
        public static string RolePermissionsSaved = "Permissions for this Role saved successfully.";
        public static string RolePermissionsFailed = "Permissions for this Role failed to save, Please try again.";
        public static string InvalidApiCredential = "Please enter valid Api Credentials.";
        public static string InvalidApiKey = "Please enter valid Api key.";
        public static string InvalidClientCode = "Please enter valid Client Code.";
        public static string GoalUploaded = "Goal detail has been updated successfully.";
        public static string InvalidGoalsExist = "There are some Invalid Goals please correct out it before upload it again.";
        public static string WelcomeText = "<p style='margin: 0 0 20px;font-size: 16px;line-height: 1.4;'>Welcome to HIPO Insights, our tool analyses your goals to help you understand if your goal is SMART (Specific, Measurable, Achievable, Realistic & Time-Bound).</p>";



        public static string CommonErrorMessage = "Something went wrong, Please try again";
        public static string ConfirmDelete = "Are you sure you want to delete this Record?";
        public static string ConfirmStatuschange = "Are you sure you want to SubAdmin Status Change? ";
        public static string ContactToAdmin = "An error occurred on the system. Please contact the administrator.";
        public static string RecordSavedSuccessfully = "Record saved successfully.";
        public static string RecordDeletedSuccessfully = "Record deleted successfully.";
        public static string StateRecordNotDeleted = "Record is not deleted successfully because it refers to another District.";
        public static string DistrictRecordNotDeleted = "Record is not deleted successfully because it refers to another Taluka.";
        public static string RecordNotDeleted = "Record is not deleted successfully because it refers to another department.";
        public static string ConfirmActivate = "Are you sure you want to activate this Record?";
        public static string PasswordReSet = "Password changed successfully.";
        public static string AlreadyExist = "This Mobile Number or Email is already exists.";
        public static string CopyContent = "Copy to clipboard.";
        public static string MsgSendSuccess = "Message Send Successfully";
        public static string NtfSendSuccess = "Notification Send Successfully";
        public static string ProductAdd = "Please Add Product !";
        #endregion

    }
}