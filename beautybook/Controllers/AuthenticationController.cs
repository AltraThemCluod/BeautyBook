using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BeautyBook.Common;
using BeautyBook.Entities.V1;
using Newtonsoft.Json;

namespace BeautyBook.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult SignIn(string email = "", string Password = "",string ErrorMsg = "")
        {
            ViewBag.UserEmail = email;
            ViewBag.UserPassword = Password;
            ViewBag.SignInErrorMsg = ErrorMsg;
            return View();
        }  
        
        public ActionResult EmployeeSignIn()
        {
            return View();
        }
        
        public ActionResult SignUp()
        {
            
            return View();
        }

        public ActionResult EmailVerification()
        {
            return View();
        }

        public ActionResult EmailVerificationCode()
        {
            return View();
        }

        public ActionResult VerificationCode()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

       

        public async Task<ActionResult> SaveGoogleUser(string code = "", string state = "")
        {
            if (string.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.googleapis.com")
            };
            var requestUrl = $"oauth2/v4/token?code={code}&client_id=" + Configurations.GoogleClientId + "&client_secret=" + Configurations.GoogleClientSecret + "&redirect_uri=" + Configurations.GoogleRedirectUrl + "&grant_type=authorization_code";

            var dict = new Dictionary<string, string>
            {
                { "Content-Type", "application/x-www-form-urlencoded" }
            };
            var req = new HttpRequestMessage(HttpMethod.Post, requestUrl) { Content = new FormUrlEncodedContent(dict) };
            var response = await httpClient.SendAsync(req);
            var token = JsonConvert.DeserializeObject<GmailToken>(await response.Content.ReadAsStringAsync());
            Session["user"] = token.AccessToken;
            var obj = await GetuserProfile(token.AccessToken);

            //IdToken property stores user data in Base64Encoded form  
            //var data = Convert.FromBase64String(token.IdToken.Split('.')[1]);  
            //var base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);  


            // obj.Email->already exists ?

            //1.Yes->e user nu details fetch, cookie ma store and login kari devanu
            //  2.No->toh e user sign up(google nu identity id insert karvanu), random password generate kari ne email mokalvanu and pachi direct login

            Users users = new Users();
            users.Email = obj.Email;
            users.FirstName = obj.FamilyName;
            users.SecondName = obj.GivenName;
            users.LookUpUserTypeId = 2;
            users.PasswordHash = CommonHelper.RandomPassword();


            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.zaan.com.sa/")
            };

            httpClient.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            requestUrl = $"api/users/Users_SignUp";

            req = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            //var content = new JObject(users);

            var json = new JavaScriptSerializer().Serialize(users);


            // Add body content
            req.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var responseAPi = await httpClient.SendAsync(req);

            var readTask = responseAPi.Content.ReadAsStringAsync().Result;
            //JObject jsonObj = JObject.Parse(readTask);

            SuccessResult<Users> successResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SuccessResult<Users>>(readTask);

            if (successResult.Code == 200 && successResult.Item != null)
            {
                return RedirectToAction("SignIn", "Authentication", new { email = successResult.Item.Email, password = ConvertTo.Base64Encode(successResult.Item.PasswordHash) });
            }
            else
            {
                httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                requestUrl = $"api/users/User_ByEmail?Email={obj.Email}&IsSeller=0";

                req = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                //var content = new JObject(users);

                var jsonEmail = new JavaScriptSerializer().Serialize(users);


                // Add body content
                req.Content = new StringContent(
                    jsonEmail,
                    Encoding.UTF8,
                    "application/json"
                );

                var responseEmailAPi = await httpClient.SendAsync(req);

                var readEmailTask = responseEmailAPi.Content.ReadAsStringAsync().Result;
                //JObject jsonObj = JObject.Parse(readTask);

                SuccessResult<Users> successEmailResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SuccessResult<Users>>(readEmailTask);

                return RedirectToAction("SignIn", "Authentication", new { email = successEmailResult.Item.Email, password = ConvertTo.Base64Encode(successEmailResult.Item.PasswordHash) });
            }

            return View("UserProfile", obj);

        }

        public async Task<UserProfile> GetuserProfile(string accesstoken)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://www.googleapis.com")
            };
            string url = $"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={accesstoken}";
            var response = await httpClient.GetAsync(url);
            return JsonConvert.DeserializeObject<UserProfile>(await response.Content.ReadAsStringAsync());
        }
        public static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
    }

    public partial class UserProfile
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }

    public class GmailToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }
    }
}