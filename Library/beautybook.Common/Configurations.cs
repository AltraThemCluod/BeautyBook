//-----------------------------------------------------------------------
// <copyright file="TitleConfigurations.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace BeautyBook.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class configurations
    /// </summary>
    public class Configurations
    {
        /// <summary>
        /// Gets the SMSApi.
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public static string BaseSmsApiAddress
        {
            get
            {
                return ConvertTo.String(ConfigurationManager.AppSettings["BaseSmsApiAddress"]);
            }
        }

        public static string ShareBlogBaseUrl
        {
            get
            {
                return ConvertTo.String(ConfigurationManager.AppSettings["ShareBlogBaseUrl"]);
            }
        }

        /// <summary>
        /// Gets the SMSApi.
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public static string SmsApiParameters
        {
            get
            {
                return ConvertTo.String(ConfigurationManager.AppSettings["SmsApiParameters"]);
            }
        }

        /// <summary>
        /// Gets the otp.
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public static int OtpPopup
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["OtpPopup"]);
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static int PageSize
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static bool TestMode
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["TestMode"]);
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static string TestEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["TestEmailAddress"];
            }
        }

        public static string EmployeSignInUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["EmployeSignInUrl"];
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static string FromEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["FromEmailAddress"];
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["ConnectionString"];
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static string LogConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["LogConnectionString"];
            }
        }

        public static int CookiesValidity
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["CookiesValidity"]);
            }
        }

        /// <summary>
        /// Gets the reach asset URI.
        /// </summary>
        /// <value>
        /// The reach asset URI.
        /// </value>
        public static string ReachAssetURI
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachAssetURI"];
            }
        }

        /// <summary>
        /// Gets the reach asset user name.
        /// </summary>
        /// <value>
        /// The reach asset user name.
        /// </value>
        public static string ReachAssetUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachAssetUsername"];
            }
        }

        /// <summary>
        /// Gets the reach asset password.
        /// </summary>
        /// <value>
        /// The reach asset password.
        /// </value>
        public static string ReachAssetPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachAssetPassword"];
            }
        }

        /// <summary>
        /// Gets the order service URL.
        /// </summary>
        /// <value>
        /// The order service URL.
        /// </value>
        public static string OrderServiceURL
        {
            get
            {
                return ConfigurationManager.AppSettings["OrderServiceURL"];
            }
        }

        /// <summary>
        /// Gets the reach elastic URI.
        /// </summary>
        /// <value>
        /// The reach elastic URI.
        /// </value>
        public static string ReachElasticURI
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachElasticURI"];
            }
        }

        /// <summary>
        /// Gets the index of the reach elastic.
        /// </summary>
        /// <value>
        /// The index of the reach elastic.
        /// </value>
        public static string ReachElasticIndex
        {
            get
            {
                return ConfigurationManager.AppSettings["ReachElasticIndex"];
            }
        }

        /// <summary>
        /// Gets the iTunes forbidden retry count.
        /// </summary>
        /// <value>
        /// The iTunes forbidden retry count.
        /// </value>
        public static int ItunesForbiddenRetryCount
        {
            get
            {
                return System.Convert.ToInt16(ConfigurationManager.AppSettings["ItunesForbiddenRetryCount"]);
            }
        }

        public static string S3AccessKeyID
        {
            get
            {
                return ConfigurationManager.AppSettings["S3AccessKeyID"];
            }
        }

        public static string S3SecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["S3SecretKey"];
            }
        }

        public static string BucketName
        {
            get
            {
                return ConfigurationManager.AppSettings["BucketName"];
            }
        }

        public static string ClientURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ClientURL"];
            }
        }

        public static string urlEndpoint
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["urlEndpoint"]);
            }
        }

        public static string PublicURL
        {
            get
            {
                return ConfigurationManager.AppSettings["PublicURL"];
            }
        }

        public static string SquareUpLocationId
        {
            get
            {
                return ConfigurationManager.AppSettings["SquareUpLocationId"];
            }
        }

        public static string SquareUpAccessToken
        {
            get
            {
                return ConfigurationManager.AppSettings["SquareUpAccessToken"];
            }
        }

        public static string SquareUpApplicationId
        {
            get
            {
                return ConfigurationManager.AppSettings["SquareUpApplicationId"];
            }
        }

        public static string Port
        {
            get
            {
                return ConfigurationManager.AppSettings["Port"];
            }
        }

        public static string EmailHost
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailHost"];
            }
        }

        public static string EnableSsl
        {
            get
            {
                return ConfigurationManager.AppSettings["EnableSsl"];
            }
        }

        public static string EmailUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailUserName"];
            }
        }

        public static string EmailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailPassword"];
            }
        }

        public static string BccEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["BccEmailAddress"];
            }
        }

        public static string ToEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["ToEmailAddress"];
            }
        }


        public static string UserName
        {
            get
            {
                return ConfigurationManager.AppSettings["UserName"];
            }
        }

        public static string AdminUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["AdminUrl"];
            }
        }

        public static string SalonBranchCreateSendEmailId
        {
            get
            {
                return ConfigurationManager.AppSettings["SalonBranchCreateSendEmailId"];
            }
        }   

        public static string UPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["UPassword"];
            }
        }

        public static string CCAvenue
        {
            get
            {
                return ConfigurationManager.AppSettings["CCAvenue"];
            }
        }

        public static string BaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Baseurl"];
            }
        }
        public static string ApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiUrl"];
            }
        }
        public static string FontEndUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["FontEndUrl"];
            }
        }

        public static string tlui2BlobBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["tlui2BlobBaseUrl"];
            }
        }

        public static string DelhiveryBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryBaseUrl"];
            }
        }

        public static string DelhiveryTrackingUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryTrackingUrl"];
            }
        }


        public static string S3BaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["S3BaseUrl"];
            }
        }

        public static string DelhiveryPickUpPin
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpPin"];
            }
        }
        public static string DelhiveryPickUpAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpAddress"];
            }
        }
        public static string DelhiveryPickUpPhone
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpPhone"];
            }
        }
        public static string DelhiveryPickUpState
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpState"];
            }
        }
        public static string DelhiveryPickUpCity
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpCity"];
            }
        }
        public static string DelhiveryPickUpCountry
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpCountry"];
            }
        }
        public static string DelhiveryPickUpName
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryPickUpName"];
            }
        }
        public static string DelhiveryCreateOrderAPIURL
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryCreateOrderAPIURL"];
            }
        }
        public static string DelhiveryToken
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryToken"];
            }
        }

        public static string DelhiveryCreatePincodeAPIURL
        {
            get
            {
                return ConfigurationManager.AppSettings["DelhiveryCreatePincodeAPIURL"];
            }
        }

        public static string ServerKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ServerKey"];
            }
        }

        public static string RedirectUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["RedirectUrl"];
            }
        }

        public static string CancelUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["CancelUrl"];
            }
        }

        public static string Pincode
        {
            get
            {
                return ConfigurationManager.AppSettings["Pincode"];
            }
        }
        public static string CCAvenueMerchantId
        {
            get
            {
                return ConfigurationManager.AppSettings["CCAvenueMerchantId"];
            }
        }
        public static string CCAvenueCode
        {
            get
            {
                return ConfigurationManager.AppSettings["CCAvenueCode"];
            }
        }

        public static string CCAvenueWorkingKey
        {
            get
            {
                return ConfigurationManager.AppSettings["CCAvenueWorkingKey"];
            }
        }

        public static string ClientLoginURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ClientLoginURL"];
            }
        }
        public static string SchedulerTime
        {
            get
            {
                return ConfigurationManager.AppSettings["SchedulerTime"];
            }
        }
        public static string AndroidUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["AndroidUrl"];
            }
        }
        public static string IosUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["IosUrl"];
            }
        }

        public static string WebURL
        {
            get
            {
                return ConfigurationManager.AppSettings["WebURL"];
            }
        }
        public static string CustomerUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["CustomerUrl"];
            }
        }
        public static string MerchantUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["MerchantUrl"];
            }
        }

        public static string AggregatorLoginURL
        {
            get
            {
                return ConfigurationManager.AppSettings["AggregatorLoginURL"];
            }
        }

        public static string ManufacturerLoginURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ManufacturerLoginURL"];
            }
        }

        public static string CustomerLoginURL
        {
            get
            {
                return ConfigurationManager.AppSettings["CustomerLoginURL"];
            }
        }

        public static string GlobalAggregatorReferenceCode
        {
            get
            {
                return ConfigurationManager.AppSettings["GlobalAggregatorReferenceCode"];
            }
        }

        public static string CurrentTargetSchedulerTime
        {
            get
            {
                return ConfigurationManager.AppSettings["CurrentTargetSchedulerTime"];
            }
        }

        public static string ApplicationCode
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationCode"];
            }
        }

        public static string S3ImageUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["S3ImageUrl"];
            }
        }

        /// <summary>
        /// Gets the otp for Resend SMS.
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public static int UseDiffrentCredentialForResend
        {
            get
            {
                return Convert.ToInt16(ConfigurationManager.AppSettings["UseDiffrentCredentialForResend"]);
            }
        }

        public static string ResendBaseSmsApiAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["ResendBaseSmsApiAddress"];
            }
        }
        public static string ResendSmsApiParameters
        {
            get
            {
                return ConfigurationManager.AppSettings["ResendSmsApiParameters"];
            }
        }
        public static string OrderPrice
        {
            get
            {
                return ConfigurationManager.AppSettings["OrderPrice"];
            }
        }

        public static string UserAgreementBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UserAgreementBaseUrl"];
            }
        }

        public static string UserAgreementVerification
        {
            get
            {
                return ConfigurationManager.AppSettings["UserAgreementVerification"];
            }
        }

        public static string UserAgreementVerificationXFunctionsKey
        {
            get
            {
                return ConfigurationManager.AppSettings["UserAgreementVerificationXFunctionsKey"];
            }
        }

        public static string InsertUserAgreementFunction
        {
            get
            {
                return ConfigurationManager.AppSettings["InsertUserAgreementFunction"];
            }
        }

        public static string InsertUserAgreementFunctionXFunctionsKey
        {
            get
            {
                return ConfigurationManager.AppSettings["InsertUserAgreementFunctionXFunctionsKey"];
            }
        }
        public static string ByPassUserAgreementFunction
        {
            get
            {
                return ConfigurationManager.AppSettings["ByPassUserAgreementFunction"];
            }
        }

        public static string ByPassUserAgreementFunctionXFunctionsKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ByPassUserAgreementFunctionXFunctionsKey"];
            }
        }

        public static string endpoint
        {
            get
            {
                return ConfigurationManager.AppSettings["endpoint"];
            }
        }

        public static string authKey
        {
            get
            {
                return ConfigurationManager.AppSettings["authKey"];
            }
        }

        public static string database
        {
            get
            {
                return ConfigurationManager.AppSettings["database"];
            }
        }

        public static string collection
        {
            get
            {
                return ConfigurationManager.AppSettings["collection"];
            }
        }

        public static string tlui2AccountName
        {
            get
            {
                return ConfigurationManager.AppSettings["tlui2AccountName"];
            }
        }

        public static string tlui2AccountKey
        {
            get
            {
                return ConfigurationManager.AppSettings["tlui2AccountKey"];
            }
        }

        public static string tlui2Container
        {
            get
            {
                return ConfigurationManager.AppSettings["tlui2Container"];
            }
        }
        public static string tlui2DocumentContainer
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["tlui2DocumentContainer"]);
            }
        }
        public static string TwilioAccountSid
        {
            get
            {
                return ConfigurationManager.AppSettings["TwilioAccountSid"];
            }
        }
        public static string TwilioAuthToken
        {
            get
            {
                return ConfigurationManager.AppSettings["TwilioAuthToken"];
            }
        }
        public static string FromMobileNumber
        {
            get
            {
                return ConfigurationManager.AppSettings["FromMobileNumber"];
            }
        }

        public static string TwilioMessagingServiceSid
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["TwilioMessagingServiceSid"]);
            }
        }

        public static string TwilioSID
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["TwilioSID"]);
            }
        }

        public static string TwilioAuth
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["TwilioAuth"]);
            }
        }

        public static string SendGridAPIKey
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["SendGridAPIKey"]);
            }
        }

        public static string PortalURL
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["PortalURL"]);
            }
        }

        public static string GoogleClientId
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["GoogleClientId"]);
            }
        }

        public static string GoogleClientSecret
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["GoogleClientSecret"]);
            }
        }

        public static string GoogleRedirectUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["GoogleRedirectUrl"]);
            }
        }

        public static string SenderId
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["SenderId"]);
            }
        }
        public static string SalonOwnerUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["SalonOwnerUrl"]);
            }
        }

        public static string VendorAdminUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["VendorAdminUrl"]);
            }
        }

        public static string BlogBaseUrl
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["BlogBaseUrl"]);
            }
        }

    }
}
