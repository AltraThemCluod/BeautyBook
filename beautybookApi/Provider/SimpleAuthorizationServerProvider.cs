using BeautyBook.Entities.Contract;
using BeautyBook.Entities.V1;
using BeautyBook.Services.V1;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;


namespace BeautyBookApi.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
       
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string DeviceToken = context.Parameters.Where(f => f.Key == "DeviceToken").Select(f => f.Value).SingleOrDefault()[0];
            long LoginType = Convert.ToInt32(context.Parameters.Where(f => f.Key == "LoginType").Select(f => f.Value).SingleOrDefault()[0]);
            long LookUpUserTypeId = Convert.ToInt32(context.Parameters.Where(f => f.Key == "LookUpUserTypeId").Select(f => f.Value).SingleOrDefault()[0]);
            
            context.OwinContext.Set<string>("DeviceToken", DeviceToken);
            context.OwinContext.Set<long>("LoginType", LoginType);
            context.OwinContext.Set<long>("LookUpUserTypeId", LookUpUserTypeId);
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string IpAddress = context.Request.RemoteIpAddress;

            string DeviceToken = context.OwinContext.Get<string>("DeviceToken");
            long LoginType = context.OwinContext.Get<long>("LoginType");
            long LookUpUserTypeId = context.OwinContext.Get<long>("LookUpUserTypeId");

            UsersServices_Token usersServices_Token = new UsersServices_Token();

            var result = await usersServices_Token.Users_Login(context.UserName, context.Password, DeviceToken, LoginType , LookUpUserTypeId);


            if (result.Code != 200)
            {
                context.SetError("invalid_grant", result.Message);
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("UserID", Convert.ToString(result.Item.Id))); //result.Item.Id
            //identity.AddClaim(new Claim("AccountID", Convert.ToString(0))); //result.Item.AccountId
            //identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }

        private void SetCORSPolicy(IOwinContext context)
        {
            //string allowedUrls = ConfigurationManager.AppSettings["allowedOrigins"];

            //if (!String.IsNullOrWhiteSpace(allowedUrls))
            //{
            //    var list = allowedUrls.Split(',');
            //    if (list.Length > 0)
            //    {

            //        string origin = context.Request.Headers.Get("Origin");
            //        var found = list.Where(item => item == origin).Any();
            //        if (found)
            //        {
            //            context.Response.Headers.Add("Access-Control-Allow-Origin",
            //                                         new string[] { origin });
            //        }
            //    }

            //}
            context.Response.Headers.Add("Access-Control-Allow-Origin",
                                         new string[] { "*" });
            context.Response.Headers.Add("Access-Control-Allow-Headers",
                                   new string[] { "Authorization", "Content-Type" });
            context.Response.Headers.Add("Access-Control-Allow-Methods",
                                   new string[] { "OPTIONS", "POST" });

        }
    }
}