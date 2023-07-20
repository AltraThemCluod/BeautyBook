using BeautyBook.Common;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace beautybookApi.PayTabs
{
    public static class RestApis
    {
        public static string Payment(decimal TotalAmount, string cart_id,string cart_description , string OrderId)
        {

            try
            {
                var FrontEndUrl = @System.Configuration.ConfigurationManager.AppSettings["SalonOwnerUrl"];
                var client = new RestClient(@System.Configuration.ConfigurationManager.AppSettings["RestClient"]);
                
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", @System.Configuration.ConfigurationManager.AppSettings["Authorization"]);
                var body = @"{" + "\n" +
                    @"""profile_id"": ""73991""," + "\n" +
                    @"""tran_type"": ""sale""," + "\n" +
                    @"""tran_class"": ""ecom"" ," + "\n" +
                    @"""cart_id"": """ + cart_id +  @" ""," + "\n" +
                    @"""cart_description"": """ + cart_description + @"""," + "\n" +
                    @"""cart_currency"": ""SAR""," + "\n" +
                    @"""cart_amount"": " + TotalAmount + "," + "\n" +
                    @"""callback"": ""https://salon.zaan.com.sa/""," + "\n" +
                    @"""return"": """+ FrontEndUrl + "Payment/PaymentCallback?OrdersId=" + OrderId + @"""" + "\n" +
                    @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return (response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        public static string PaymentEmailPackages(decimal TotalAmount, string cart_id, string cart_description, string PackageId)
        {

            try
            {
                var FrontEndUrl = @System.Configuration.ConfigurationManager.AppSettings["SalonOwnerUrl"];

                var client = new RestClient(@System.Configuration.ConfigurationManager.AppSettings["RestClient"]);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", @System.Configuration.ConfigurationManager.AppSettings["Authorization"]);
                var body = @"{" + "\n" +
                    @"""profile_id"": ""73991""," + "\n" +
                    @"""tran_type"": ""sale""," + "\n" +
                    @"""tran_class"": ""ecom"" ," + "\n" +
                    @"""cart_id"": """ + cart_id + @" ""," + "\n" +
                    @"""cart_description"": """ + cart_description + @"""," + "\n" +
                    @"""cart_currency"": ""SAR""," + "\n" +
                    @"""cart_amount"": " + TotalAmount + "," + "\n" +
                    @"""callback"": ""https://salon.zaan.com.sa/""," + "\n" +
                    @"""return"": """ + FrontEndUrl + "Payment/PackagesPaymentCallback?PackagesId=" + PackageId + @"""" + "\n" +
                    @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return (response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        public static string PaymentSMSPackages(decimal TotalAmount, string cart_id, string cart_description, string PackageId)
        {

            try
            {
                var FrontEndUrl = @System.Configuration.ConfigurationManager.AppSettings["SalonOwnerUrl"];

                var client = new RestClient(@System.Configuration.ConfigurationManager.AppSettings["RestClient"]);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", @System.Configuration.ConfigurationManager.AppSettings["Authorization"]);
                var body = @"{" + "\n" +
                    @"""profile_id"": ""73991""," + "\n" +
                    @"""tran_type"": ""sale""," + "\n" +
                    @"""tran_class"": ""ecom"" ," + "\n" +
                    @"""cart_id"": """ + cart_id + @" ""," + "\n" +
                    @"""cart_description"": """ + cart_description + @"""," + "\n" +
                    @"""cart_currency"": ""SAR""," + "\n" +
                    @"""cart_amount"": " + TotalAmount + "," + "\n" +
                    @"""callback"": ""https://salon.zaan.com.sa/""," + "\n" +
                    @"""return"": """ + FrontEndUrl + "Payment/SMSPackagesPaymentCallback?PackagesId=" + PackageId + @"""" + "\n" +
                    @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return (response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

    }
}